using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Sitecore.Diagnostics;
using Sitecore.Owin.Authentication.Configuration;
using Sitecore.Owin.Authentication.Identity;
using Sitecore.Owin.Authentication.Services;
using Sitecore.SecurityModel.Cryptography;

namespace ADFS
{
    public class AdfsExternalUserBuilder : DefaultExternalUserBuilder
    {
        public AdfsExternalUserBuilder(ApplicationUserFactory applicationUserFactory, IHashEncryption hashEncryption) : base(applicationUserFactory, hashEncryption)
        {
        }

        public override ApplicationUser BuildUser(UserManager<ApplicationUser> userManager, ExternalLoginInfo externalLoginInfo)
        {
            ApplicationUser user = this.ApplicationUserFactory.CreateUser(this.CreateUniqueUserName(userManager, externalLoginInfo));
            user.IsVirtual = !this.IsPersistentUser;
            return user;
        }

        protected override string CreateUniqueUserName(UserManager<ApplicationUser> userManager, ExternalLoginInfo externalLoginInfo)
        {
            Assert.ArgumentNotNull(userManager, nameof(userManager));
            Assert.ArgumentNotNull(externalLoginInfo, nameof(externalLoginInfo));
            IdentityProvider identityProvider = this.FederatedAuthenticationConfiguration.GetIdentityProvider(externalLoginInfo.ExternalIdentity);
            if (identityProvider == null)
            {
                throw new InvalidOperationException("Unable to retrieve an identity provider for the given identity");
            }

            Claim upnClaim = externalLoginInfo.ExternalIdentity.Claims.FirstOrDefault(x => x.Type == "upn");
            return upnClaim != null ? $"{identityProvider.Domain}\\{upnClaim.Value}" : base.CreateUniqueUserName(userManager, externalLoginInfo);
        }
    }
}
