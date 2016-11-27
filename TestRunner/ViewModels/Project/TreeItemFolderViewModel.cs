using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace TestRunner.ViewModels
{
    public class TreeItemFolderViewModel : TreeItemBaseViewModel
    {
        #region Properties and Fields

        public DateTime Date { get; private set; }

        public string Name
        {
            get
            {
                return Date.ToLongDateString();
            }
        }

        public List<TreeItemTestResultViewModel> TestResults { get; private set; }

        #endregion

        public TreeItemFolderViewModel(KeyValuePair<DateTime, List<TestResult>> dateResultsPair) :
            this(dateResultsPair.Key, dateResultsPair.Value)
        {
        }

        public TreeItemFolderViewModel(DateTime date, List<TestResult> testResultsFromDate)
        {
            Date = date;
            TestResults = new List<TreeItemTestResultViewModel>();

            foreach (TestResult testResult in testResultsFromDate)
            {
                TestResults.Add(new TreeItemTestResultViewModel(testResult));
            }
        }

        public override void RefreshOnProjectChanged(ProjectChangedEventArgs args)
        {
            foreach (TestResult added in args.AddedTests)
            {
                if (added.DateOfTesting.Date == Date.Date)
                {
                    TestResults.Add(new TreeItemTestResultViewModel(added));
                }
            }

            foreach (TestResult removed in args.RemovedTests)
            {
                // Might need to expose the TestResult instance in the view model so we can compare it?
                Debug.Fail("TODO");
            }
        }
    }
}
