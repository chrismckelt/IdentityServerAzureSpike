using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Web.Helpers;
using IdentityServerAzureSpike.SiteE;
using Microsoft.Owin;
using Owin;
using Thinktecture.IdentityServer.Core;

[assembly: OwinStartup(typeof (Startup))]

namespace IdentityServerAzureSpike.SiteE
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            AntiForgeryConfig.UniqueClaimTypeIdentifier = Constants.ClaimTypes.ClientId;
            JwtSecurityTokenHandler.InboundClaimTypeMap = new Dictionary<string, string>();

            app.UseKentorOwinCookieSaver();

            app.UseCookieAuthentication(Shared.Constants.Cookie.BuildActive());

            app.UseCookieAuthentication(Shared.Constants.Cookie.BuildPassive());

        }
    }
}