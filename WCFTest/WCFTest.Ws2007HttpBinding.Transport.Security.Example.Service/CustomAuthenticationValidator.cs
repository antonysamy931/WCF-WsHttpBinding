using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IdentityModel;
using System.IdentityModel.Selectors;
using System.ServiceModel.Security;

namespace WCFTest.Ws2007HttpBinding.Transport.Security.Example.Service
{
    public class CustomAuthenticationValidator : UserNamePasswordValidator
    {
        public override void Validate(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                throw new ArgumentNullException();

            if (userName == "test" && password == "test")
                return;
            throw new System.IdentityModel.Tokens.SecurityTokenException("Unknow Username or password");
        }
    }
}