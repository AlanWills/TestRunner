using System;
using System.Collections.Generic;
using TestRunner.Models;

namespace TestRunner.ViewModels
{
    public class TreeItemTestResultViewModel : TreeItemBaseViewModel
    {
        #region Properties and Fields

        private TestResult TestResult { get; set; }

        public string Name
        {
            get { return TestResult.Name; }
        }

        public bool Passed
        {
            get { return TestResult.Passed; }
        }

        public List<UnitTestResult> UnitTests
        {
            get { return TestResult.UnitTests; }
        }

        #endregion

        public TreeItemTestResultViewModel(TestResult testResult)
        {
            TestResult = testResult;
        }

        public override void RefreshOnProjectChanged(ProjectChangedEventArgs args)
        {
            // No-op
        }
    }
}
