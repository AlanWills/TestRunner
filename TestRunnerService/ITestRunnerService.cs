using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace TestRunnerService
{
    [ServiceContract]
    public interface ITestRunnerService
    {
        [OperationContract]
        void StartTesting(string testConfigFilePath);

        [OperationContract]
        bool GetTestingStatus(ulong testingRunID);

        [OperationContract]
        string GetProcessOutput(ulong testingRunID);

        [OperationContract]
        string GetProcessError(ulong testingRunID);

        [OperationContract]
        List<string> GetAllProcesses();
    }
}
