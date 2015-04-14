using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Helpers;
using IdentityServerAzureSpike.Shared;
using IdentityServerAzureSpike.SiteA;
using Microsoft.IdentityModel.Protocols;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Notifications;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;
using Serilog;
using Thinktecture.IdentityModel.Client;
using Constants = Thinktecture.IdentityServer.Core.Constants;

[assembly: OwinStartup(typeof (Startup))]

namespace IdentityServerAzureSpike.SiteA
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            AntiForgeryConfig.UniqueClaimTypeIdentifier = Constants.ClaimTypes.ClientId;
            JwtSecurityTokenHandler.InboundClaimTypeMap = new Dictionary<string, string>();

            app.UseKentorOwinCookieSaver();

            app.UseCookieAuthentication(Shared.Constants.Cookie.Build());

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = Shared.Constants.Cookie.TempPassiveStateName,
                AuthenticationMode = AuthenticationMode.Passive
            });

            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
            {
                ClientId = Shared.Constants.Sites.A.Name,
                // must match IdentityServerAzureSpike.SelfHostedIdentityServerWebApi.Config.Clients
                Authority = Shared.Constants.IdentityServerCoreUri,
                RedirectUri = Shared. Constants.Sites.A.RedirectUri,
                ResponseType = Constants.ResponseTypes.CodeIdTokenToken,
                Scope = Shared.Constants.Scopes.Full,
                SignInAsAuthenticationType = "Cookies",
                Notifications = Util.SetOpenIdConnectAuthenticationNotifications()
            });
        }

        
    }
}