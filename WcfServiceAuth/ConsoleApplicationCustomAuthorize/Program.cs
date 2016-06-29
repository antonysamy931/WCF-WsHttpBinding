using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Security;

namespace ConsoleApplicationCustomAuthorize
{
    class Program
    {
        static void Main(string[] args)
        {
            //Service1Client client = new Service1Client();
            //client.ClientCredentials.UserName.UserName = "test";
            //client.ClientCredentials.UserName.Password = "test";
            //client.ClientCredentials.ServiceCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.None;
            //var s = client.GetData(10);

            var factory = new ChannelFactory<IService1>("*");
            factory.Credentials.UserName.UserName = "test";
            factory.Credentials.UserName.Password = "test";
            factory.Credentials.ServiceCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.None;
            var channel = factory.CreateChannel();
            var s = channel.GetData(10);

            var sa = channel.GetData(20);
            Console.Write(s);
            Console.Read();
        }
    }
}
