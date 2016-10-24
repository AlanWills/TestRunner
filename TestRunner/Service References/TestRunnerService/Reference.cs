﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This code was auto-generated by Microsoft.VisualStudio.ServiceReference.Platforms, version 14.0.23107.0
// 
namespace TestRunner.TestRunnerService {
    using System.Runtime.Serialization;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="TestingStatus", Namespace="http://schemas.datacontract.org/2004/07/TestRunnerService")]
    public enum TestingStatus : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        kRunning = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        kFinished = 1,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="TestRunnerService.ITestRunnerService")]
    public interface ITestRunnerService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITestRunnerService/StartTesting", ReplyAction="http://tempuri.org/ITestRunnerService/StartTestingResponse")]
        System.Threading.Tasks.Task StartTestingAsync(string testConfigFilePath);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITestRunnerService/GetTestingStatus", ReplyAction="http://tempuri.org/ITestRunnerService/GetTestingStatusResponse")]
        System.Threading.Tasks.Task<TestRunner.TestRunnerService.TestingStatus> GetTestingStatusAsync(ulong testingRunID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITestRunnerService/GetProcessOutput", ReplyAction="http://tempuri.org/ITestRunnerService/GetProcessOutputResponse")]
        System.Threading.Tasks.Task<string> GetProcessOutputAsync(ulong testingRunID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITestRunnerService/GetProcessError", ReplyAction="http://tempuri.org/ITestRunnerService/GetProcessErrorResponse")]
        System.Threading.Tasks.Task<string> GetProcessErrorAsync(ulong testingRunID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITestRunnerService/GetAllProcesses", ReplyAction="http://tempuri.org/ITestRunnerService/GetAllProcessesResponse")]
        System.Threading.Tasks.Task<System.Collections.ObjectModel.ObservableCollection<string>> GetAllProcessesAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ITestRunnerServiceChannel : TestRunner.TestRunnerService.ITestRunnerService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class TestRunnerServiceClient : System.ServiceModel.ClientBase<TestRunner.TestRunnerService.ITestRunnerService>, TestRunner.TestRunnerService.ITestRunnerService {
        
        /// <summary>
        /// Implement this partial method to configure the service endpoint.
        /// </summary>
        /// <param name="serviceEndpoint">The endpoint to configure</param>
        /// <param name="clientCredentials">The client credentials</param>
        static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public TestRunnerServiceClient() : 
                base(TestRunnerServiceClient.GetDefaultBinding(), TestRunnerServiceClient.GetDefaultEndpointAddress()) {
            this.Endpoint.Name = EndpointConfiguration.BasicHttpBinding_ITestRunnerService.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public TestRunnerServiceClient(EndpointConfiguration endpointConfiguration) : 
                base(TestRunnerServiceClient.GetBindingForEndpoint(endpointConfiguration), TestRunnerServiceClient.GetEndpointAddress(endpointConfiguration)) {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public TestRunnerServiceClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(TestRunnerServiceClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress)) {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public TestRunnerServiceClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(TestRunnerServiceClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress) {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public TestRunnerServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Threading.Tasks.Task StartTestingAsync(string testConfigFilePath) {
            return base.Channel.StartTestingAsync(testConfigFilePath);
        }
        
        public System.Threading.Tasks.Task<TestRunner.TestRunnerService.TestingStatus> GetTestingStatusAsync(ulong testingRunID) {
            return base.Channel.GetTestingStatusAsync(testingRunID);
        }
        
        public System.Threading.Tasks.Task<string> GetProcessOutputAsync(ulong testingRunID) {
            return base.Channel.GetProcessOutputAsync(testingRunID);
        }
        
        public System.Threading.Tasks.Task<string> GetProcessErrorAsync(ulong testingRunID) {
            return base.Channel.GetProcessErrorAsync(testingRunID);
        }
        
        public System.Threading.Tasks.Task<System.Collections.ObjectModel.ObservableCollection<string>> GetAllProcessesAsync() {
            return base.Channel.GetAllProcessesAsync();
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync() {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        public virtual System.Threading.Tasks.Task CloseAsync() {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration) {
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_ITestRunnerService)) {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                return result;
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration) {
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_ITestRunnerService)) {
                return new System.ServiceModel.EndpointAddress("http://localhost:8733/Design_Time_Addresses/TestRunnerService/");
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.Channels.Binding GetDefaultBinding() {
            return TestRunnerServiceClient.GetBindingForEndpoint(EndpointConfiguration.BasicHttpBinding_ITestRunnerService);
        }
        
        private static System.ServiceModel.EndpointAddress GetDefaultEndpointAddress() {
            return TestRunnerServiceClient.GetEndpointAddress(EndpointConfiguration.BasicHttpBinding_ITestRunnerService);
        }
        
        public enum EndpointConfiguration {
            
            BasicHttpBinding_ITestRunnerService,
        }
    }
}
