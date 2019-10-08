using Sitecore.Owin.Authentication.Services;
using System.Security.Claims;

namespace ADFS
{
    public class PrefixRoles : Transformation
    {
        public override void Transform(ClaimsIdentity identity, TransformationContext context)
        {
            foreach (Claim claim in identity.FindAll("role"))
            {
                identity.RemoveClaim(claim);
                identity.AddClaim(new Claim("role", $"Sitecore\\{claim.Value}"));
            }
        }
    }
}
