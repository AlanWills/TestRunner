using TestRunner.Commands;

namespace TestRunner
{
    public static class ProjectManager
    {
        #region Properties and Fields

        public static Project CurrentProject { get; private set; }

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

        private static bool Initialized { get; set; }

        #endregion

        public static void Initialize()
        {
            if (!Initialized)
            {
                OpenProjectCommand.ProjectLoaded += ProjectLoaded;
                Initialized = true;
            }
        }

        private static void ProjectLoaded(Project loadedProject)
        {
            CurrentProject = loadedProject;
        }
    }
}
