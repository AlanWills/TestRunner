using System.Collections.ObjectModel;
using TestRunner.Models;

namespace TestRunner.ViewModels
{
    public class TestResultTabViewModel
    {
        #region Properties and Fields

        private TestResult TestResult { get; set; }

        public ObservableCollection<UnitTestResultTextViewModel> UnitTestResults { get; private set; }
        
        public string Name { get { return TestResult.Name.Replace(" ", "") + "Tab"; } }

        public string Header { get { return TestResult.Name; } }

        public string ToolTip { get { return TestResult.Name; } }

        public bool IsSelected { get; set; }

        #endregion

        public TestResultTabViewModel(TestResult testResult)
        {
            TestResult = testResult;
            IsSelected = true;

            UnitTestResults = new ObservableCollection<UnitTestResultTextViewModel>();

            foreach (UnitTestResult unitTestResult in testResult.UnitTests)
            {
                UnitTestResults.Add(new UnitTestResultTextViewModel(unitTestResult));
            }
        }
    }
}
