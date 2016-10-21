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
        int StartTesting(string testConfigFilePath);

        [OperationContract]
        TestingStatus GetTestingStatus(string testConfigFilePath);

        [OperationContract]
        string GetTestingResultsFilePath(int testingRunID);
    }
}
