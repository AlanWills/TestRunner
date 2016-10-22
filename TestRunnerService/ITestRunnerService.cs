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
        ulong StartTesting(string testConfigFilePath);

        [OperationContract]
        TestingStatus GetTestingStatus(string testConfigFilePath);

        [OperationContract]
        string GetTestingResultsFilePath(ulong testingRunID);
    }
}
