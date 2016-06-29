using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using ConsoleApplication1.ServiceReferenceWcf;
using System.ServiceModel.Security;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Service1Client client = new Service1Client();
            client.ClientCredentials.UserName.UserName = "test";
            client.ClientCredentials.UserName.Password = "test";
            client.ClientCredentials.ServiceCertificate.Authentication.CertificateValidationMode 
                = X509CertificateValidationMode.None;
            var s = client.GetData(1);

            var baseaddress = "http://localhost:49525/Service1.svc";
            var servicepoint = new EndpointAddress(new Uri(baseaddress), 
                EndpointIdentity.CreateDnsIdentity("MyWebSite"));
            var binding = new WSHttpBinding();
            binding.Security.Mode = SecurityMode.Message;
            binding.Security.Message.ClientCredentialType = MessageCredentialType.UserName;
            var result = new Service1Client(binding, servicepoint);
            result.ClientCredentials.UserName.UserName = "test";
            result.ClientCredentials.UserName.Password = "test";
            result.ClientCredentials.ServiceCertificate.Authentication.CertificateValidationMode 
                = X509CertificateValidationMode.None;

            var s1 = result.GetData(3);

        }
    }
}
