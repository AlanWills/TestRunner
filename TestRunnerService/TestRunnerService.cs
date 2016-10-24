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

        public TestingStatus GetTestingStatus(ulong testingRunID)
        {
            return TestRunnerProcessManager.GetProcessStatus(testingRunID);
        }

        public string GetProcessOutput(ulong testingRunID)
        {
            return TestRunnerProcessManager.GetProcessOutput(testingRunID);
        }

        public string GetProcessError(ulong testingRunID)
        {
            return TestRunnerProcessManager.GetProcessOutput(testingRunID);
        }
    }
}
