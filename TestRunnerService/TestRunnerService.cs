using System;
using System.Diagnostics;
using System.IO;

namespace TestRunnerService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class TestRunnerService : ITestRunnerService
    {
        public int StartTesting(string testConfigFilePath)
        {
            TestRunnerProcess testRunnerProcess = new TestRunnerProcess("\"TestKernel.dll\"");
            
            return 0;
        }

        public TestingStatus GetTestingStatus(string testConfigFilePath)
        {
            return TestingStatus.kFinished;
        }

        public string GetTestingResultsFilePath(int testingRunID)
        {
            return "";
        }
    }
}
