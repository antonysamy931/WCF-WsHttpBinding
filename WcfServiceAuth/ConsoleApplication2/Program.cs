using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Security;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            Service1Client client = new Service1Client();
            client.ClientCredentials.UserName.UserName = "test";
            client.ClientCredentials.UserName.Password = "test";
            client.ClientCredentials.ServiceCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.None;
            var s = client.GetData(30);
        }
    }
}
