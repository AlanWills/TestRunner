using System.Collections.Generic;
using System.Diagnostics;

namespace TestRunner
{
    /// <summary>
    /// Static class which looks after all of the currently running testing process
    /// </summary>
    public static class TestRunnerProcessManager
    {
        #region Properties and Fields

        private static ulong IDCounter = 0;

        private static Dictionary<ulong, TestRunnerProcess> Processes = new Dictionary<ulong, TestRunnerProcess>();

        #endregion

        public static ulong CreateProcess(Project project)
        {
            Debug.Assert(project != null);

            // We should never be creating a process with an ID that already exists in our running processes
            Debug.Assert(!Processes.ContainsKey(IDCounter));

            // Add our process to our dictionary and then return the ID so we can obtain it again later
            Processes.Add(IDCounter, new TestRunnerProcess(project));
            return IDCounter++;
        }

        public static bool GetProcessStatus(ulong processID)
        {
            return GetProcess(processID).HasExited;
        }
        
        private static TestRunnerProcess GetProcess(ulong processID)
        {
            // Make sure the requested process exists in our dictionary, otherwise we are in trouble!
            Debug.Assert(Processes.ContainsKey(processID));

            return Processes[processID];
        }
    }
}
