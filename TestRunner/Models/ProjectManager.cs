using System.Xml;
using System.Xml.Serialization;
using TestRunner.Commands;

namespace TestRunner
{
    public static class ProjectManager
    {
        #region Properties and Fields

        private static Project CurrentProject { get; set; }

        private static NewProjectCommand newProjectCommand;
        public static NewProjectCommand NewProjectCommand
        {
            get
            {
                return newProjectCommand ?? (newProjectCommand = new NewProjectCommand());
            }
        }

        private static OpenProjectCommand openProjectCommand;
        public static OpenProjectCommand OpenProjectCommand
        {
            get
            {
                return openProjectCommand ?? (openProjectCommand = new OpenProjectCommand());
            }
        }

        private static RunProjectCommand runProjectCommand;
        public static RunProjectCommand RunProjectCommand
        {
            get
            {
                return runProjectCommand ?? (runProjectCommand = new RunProjectCommand());
            }
        }

        #endregion

        static ProjectManager()
        {
            OpenProjectCommand.ProjectLoaded += ProjectLoaded;
        }

        private static void ProjectLoaded(Project loadedProject)
        {
            CurrentProject = loadedProject;
        }
    }
}
