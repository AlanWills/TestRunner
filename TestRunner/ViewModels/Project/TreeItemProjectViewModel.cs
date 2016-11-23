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

        public string TimeUntilNextRunDisplayString
        {
            get { return TimeUntilNextRun.ToString(); }
        }

        private TimeSpan TimeUntilNextRun { get; set; }

        public ObservableCollection<TreeItemFolderViewModel> TestFolders { get; private set; }

        private Timer Timer { get; set; }

        #endregion

        public TreeItemProjectViewModel(Project project)
        {
            Project = project;
            DisplayString = Project.Name + "  (" + project.Frequency.ToTestRunFrequency().ToDisplayString() + ")";
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

            Timer = new Timer(UpdateTimeText, null, 0, 1000);
        }

        private void UpdateTimeText(object state)
        {
            TimeUntilNextRun -= TimeSpan.FromSeconds(1);
        }

        public override void RefreshOnProjectChanged(ProjectChangedEventArgs args)
        {
            TimeUntilNextRun = Project.Frequency;

            foreach (TreeItemFolderViewModel folder in TestFolders)
            {
                folder.RefreshOnProjectChanged(args);
            }
        }
    }
}
