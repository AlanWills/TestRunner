using System.Windows.Media;
using TestRunner.Models;

namespace TestRunner.ViewModels
{
    public class UnitTestResultTextViewModel
    {
        #region Properties and Fields

        private UnitTestResult UnitTestResult { get; set; }

        public string UnitTestName { get { return UnitTestResult.Name; } }

        public Brush ForegroundColour
        {
            get
            {
                return UnitTestResult.Passed ? Brushes.LawnGreen : Brushes.Red;
            }
        }

        #endregion

        public UnitTestResultTextViewModel(UnitTestResult unitTestResult)
        {
            UnitTestResult = unitTestResult;
        }
    }
}
