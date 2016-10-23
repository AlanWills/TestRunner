using System.Diagnostics;

namespace TestRunnerService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class TestRunnerService : ITestRunnerService
    {
        public ulong StartTesting(string testConfigFilePath)
        {
            //C:\\Users\\Alan\\AppData\\Local\\Packages\\TestRunner_cxt6mtc0m2z7c\\LocalState\\Test.xml
            Debug.Assert(testConfigFilePath != null && testConfigFilePath.Length > 0);
            return TestRunnerProcessManager.CreateProcess(testConfigFilePath);
        }

        public TestingStatus GetTestingStatus(ulong testingRunID)
        {
            return TestRunnerProcessManager.GetProcessStatus(testingRunID);
        }

        public string GetOutputFilePath(ulong testingRunID)
        {
            return TestRunnerProcessManager.GetProcessOutputFilePath(testingRunID);
        }

        public string GetErrorFilePath(ulong testingRunID)
        {
            return TestRunnerProcessManager.GetProcessOutputFilePath(testingRunID);
        }
    }
}
