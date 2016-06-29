using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security;
using System.Security.Principal;
using System.Security.Claims;

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