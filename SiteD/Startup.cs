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

            app.UseCookieAuthentication(Shared.Constants.Cookie.BuildActive());

            app.UseCookieAuthentication(Shared.Constants.Cookie.BuildPassive());
        }
    }
}