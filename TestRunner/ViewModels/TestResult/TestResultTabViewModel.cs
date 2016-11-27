using System.Collections.ObjectModel;
using TestRunner.Models;

namespace TestRunner.ViewModels
{
    public class TestResultTabViewModel
    {
        #region Properties and Fields

        private TestResult TestResult { get; set; }

        public ObservableCollection<UnitTestResult> UnitTestResults { get; private set; }
        
        public string Header { get { return TestResult.Name; } }

        public string ToolTip { get { return TestResult.Name; } }

        #endregion

        public TestResultTabViewModel(TestResult testResult)
        {
            TestResult = testResult;

            // Need a VM for this
            UnitTestResults = new ObservableCollection<UnitTestResult>(testResult.UnitTests);
        }
    }
}
