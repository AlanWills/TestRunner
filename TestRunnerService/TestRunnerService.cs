using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace TestRunnerService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class TestRunnerService : ITestRunnerService
    {
        public void StartTesting(string testConfigFilePath)
        {
            Debug.Assert(testConfigFilePath != null && testConfigFilePath.Length > 0);
            TestRunnerProcessManager.CreateProcess(testConfigFilePath);
        }

        public bool GetTestingStatus(ulong testingProcessID)
        {
            return TestRunnerProcessManager.GetProcessStatus(testingProcessID);
        }

        public string GetProcessOutput(ulong testingProcessID)
        {
            return TestRunnerProcessManager.GetProcessOutput(testingProcessID);
        }

        public string GetProcessError(ulong testingProcessID)
        {
            return TestRunnerProcessManager.GetProcessOutput(testingProcessID);
        }

        public List<string> GetAllConfigFilePaths()
        {
            return TestRunnerProcessManager.GetAllConfigFilePaths();
        }

        public string GetProcessConfigFilePath(ulong testingProcessID)
        {
            return TestRunnerProcessManager.GetProcessConfigFilePath(testingProcessID);
        }
    }
}
