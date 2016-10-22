using System;
using System.Diagnostics;
using System.IO;

namespace TestRunnerService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class TestRunnerService : ITestRunnerService
    {
        public ulong StartTesting(string testConfigFilePath)
        {
            return TestRunnerProcessManager.CreateProcess("\"TestKernel.dll\"");
        }

        public TestingStatus GetTestingStatus(string testConfigFilePath)
        {
            return TestingStatus.kFinished;
        }

        public string GetTestingResultsFilePath(ulong testingRunID)
        {
            return "";
        }
    }
}
