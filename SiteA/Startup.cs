using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Web.Helpers;
using IdentityServerAzureSpike.Shared.Config;
using IdentityServerAzureSpike.Shared.Utility;
using IdentityServerAzureSpike.SiteA;
using Microsoft.Owin;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;
using Thinktecture.IdentityServer.Core;

[assembly: OwinStartup(typeof (Startup))]

namespace IdentityServerAzureSpike.SiteA
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            AntiForgeryConfig.UniqueClaimTypeIdentifier = Constants.ClaimTypes.ClientId;
            JwtSecurityTokenHandler.InboundClaimTypeMap = new Dictionary<string, string>();

            //https://github.com/KentorIT/owin-cookie-saver 
            app.UseKentorOwinCookieSaver();
            
            app.UseCookieAuthentication(Shared.Constants.Cookie.BuildActive());

            app.UseCookieAuthentication(Shared.Constants.Cookie.BuildPassive());

            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
            {
                // must match IdentityServerAzureSpike.SelfHostedIdentityServerWebApi.Config.Clients
                ClientId = DemoSites.Instance.A.Name,
                Authority = Shared.Constants.IdentityServerCoreUri,
                RedirectUri = DemoSites.Instance.A.HybridUri,
                PostLogoutRedirectUri = DemoSites.Instance.A.HybridUri,
                ResponseType = Constants.ResponseTypes.CodeIdTokenToken,
                Scope = Shared.Constants.Scopes.Full,
                SignInAsAuthenticationType = Shared.Constants.Cookie.AuthenticationType,
                Notifications = Util.SetOpenIdConnectAuthenticationNotifications(DemoSites.Instance.A.Name),
            });
        }

        
    }
}