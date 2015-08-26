# WCF-WsHttpBinding
WCF service WsHttpBinding example with description

WsHttpBinding

I. Create Certificate

makecert -pe -r -n "CN=MyWebSite1" -sky exchange -b 01/01/2014 -e 01/01/2050 -sr localMachine -ss My

II. Create wcf service

1. Create WCF service application
2. Add reference System.IdentityModel, System.IndentityModel.Selectors
3. Create Custome Username password validator 

Ex:

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IdentityModel;
using System.IdentityModel.Selectors;
using System.ServiceModel;

namespace WcfServiceAuth.Authorize.Service
{
    public class CustomUsernamePasswordValidator : UserNamePasswordValidator
    {
        public override void Validate(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                throw new ArgumentNullException();
            if (string.Equals(userName, "test") && string.Equals(password, "test"))
            {                
                return;
            }
            else
                throw new FaultException("Invalid username and password");
        }
    }
}


4. Change webconfig file

<system.serviceModel>

    <bindings>
      <wsHttpBinding>
        <binding name="custom_binding">
          <security mode="Message">
            <message clientCredentialType="UserName"/>
          </security>
        </binding>
      </wsHttpBinding>
    </bindings>

    <behaviors>
      <serviceBehaviors>
        <behavior name="custom_behavior">
          <!-- To avoid disclosing metadata information, set the values below to false before deployment -->
          <serviceMetadata httpGetEnabled="true" httpsGetEnabled="true"/>
          <!-- To receive exception details in faults for debugging purposes, set the value below to true.  Set to false before deployment to avoid disclosing exception information -->
          <serviceDebug includeExceptionDetailInFaults="false"/>

          <serviceCredentials>
            <serviceCertificate findValue="MyWebSite1"
                                storeLocation="LocalMachine"
                                storeName="My"
                                x509FindType="FindBySubjectName"/>
            <userNameAuthentication userNamePasswordValidationMode="Custom"
                                    customUserNamePasswordValidatorType="WcfServiceAuth.Authorize.Service.CustomUsernamePasswordValidator, WcfServiceAuth.Authorize.Service"/>
          </serviceCredentials>
        </behavior>
      </serviceBehaviors>
    </behaviors>

    <services>
      <service name="WcfServiceAuth.Authorize.Service.Service1" behaviorConfiguration="custom_behavior">
        <endpoint address="" binding="wsHttpBinding" bindingConfiguration="custom_binding" contract="WcfServiceAuth.Authorize.Service.IService1"></endpoint>
        <endpoint address="mex" contract="IMetadataExchange" binding="mexHttpBinding" />
      </service>
    </services>

  </system.serviceModel>


4. Run the service.

III. Consume Wcf service to application

1. Open visual studio command prompet, and extract service url using svcutil.exe 

Ex : svcutil.exe url

2. Create console application
3. Modify config file

<system.serviceModel>
    <bindings>
      <wsHttpBinding>
        <binding name="WSHttpBinding_IService1">
          <security>
            <message clientCredentialType="UserName" />
          </security>
        </binding>
      </wsHttpBinding>
    </bindings>
    <client>
      <endpoint address="http://localhost:51032/Service1.svc" binding="wsHttpBinding"
          bindingConfiguration="WSHttpBinding_IService1" contract="IService1"
          name="WSHttpBinding_IService1">
        <identity>
          <certificate encodedValue="AwAAAAEAAAAUAAAARdvaUze49OJpSqie2wqE6Y5y+ZTU1I=" />
        </identity>
      </endpoint>
    </client>
  </system.serviceModel>

4. Add reference System.ServiceModel, System.IndentityModel
5. Consume service and pass user credentital like the following code.

Ex:

 var factory = new ChannelFactory<IService1>("*");
            factory.Credentials.UserName.UserName = "test";
            factory.Credentials.UserName.Password = "test";
            factory.Credentials.ServiceCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.None;
            var channel = factory.CreateChannel();
            var s = channel.GetData(10);
            Console.Write(s);
            Console.Read();


IV. Create custom Autorization policy and Custom Principal in Wcf 

1. Add reference System.IdentityModel, System.Security
2. Create custome Authorization policy class and Custom principal class

Ex : CustomAuthorizationPolicy

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;
using System.Security.Principal;

namespace WcfServiceAuth.Authorize.Service
{
    public class CustomAuthorizationProlicy : IAuthorizationPolicy
    {
        private Guid _id = new Guid();
        public bool Evaluate(EvaluationContext evaluationContext, ref object state)
        {
            IIdentity identity = GetClientIdentity(evaluationContext);
            evaluationContext.Properties["Principal"] = new CustomPrincipal(identity);
            return true;
        }

        private IIdentity GetClientIdentity(EvaluationContext evaluationContext)
        {
            object obj;
            if (!evaluationContext.Properties.TryGetValue("Identities", out obj))
                throw new Exception("No Identity found");

            IList<IIdentity> identities = obj as IList<IIdentity>;
            if (identities == null || identities.Count <= 0)
                throw new Exception("No Identity found");

            return identities[0];
        }

        public ClaimSet Issuer
        {
            get { return ClaimSet.System; }
        }

        public string Id
        {
            get { return _id.ToString(); }
        }
    }
}

Ex: Custom Principal

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security;
using System.Security.Principal;

namespace WcfServiceAuth.Authorize.Service
{
    public class CustomPrincipal : IPrincipal
    {
        private IIdentity _identity;

        public CustomPrincipal(IIdentity identity)
        {
            _identity = new CustomIdentity(identity);
        }

        public IIdentity Identity
        {
            get { return _identity; }
        }

        public bool IsInRole(string role)
        {
            return ((CustomIdentity)_identity).Role.Equals(role);
        }
    }

    public class CustomIdentity : IIdentity
    {
        public CustomIdentity(IIdentity identity)
        {
            this.AuthenticationType = identity.AuthenticationType;
            this.IsAuthenticated = identity.IsAuthenticated;
            this.Name = identity.Name;
            this.Role = "admin";
        }
        public string AuthenticationType
        {
            get;
            set;
        }

        public bool IsAuthenticated
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string Role
        {
            get;
            set;
        }
    }
}

3. Need to configure authorization type to webconfig behavior section.

Ex: 

<behaviors>
      <serviceBehaviors>
        <behavior name="custom_behavior">
          <!-- To avoid disclosing metadata information, set the values below to false before deployment -->
          <serviceMetadata httpGetEnabled="true" httpsGetEnabled="true"/>
          <!-- To receive exception details in faults for debugging purposes, set the value below to true.  Set to false before deployment to avoid disclosing exception information -->
          <serviceDebug includeExceptionDetailInFaults="false"/>

          <serviceAuthorization principalPermissionMode="Custom">
            <authorizationPolicies>
              <add policyType="WcfServiceAuth.Authorize.Service.CustomAuthorizationProlicy, WcfServiceAuth.Authorize.Service"/>
            </authorizationPolicies>
          </serviceAuthorization>

          <serviceCredentials>
            <serviceCertificate findValue="MyWebSite1"
                                storeLocation="LocalMachine"
                                storeName="My"
                                x509FindType="FindBySubjectName"/>
            <userNameAuthentication userNamePasswordValidationMode="Custom"
                                    customUserNamePasswordValidatorType="WcfServiceAuth.Authorize.Service.CustomUsernamePasswordValidator, WcfServiceAuth.Authorize.Service"/>
          </serviceCredentials>
        </behavior>
      </serviceBehaviors>
    </behaviors>

4. Authorization check in service method

Ex:

using System.Security.Permissions;


namespace WcfServiceAuth.Authorize.Service
{    
    public class Service1 : IService1
    {
        [PrincipalPermission(SecurityAction.Demand, Role = "admin")]
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }
    }
}

