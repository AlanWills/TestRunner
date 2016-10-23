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
        TestingStatus GetTestingStatus(ulong testingRunID);

        [OperationContract]
        string GetOutputFilePath(ulong testingRunID);

        [OperationContract]
        string GetErrorFilePath(ulong testingRunID);
    }
}
