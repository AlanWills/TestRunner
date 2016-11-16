using System.Diagnostics;

namespace TestExtensions
{
    /// <summary>
    /// A trace listener we use in unit tests to assert that an assertion has indeed been raised.
    /// Catches the assertion and prevents the modal dialog from appearing, but still logs the assertion being caught.
    /// Probably best to just use this to test one assertion - the natural scoping of unit tests should provide a nice way to wrap this up.
    /// </summary>
    public class CatchTraceListener : TraceListener
    {
        #region Properties and Fields

        public bool AssertionCaught { get; private set; }

        #endregion

        public override void Write(string message)
        {
            AssertionCaught = true;
        }

        public override void WriteLine(string message)
        {
            AssertionCaught = true;
        }
    }
}
