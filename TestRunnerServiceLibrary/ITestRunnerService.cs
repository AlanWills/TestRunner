using System.Collections.Generic;
using System.ServiceModel;

namespace TestRunnerServiceLibrary
{
    [ServiceContract]
    public interface ITestRunnerService
    {
        [OperationContract]
        void StartTesting(string testConfigFilePath);

        [OperationContract]
        bool GetTestingStatus(ulong testingProcessID);

        [OperationContract]
        string GetProcessOutput(ulong testingProcessID);

        [OperationContract]
        string GetProcessError(ulong testingProcessID);

        [OperationContract]
        string GetProcessConfigFilePath(ulong testingProcessID);

        [OperationContract]
        List<string> GetAllConfigFilePaths();
    }
}
