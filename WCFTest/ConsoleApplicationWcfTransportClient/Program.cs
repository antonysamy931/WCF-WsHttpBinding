using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplicationWcfTransportClient
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceReferenceCertificate.SecureServiceClient client = new ServiceReferenceCertificate.SecureServiceClient();
            var message = client.DoWork();

            ServicePointManager.ServerCertificateValidationCallback =
          new RemoteCertificateValidationCallback(IgnoreCertificateErrorHandler);

            ServiceReferenceTransport.SecureServiceClient cli = new ServiceReferenceTransport.SecureServiceClient();
            cli.ClientCredentials.UserName.UserName = "test";
            cli.ClientCredentials.UserName.Password = "test";

            var s = cli.SecureWork();
        }

        public static bool IgnoreCertificateErrorHandler(object sender,
     X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
    }
}
