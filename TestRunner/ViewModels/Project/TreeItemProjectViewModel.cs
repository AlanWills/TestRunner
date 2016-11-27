using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using TestRunner.Extensions;

namespace TestRunner.ViewModels
{
    /// <summary>
    /// A class for representing a project in a Tree.
    /// </summary>
    public class TreeItemProjectViewModel : TreeItemBaseViewModel
    {
        #region Properties and Fields

        public Project Project { get; private set; }

        public string DisplayString { get; private set; }

        public string TimeUntilNextRunDisplayString { get; private set; }

        private TimeSpan TimeUntilNextRun { get; set; }

        public ObservableCollection<TreeItemFolderViewModel> TestFolders { get; private set; }

        private Timer Timer { get; set; }

        #endregion

        public TreeItemProjectViewModel(Project project)
        {
            Project = project;
            DisplayString = Project.Name + "  (" + project.Frequency.ToTestRunFrequency().ToDisplayString() + ")";
            TimeUntilNextRunDisplayString = "";
            TestFolders = new ObservableCollection<TreeItemFolderViewModel>();

            Dictionary<DateTime, List<TestResult>> datesAndTests = new Dictionary<DateTime, List<TestResult>>();
            foreach (TestResult result in project.TestResults)
            {
                if (!datesAndTests.ContainsKey(result.DateOfTesting.Date))
                {
                    datesAndTests.Add(result.DateOfTesting.Date, new List<TestResult>());
                }

                datesAndTests[result.DateOfTesting.Date].Add(result);
            }

            foreach (KeyValuePair<DateTime, List<TestResult>> dateTestPair in datesAndTests)
            {
                TestFolders.Add(new TreeItemFolderViewModel(dateTestPair));
            }

            Timer = new Timer(UpdateTimeText, null, Timeout.Infinite, 1000);
        }

        private void UpdateTimeText(object state)
        {
            TimeUntilNextRun -= TimeSpan.FromSeconds(1);
            TimeUntilNextRunDisplayString = TimeUntilNextRun.ToString();
        }

        public override void RefreshOnProjectChanged(ProjectChangedEventArgs args)
        {
            TimeUntilNextRun = Project.Frequency;
            Timer.Change(0, 1000);

            HashSet<DateTime> currentDates = new HashSet<DateTime>();
            foreach (TreeItemFolderViewModel folder in TestFolders)
            {
                currentDates.Add(folder.Date.Date);
            }

            foreach (TestResult result in args.AddedTests)
            {
                if (!currentDates.Contains(result.DateOfTesting.Date))
                {
                    currentDates.Add(result.DateOfTesting.Date);
                    TestFolders.Add(new TreeItemFolderViewModel(result.DateOfTesting, new List<TestResult>()));
                }
            }

            foreach (TreeItemFolderViewModel folder in TestFolders)
            {
                folder.RefreshOnProjectChanged(args);
            }
        }
    }
}
