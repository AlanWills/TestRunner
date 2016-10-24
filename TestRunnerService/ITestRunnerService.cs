using System.Collections.Generic;
using System.ServiceModel;

namespace TestRunnerService
{
    public enum TestingStatus
    {
        kRunning,
        kFinished,
    }

    [ServiceContract]
    public interface ITestRunnerService
    {
        [OperationContract]
        void StartTesting(string testConfigFilePath);

        [OperationContract]
        TestingStatus GetTestingStatus(ulong testingRunID);

        [OperationContract]
        string GetProcessOutput(ulong testingRunID);

        [OperationContract]
        string GetProcessError(ulong testingRunID);

        [OperationContract]
        List<string> GetAllProcesses();
    }
}
