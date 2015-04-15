using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Web.Helpers;
using IdentityServerAzureSpike.Shared.Config;
using IdentityServerAzureSpike.Shared.Utility;
using IdentityServerAzureSpike.SiteB;
using Microsoft.Owin;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;
using Thinktecture.IdentityServer.Core;

[assembly: OwinStartup(typeof (Startup))]

namespace IdentityServerAzureSpike.SiteB
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

            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
            {
                ClientId = DemoSites.Instance.B.Name,
                // must match IdentityServerAzureSpike.SelfHostedIdentityServerWebApi.Config.Clients
                Authority = Shared.Constants.IdentityServerCoreUri,
                RedirectUri = DemoSites.Instance.B.RedirectUri,
                PostLogoutRedirectUri = DemoSites.Instance.B.RedirectUri,
                ResponseType = Constants.ResponseTypes.CodeIdTokenToken,
                Scope = Shared.Constants.Scopes.Full,
                SignInAsAuthenticationType = Shared.Constants.Cookie.AuthenticationType,
                Notifications = Util.SetOpenIdConnectAuthenticationNotifications(DemoSites.Instance.B.Name)
            });
        }
    }
}