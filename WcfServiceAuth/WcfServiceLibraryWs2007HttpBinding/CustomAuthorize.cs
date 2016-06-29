using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;
using System.Security.Principal;

namespace WcfServiceLibraryWs2007HttpBinding
{
    public class CustomAuthorize : IAuthorizationPolicy
    {
        Guid _id = new Guid();
        public bool Evaluate(EvaluationContext evaluationContext, ref object state)
        {
            object obj;
            if (!evaluationContext.Properties.TryGetValue("Identities", out obj))
                throw new Exception("No Identity Found");
            IList<IIdentity> identity = obj as IList<IIdentity>;
            if (identity == null || identity.Count <= 0)
                throw new Exception("No Indentity Found");
            IIdentity Identity = identity[0];
            evaluationContext.Properties["Principal"] = new GenericPrincipal(Identity, new string[] { "admin", "user" });
            return true;
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