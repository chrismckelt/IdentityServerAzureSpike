using IdentityServerAzureSpike.Shared;
using IdentityServerAzureSpike.SiteD;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartup(typeof (Startup))]

namespace IdentityServerAzureSpike.SiteD
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
            app.UseKentorOwinCookieSaver();

            app.UseCookieAuthentication(Constants.Cookie.Build());

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = Constants.Cookie.TempPassiveStateName,
                AuthenticationMode = AuthenticationMode.Passive
            });
        }
    }
}