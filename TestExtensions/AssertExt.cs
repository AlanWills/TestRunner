using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace TestExtensions
{
    public static class AssertExt
    {
        /// <summary>
        /// Assert that two inputted instances are equal using a custom equality function rather than '==' or Equals().
        /// Useful for checking values are within a reasonably tolerance of one another for example.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="equalityFunction"></param>
        public static void AreEqual<T>(T expected, T actual, Func<T, T, bool> equalityFunction)
        {
            Assert.IsTrue(equalityFunction.Invoke(expected, actual), "Expected: " + expected.ToString() + " but got: " + actual.ToString());
        }

        /// <summary>
        /// Checks that the inputted parameters does not cause the inputted function to throw an exception OR raise a debug assert.
        /// If either of these things happen, the test will fail.
        /// </summary>
        /// <param name="functionToTest"></param>
        /// <param name="arguments"></param>
        public static object DoesNotThrow(Delegate functionToTest, params object[] arguments)
        {
            CatchTraceListener assertListener = new CatchTraceListener();

            // Set up the assertion framework to just use our listener
            Trace.Listeners.Clear();
            Trace.Listeners.Add(assertListener);

            object result = null;

            try
            {
                result = functionToTest.DynamicInvoke(arguments);
            }
            catch (Exception e)
            {
                // If we have thrown an exception the test should fail
                Assert.Fail("Exception thrown with message: " + e.Message);
            }

            Assert.IsFalse(assertListener.AssertionCaught, "Assertion raised");

            // Reset it so that asserts still happen for the remainder of our test run
            Trace.Refresh();

            return result;
        }

        /// <summary>
        /// Check that the inputted parameters cause an exception to be thrown by the inputted method.
        /// </summary>
        /// <param name="functionToTest">Will have to pass in the method group casted to the appropriate Func.
        /// e.g. public int MyFunction(string str, bool result) -> AssertExt.AssertThrows((Func<str, bool, int>)MyFunction);</param>
        /// <param name="arguments"></param>
        public static void Throws(Delegate functionToTest, params object[] arguments)
        {
            bool hasThrown = false;
            object result = null;

            try
            {
                result = functionToTest.DynamicInvoke(arguments);
            }
            catch (Exception e) when (e is MemberAccessException || e is ArgumentException)
            {
                // If we have caught this exception, the call to DynamicInvoke failed, rather than the method failing itself
                // Wrong kind of throw!
                Assert.Fail();
            }
            catch
            {
                // The method threw an exception within it and so the test is a success!
                hasThrown = true;
            }

            Assert.IsTrue(hasThrown, "Obtained result: " + (result != null ? result.ToString() : "null"));
        }

        /// <summary>
        /// Check that the inputted parameters cause the inputted exception to be thrown by the inputted method.
        /// </summary>
        /// <param name="functionToTest">Will have to pass in the method group casted to the appropriate Func.
        /// e.g. public int MyFunction(string str, bool result) -> AssertExt.Throws<InvalidCastException>((Func<str, bool, int>)MyFunction);</param>
        /// <param name="arguments"></param>
        public static void Throws<T>(Delegate functionToTest, params object[] arguments) where T : Exception
        {
            bool hasThrown = false;
            object result = null;

            try
            {
                result = functionToTest.DynamicInvoke(arguments);
            }
            catch (Exception e) when (e is T)
            {
                // The method threw an exception within it and so the test is a success!
                hasThrown = true;
            }
            catch (Exception e) when (e is MemberAccessException || e is ArgumentException)
            {
                // If we have caught this exception, the call to DynamicInvoke failed, rather than the method failing itself
                // Wrong kind of throw!
                Assert.Fail();
            }

            Assert.IsTrue(hasThrown, "Obtained result: " + (result != null ? result.ToString() : "null"));
        }

        /// <summary>
        /// Ensure that the arguments passed to the method raise a Debug assertion.
        /// Will catch the dialog so that it does not block our tests from continuing.
        /// </summary>
        /// <param name="functionToTest">Will have to pass in the method group casted to the appropriate Func.
        /// e.g. public int MyFunction(string str, bool result) -> AssertExt.AssertDebugAssertionRaised((Func<str, bool, int>)MyFunction);</param>
        /// <param name="arguments"></param>
        public static void DebugAssertionRaised(Delegate functionToTest, params object[] arguments)
        {
            CatchTraceListener assertListener = new CatchTraceListener();

            // Set up the assertion framework to just use our listener
            Trace.Listeners.Clear();
            Trace.Listeners.Add(assertListener);

            try
            {
                functionToTest.DynamicInvoke(arguments);
            }
            catch
            {
                // If an assert fires, it is likely an exception was also caused.
                // Swallow the exception for now
            }

            Assert.IsTrue(assertListener.AssertionCaught);

            // Reset it so that asserts still happen for the remainder of our test run
            Trace.Refresh();
        }

        /// <summary>
        /// Specialisation of the more generic method, as we cannot use it with arrays as input.
        /// Checks the two arrays are of the same size and contain the same elements in the same order.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        public static void ContentsEqual<T>(T[] expected, T[] actual) where T : IEquatable<T>
        {
            // Could explicitly write out the logic, but instead am going to use the other function
            // This is probably slower, but it's testing so speed is not of the utmost importance
            ContentsEqual<List<T>, T>(expected.ToList(), actual.ToList());
        }

        /// <summary>
        /// Checks the inputted enumerable collections' contents.
        /// They must be the same size and then each element must be index-wise equal.
        /// e.g. For two lists, the Counts must be the same and then expected[i] == actual[i] must be true for all 0 <= i < expected.Length
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="K"></typeparam>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        public static void ContentsEqual<T, K>(T expected, T actual) where T : IEnumerable<K>, ICollection<K> where K : IEquatable<K>
        {
            // Check the sizes are the same
            if (expected.Count != actual.Count)
            {
                Assert.Fail("Sizes of inputs are different");
                return;
            }

            // Now pairwise check the elements of the collection - can't index access so have to use enumerators
            IEnumerator<K> expectedEnum = expected.GetEnumerator();
            IEnumerator<K> actualEnum = actual.GetEnumerator();

            while (expectedEnum.MoveNext())
            {
                actualEnum.MoveNext();
                Assert.AreEqual(expectedEnum.Current, actualEnum.Current, actualEnum.Current.ToString() + " should equal expected value " + expectedEnum.Current.ToString());
            }
        }

        /// <summary>
        /// Checks the inputted enumerable collections' contents.
        /// They must be the same size and then each element must be index-wise equal.
        /// e.g. For two lists, the Counts must be the same and then expected[i] == actual[i] must be true for all 0 <= i < expected.Length
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="K"></typeparam>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="equalityFunction">A custom function that can be passed in to use as the equality check.  Useful if you do not wish to perform reference equality on custom classes.</param>
        public static void ContentsEqual<T, K>(T expected, T actual, Func<K, K, bool> equalityFunction) where T : IEnumerable<K>, ICollection<K>
        {
            // Check the sizes are the same
            if (expected.Count != actual.Count)
            {
                Assert.Fail("Sizes of inputs are different");
                return;
            }

            // Now pairwise check the elements of the collection - can't index access so have to use enumerators
            IEnumerator<K> expectedEnum = expected.GetEnumerator();
            IEnumerator<K> actualEnum = actual.GetEnumerator();

            while (expectedEnum.MoveNext())
            {
                actualEnum.MoveNext();
                Assert.IsTrue(equalityFunction.Invoke(expectedEnum.Current, actualEnum.Current), actualEnum.Current.ToString() + " should equal expected value " + expectedEnum.Current.ToString());
            }
        }
    }
}
