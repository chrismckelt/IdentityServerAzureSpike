using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Helpers;
using IdentityServerAzureSpike.Shared;
using IdentityServerAzureSpike.SiteB;
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
                ClientId = Shared.Constants.Sites.B.Name,
                // must match IdentityServerAzureSpike.SelfHostedIdentityServerWebApi.Config.Clients
                Authority = Shared.Constants.IdentityServerCoreUri,
                RedirectUri = Shared.Constants.Sites.B.RedirectUri,
                PostLogoutRedirectUri = Shared.Constants.Sites.B.RedirectUri,
                ResponseType = Constants.ResponseTypes.CodeIdTokenToken,
                Scope = Shared.Constants.Scopes.Full,
                SignInAsAuthenticationType = Shared.Constants.Cookie.AuthenticationType,
                Notifications = Util.SetOpenIdConnectAuthenticationNotifications(Shared.Constants.Sites.B.Name)
            });
        }
    }
}