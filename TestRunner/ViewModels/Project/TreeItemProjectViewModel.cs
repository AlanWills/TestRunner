using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestRunner.ViewModels
{
    /// <summary>
    /// A class for representing a project in a Tree.
    /// </summary>
    public class TreeItemProjectViewModel : TreeItemBaseViewModel
    {
        #region Properties and Fields

        public Project Project { get; private set; }

        public string Name
        {
            get { return Project.Name; }
        }

        public ObservableCollection<TreeItemFolderViewModel> TestFolders { get; private set; }

        #endregion

        public TreeItemProjectViewModel(Project project)
        {
            Project = project;
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
        }

        public override void RefreshOnProjectChanged(ProjectChangedEventArgs args)
        {
            foreach (TreeItemFolderViewModel folder in TestFolders)
            {
                folder.RefreshOnProjectChanged(args);
            }
        }
    }
}
