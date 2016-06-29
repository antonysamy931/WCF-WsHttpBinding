﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ConsoleApplicationWcfTransportClient.ServiceReferenceTransport {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReferenceTransport.ISecureService")]
    public interface ISecureService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISecureService/SecureWork", ReplyAction="http://tempuri.org/ISecureService/SecureWorkResponse")]
        string SecureWork();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISecureService/SecureWork", ReplyAction="http://tempuri.org/ISecureService/SecureWorkResponse")]
        System.Threading.Tasks.Task<string> SecureWorkAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ISecureServiceChannel : ConsoleApplicationWcfTransportClient.ServiceReferenceTransport.ISecureService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SecureServiceClient : System.ServiceModel.ClientBase<ConsoleApplicationWcfTransportClient.ServiceReferenceTransport.ISecureService>, ConsoleApplicationWcfTransportClient.ServiceReferenceTransport.ISecureService {
        
        public SecureServiceClient() {
        }
        
        public SecureServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public SecureServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SecureServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SecureServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string SecureWork() {
            return base.Channel.SecureWork();
        }
        
        public System.Threading.Tasks.Task<string> SecureWorkAsync() {
            return base.Channel.SecureWorkAsync();
        }
    }
}
