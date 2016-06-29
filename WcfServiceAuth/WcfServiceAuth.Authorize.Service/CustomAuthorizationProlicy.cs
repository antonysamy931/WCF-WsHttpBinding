using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;
using System.Security.Principal;
using System.IdentityModel.Metadata;

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