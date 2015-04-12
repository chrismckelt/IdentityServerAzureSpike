using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Web.Helpers;
using IdentityServerAzureSpike.SiteC;
using Microsoft.Owin;
using Owin;
using Thinktecture.IdentityServer.Core;

[assembly: OwinStartup(typeof(Startup))]

namespace IdentityServerAzureSpike.SiteC
{
    public class Startup
    {
         public void Configuration(IAppBuilder app)
        {
            AntiForgeryConfig.UniqueClaimTypeIdentifier = Constants.ClaimTypes.ClientId;
            JwtSecurityTokenHandler.InboundClaimTypeMap = new Dictionary<string, string>();

            
        }
    }

}
