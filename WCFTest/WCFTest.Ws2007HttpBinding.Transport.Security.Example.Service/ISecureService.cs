
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCFTest.Ws2007HttpBinding.Transport.Security.Example.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISecureService" in both code and config file together.
    [ServiceContract]
    public interface ISecureService
    {
        [OperationContract]
        string SecureWork();
    }
}
