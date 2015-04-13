using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using System.Web.Helpers;
using IdentityServerAzureSpike.SiteD;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;
using Thinktecture.IdentityModel.Client;

[assembly: OwinStartup(typeof(Startup))]

namespace IdentityServerAzureSpike.SiteD
{
    public class Startup
    {
         public void Configuration(IAppBuilder app)
        {
            AntiForgeryConfig.UniqueClaimTypeIdentifier = Thinktecture.IdentityServer.Core.Constants.ClaimTypes.ClientId;
            JwtSecurityTokenHandler.InboundClaimTypeMap = new Dictionary<string, string>();

            app.UseCookieAuthentication(Shared.Constants.Cookie.Build());

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "TempState",
                AuthenticationMode = AuthenticationMode.Passive
            });

             app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
                {
                    
                    ClientId = Shared.Constants.SiteDCodeFlow, // must match IdentityServerAzureSpike.SelfHostedIdentityServerWebApi.Config.Clients
                    Authority = Shared.Constants.IdentityServerCoreUri,
                    RedirectUri = Shared.Constants.SiteDUri,
                    PostLogoutRedirectUri = Shared.Constants.SiteDUri,
                    ResponseType = Thinktecture.IdentityServer.Core.Constants.ResponseTypes.CodeIdToken,
                    Scope = Shared.Constants.Scopes.CodeFlow,
                    SignInAsAuthenticationType = "Cookies",
                    
                    Notifications = new OpenIdConnectAuthenticationNotifications
                    {
                        AuthorizationCodeReceived = async n =>
                        {
                            // filter "protocol" claims
                            var claims = new List<Claim>(from c in n.AuthenticationTicket.Identity.Claims
                                                         where c.Type != "iss" &&
                                                               c.Type != "aud" &&
                                                               c.Type != "nbf" &&
                                                               c.Type != "exp" &&
                                                               c.Type != "iat" &&
                                                               c.Type != "nonce" &&
                                                               c.Type != "c_hash" &&
                                                               c.Type != "at_hash"
                                                         select c);

                            // get userinfo data
                            var userInfoClient = new UserInfoClient(
                                new Uri(Shared.Constants.UserInfoEndpoint),
                                n.ProtocolMessage.AccessToken);

                            var userInfo = await userInfoClient.GetAsync();
                            userInfo.Claims.ToList().ForEach(ui => claims.Add(new Claim(ui.Item1, ui.Item2)));

                            // get access and refresh token
                            var tokenClient = new OAuth2Client(new Uri(Shared.Constants.TokenEndpoint), Shared.Constants.SiteDCodeFlow, Shared.Constants.Secret);

                            var response = await tokenClient.RequestAuthorizationCodeAsync(n.Code, n.RedirectUri);

                            claims.Add(new Claim("access_token", response.AccessToken));
                            claims.Add(new Claim("expires_at", DateTime.Now.AddSeconds(response.ExpiresIn).ToLocalTime().ToString()));
                            claims.Add(new Claim("refresh_token", response.RefreshToken));
                            claims.Add(new Claim("id_token", n.ProtocolMessage.IdToken));

                            n.AuthenticationTicket = new AuthenticationTicket(new ClaimsIdentity(claims.Distinct(), n.AuthenticationTicket.Identity.AuthenticationType), n.AuthenticationTicket.Properties);
                        },
                        SecurityTokenValidated = async n =>
                        {
                            var nid = new ClaimsIdentity(
                                n.AuthenticationTicket.Identity.AuthenticationType,
                                JwtClaimTypes.GivenName,
                                JwtClaimTypes.Role);

                            // get userinfo data
                            var userInfoClient = new UserInfoClient(
                                new Uri(n.Options.Authority + "/connect/userinfo"),
                                n.ProtocolMessage.AccessToken);

                            var userInfo = await userInfoClient.GetAsync();
                            userInfo.Claims.ToList().ForEach(ui => nid.AddClaim(new Claim(ui.Item1, ui.Item2)));

                            // keep the id_token for logout
                            nid.AddClaim(new Claim("id_token", n.ProtocolMessage.IdToken));

                            // add access token for sample API
                            nid.AddClaim(new Claim("access_token", n.ProtocolMessage.AccessToken));

                            // keep track of access token expiration
                            nid.AddClaim(new Claim("expires_at", DateTimeOffset.Now.AddSeconds(int.Parse(n.ProtocolMessage.ExpiresIn)).ToString()));

                            // add some other app specific claim
                            nid.AddClaim(new Claim("app_specific", "some data"));

                            n.AuthenticationTicket = new AuthenticationTicket(nid,n.AuthenticationTicket.Properties);
                        },
                        
                    }
                });
        }
    }

}
