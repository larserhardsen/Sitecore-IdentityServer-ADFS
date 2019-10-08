using System.Diagnostics;
using Sitecore.Diagnostics;
using Sitecore.Owin.Authentication.Services;
using System.Linq;
using System.Security.Claims;

namespace ADFS
{
    public class Debug : Transformation
    {
        public override void Transform(ClaimsIdentity identity, TransformationContext context)
        {
            Assert.ArgumentNotNull((object)identity, nameof(identity));

            Debugger.Launch();

            foreach (Claim claim in identity.FindAll("http://schemas.microsoft.com/ws/2008/06/identity/claims/role").ToList<Claim>())
            {
                string s = claim.Value;
            }
        }
    }
}
