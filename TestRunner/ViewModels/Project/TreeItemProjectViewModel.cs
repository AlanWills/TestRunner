using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestRunner.ViewModels
{
    /// <summary>
    /// A class for representing a project in a Tree.
    /// </summary>
    public class TreeItemProjectViewModel
    {
        #region Properties and Fields

        public Project Project { get; private set; }

        public bool IsExpanded { get; set; }

        public string Name
        {
            get { return Project.Name; }
        }

        public List<TestResult> TestResults
        {
            get { return Project.TestResults; }
        }

        #endregion

        public TreeItemProjectViewModel(Project project)
        {
            Project = project;
        }
    }
}
