using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Security;

namespace ConsoleApplicationWs2007BindingWithTransport
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ChannelFactory<ServiceReferenceTransport.IService1>("*");
            factory.Credentials.UserName.UserName = "test";
            factory.Credentials.UserName.Password = "test";

            factory.Credentials.ServiceCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.None;

            var chennal = factory.CreateChannel();
            var s = chennal.GetData(10);
        }
    }
}
