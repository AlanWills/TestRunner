using System;
using System.Diagnostics;
using System.Reflection;

namespace TestExtensions
{
    /// <summary>
    /// A mock class wrapper around a class we wish to test.
    /// Provides a way to execute protected and private functions and obtain protected and private properties, without having to
    /// write public wrappers around them in an inherited mock class.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Mock<T> where T : class
    {
        private T ClassInstance { get; set; }

        public Mock(params object[] constructorParameters)
        {
            ClassInstance = (T)Activator.CreateInstance(typeof(T), constructorParameters);
            Debug.Assert(ClassInstance != null);
        }

        /// <summary>
        /// Execute a public or non-public instance method that has no return value on the mocked object.
        /// </summary>
        /// <param name="methodName">The name of the method to execute</param>
        /// <param name="parametersToMethod">The parameters the method requires</param>
        public void ExecuteMethod(string methodName, params object[] parametersToMethod)
        {
            MethodInfo method = ClassInstance.GetType().GetMethod(methodName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);
            Debug.Assert(method != null, "Method " + methodName + " could not be found");

            method.Invoke(ClassInstance, parametersToMethod);
        }

        /// <summary>
        /// Execute a public or non-public instance method that returns a value, and cast it to the type K.
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <param name="methodName"></param>
        /// <param name="parametersToMethod"></param>
        /// <returns></returns>
        public K ExecuteMethod<K>(string methodName, params object[] parametersToMethod)
        {
            MethodInfo method = ClassInstance.GetType().GetMethod(methodName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);
            Debug.Assert(method != null, "Method " + methodName + " could not be found");

            return (K)method.Invoke(ClassInstance, parametersToMethod);
        }

        /// <summary>
        /// An explicit operator which converts the mock to it's underlying type, allowing the mock to be used as the type T.
        /// </summary>
        /// <param name="mock"></param>
        public static explicit operator T(Mock<T> mock)
        {
            return mock.ClassInstance;
        }
    }
}
