using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IdentityModel;
using System.IdentityModel.Selectors;
using System.ServiceModel;

namespace WcfServiceAuth.Service.App_Code
{
    public class CustomValidator : UserNamePasswordValidator
    {
        public override void Validate(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                throw new ArgumentNullException("Username & Password");
            if (userName == "test" && password == "test")
                return;
            else
                throw new FaultException("Username & Password incorrect");
        }
    }
}