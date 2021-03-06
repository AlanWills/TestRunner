﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestRunner.TestRunnerService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="TestRunnerService.ITestRunnerService")]
    public interface ITestRunnerService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITestRunnerService/StartTesting", ReplyAction="http://tempuri.org/ITestRunnerService/StartTestingResponse")]
        void StartTesting(string testConfigFilePath);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITestRunnerService/StartTesting", ReplyAction="http://tempuri.org/ITestRunnerService/StartTestingResponse")]
        System.Threading.Tasks.Task StartTestingAsync(string testConfigFilePath);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITestRunnerService/GetTestingStatus", ReplyAction="http://tempuri.org/ITestRunnerService/GetTestingStatusResponse")]
        bool GetTestingStatus(ulong testingProcessID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITestRunnerService/GetTestingStatus", ReplyAction="http://tempuri.org/ITestRunnerService/GetTestingStatusResponse")]
        System.Threading.Tasks.Task<bool> GetTestingStatusAsync(ulong testingProcessID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITestRunnerService/GetProcessOutput", ReplyAction="http://tempuri.org/ITestRunnerService/GetProcessOutputResponse")]
        string GetProcessOutput(ulong testingProcessID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITestRunnerService/GetProcessOutput", ReplyAction="http://tempuri.org/ITestRunnerService/GetProcessOutputResponse")]
        System.Threading.Tasks.Task<string> GetProcessOutputAsync(ulong testingProcessID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITestRunnerService/GetProcessError", ReplyAction="http://tempuri.org/ITestRunnerService/GetProcessErrorResponse")]
        string GetProcessError(ulong testingProcessID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITestRunnerService/GetProcessError", ReplyAction="http://tempuri.org/ITestRunnerService/GetProcessErrorResponse")]
        System.Threading.Tasks.Task<string> GetProcessErrorAsync(ulong testingProcessID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITestRunnerService/GetProcessConfigFilePath", ReplyAction="http://tempuri.org/ITestRunnerService/GetProcessConfigFilePathResponse")]
        string GetProcessConfigFilePath(ulong testingProcessID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITestRunnerService/GetProcessConfigFilePath", ReplyAction="http://tempuri.org/ITestRunnerService/GetProcessConfigFilePathResponse")]
        System.Threading.Tasks.Task<string> GetProcessConfigFilePathAsync(ulong testingProcessID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITestRunnerService/GetAllConfigFilePaths", ReplyAction="http://tempuri.org/ITestRunnerService/GetAllConfigFilePathsResponse")]
        string[] GetAllConfigFilePaths();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITestRunnerService/GetAllConfigFilePaths", ReplyAction="http://tempuri.org/ITestRunnerService/GetAllConfigFilePathsResponse")]
        System.Threading.Tasks.Task<string[]> GetAllConfigFilePathsAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ITestRunnerServiceChannel : TestRunner.TestRunnerService.ITestRunnerService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class TestRunnerServiceClient : System.ServiceModel.ClientBase<TestRunner.TestRunnerService.ITestRunnerService>, TestRunner.TestRunnerService.ITestRunnerService {
        
        public TestRunnerServiceClient() {
        }
        
        public TestRunnerServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public TestRunnerServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TestRunnerServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TestRunnerServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void StartTesting(string testConfigFilePath) {
            base.Channel.StartTesting(testConfigFilePath);
        }
        
        public System.Threading.Tasks.Task StartTestingAsync(string testConfigFilePath) {
            return base.Channel.StartTestingAsync(testConfigFilePath);
        }
        
        public bool GetTestingStatus(ulong testingProcessID) {
            return base.Channel.GetTestingStatus(testingProcessID);
        }
        
        public System.Threading.Tasks.Task<bool> GetTestingStatusAsync(ulong testingProcessID) {
            return base.Channel.GetTestingStatusAsync(testingProcessID);
        }
        
        public string GetProcessOutput(ulong testingProcessID) {
            return base.Channel.GetProcessOutput(testingProcessID);
        }
        
        public System.Threading.Tasks.Task<string> GetProcessOutputAsync(ulong testingProcessID) {
            return base.Channel.GetProcessOutputAsync(testingProcessID);
        }
        
        public string GetProcessError(ulong testingProcessID) {
            return base.Channel.GetProcessError(testingProcessID);
        }
        
        public System.Threading.Tasks.Task<string> GetProcessErrorAsync(ulong testingProcessID) {
            return base.Channel.GetProcessErrorAsync(testingProcessID);
        }
        
        public string GetProcessConfigFilePath(ulong testingProcessID) {
            return base.Channel.GetProcessConfigFilePath(testingProcessID);
        }
        
        public System.Threading.Tasks.Task<string> GetProcessConfigFilePathAsync(ulong testingProcessID) {
            return base.Channel.GetProcessConfigFilePathAsync(testingProcessID);
        }
        
        public string[] GetAllConfigFilePaths() {
            return base.Channel.GetAllConfigFilePaths();
        }
        
        public System.Threading.Tasks.Task<string[]> GetAllConfigFilePathsAsync() {
            return base.Channel.GetAllConfigFilePathsAsync();
        }
    }
}
