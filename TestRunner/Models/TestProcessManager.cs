using System.Collections.Generic;
using System.Diagnostics;

namespace TestRunner
{
    /// <summary>
    /// Static class which looks after all of the currently running testing process
    /// </summary>
    public static class TestProcessManager
    {
        #region Properties and Fields

        private static Dictionary<Project, TestProcess> Processes = new Dictionary<Project, TestProcess>();

        #endregion

        /// <summary>
        /// Adds a test process for the inputted project and stores it in our dictionary.
        /// </summary>
        /// <param name="project"></param>
        public static void CreateProcess(Project project)
        {
            Debug.Assert(project != null);

            // Add our process to our dictionary and then return the ID so we can obtain it again later
            Processes.Add(project, new TestProcess(project));
        }

        /// <summary>
        /// Returns whether the test process for the inputted project has completed it's testing.
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public static bool GetProcessStatus(Project project)
        {
            return GetProcess(project).HasExited;
        }

        /// <summary>
        /// Wrapper for obtaining a process from the dictionary and doing some debug checks.
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        private static TestProcess GetProcess(Project project)
        {
            // Make sure the requested process exists in our dictionary, otherwise we are in trouble!
            Debug.Assert(Processes.ContainsKey(project));

            return Processes[project];
        }
    }
}
