using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Web.Helpers;
using IdentityServerAzureSpike.SiteD;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Thinktecture.IdentityServer.Core;

[assembly: OwinStartup(typeof (Startup))]

namespace IdentityServerAzureSpike.SiteD
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
            app.UseKentorOwinCookieSaver();

            app.UseCookieAuthentication(Shared.Constants.Cookie.Build());

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "TempState",
                AuthenticationMode = AuthenticationMode.Passive
            });
        }
    }
}