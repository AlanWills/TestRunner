﻿using System.Collections.Generic;
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
        /// Adds a test process for the inputted project if it does not exist and stores it in our dictionary.
        /// Then starts a process to run the unit tests in the project's dll.
        /// </summary>
        /// <param name="project"></param>
        public static void StartProcess(Project project)
        {
            Debug.Assert(project != null);

            if (!Processes.ContainsKey(project))
            {
                // Add our process to our dictionary and then return the ID so we can obtain it again later
                Processes.Add(project, new TestProcess(project));
            }

            GetProcess(project).Start();
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
