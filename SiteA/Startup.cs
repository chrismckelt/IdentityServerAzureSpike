using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using System.Web.Helpers;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Resources;
using IdentityServerAzureSpike.Shared;
using IdentityServerAzureSpike.SiteA;
using Microsoft.IdentityModel.Protocols;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;
using Thinktecture.IdentityModel.Client;

[assembly: OwinStartup(typeof(Startup))]

namespace IdentityServerAzureSpike.SiteA
{
    public class Startup
    {
         public void Configuration(IAppBuilder app)
        {
            AntiForgeryConfig.UniqueClaimTypeIdentifier = IdentityServer3.Core.Constants.ClaimTypes.Subject;
            JwtSecurityTokenHandler.InboundClaimTypeMap = new Dictionary<string, string>();

            app.UseCookieAuthentication(new CookieAuthenticationOptions
                {
                    AuthenticationType = "Cookies"
                });

            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
                {
                    ClientId = "SiteA",
                    Authority = Constants.IdentityServerUri,
                    RedirectUri = Constants.SiteAUri,
                    PostLogoutRedirectUri = Constants.SiteAUri + "/logout",
                    ResponseType = "code id_token token",
                    Scope = "openid email profile read write offline_access",

                    SignInAsAuthenticationType = "Cookies",
                    UseTokenLifetime = false,

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
                                    new Uri(Constants.UserInfoEndpoint),
                                    n.ProtocolMessage.AccessToken);

                                var userInfo = await userInfoClient.GetAsync();
                                userInfo.Claims.ToList().ForEach(ui => claims.Add(new Claim(ui.Item1, ui.Item2)));

                                // get access and refresh token
                                var tokenClient = new OAuth2Client(
                                    new Uri(Constants.TokenEndpoint),
                                    Constants.SiteAUri,
                                    Constants.Secret);

                                var response = await tokenClient.RequestAuthorizationCodeAsync(n.Code, n.RedirectUri);

                                claims.Add(new Claim("access_token", response.AccessToken));
                                claims.Add(new Claim("expires_at", DateTime.Now.AddSeconds(response.ExpiresIn).ToLocalTime().ToString()));
                                claims.Add(new Claim("refresh_token", response.RefreshToken));
                                claims.Add(new Claim("id_token", n.ProtocolMessage.IdToken));

                                //n.AuthenticationTicket = new AuthenticationTicket(new ClaimsIdentity(claims.Distinct(new ClaimComparer()), n.AuthenticationTicket.Identity.AuthenticationType), n.AuthenticationTicket.Properties);
                                n.AuthenticationTicket = new AuthenticationTicket(new ClaimsIdentity(claims.Distinct(), n.AuthenticationTicket.Identity.AuthenticationType), n.AuthenticationTicket.Properties);
                            },

                        RedirectToIdentityProvider = async n =>
                            {
                                // if signing out, add the id_token_hint
                                if (n.ProtocolMessage.RequestType == OpenIdConnectRequestType.LogoutRequest)
                                {
                                    var idTokenHint = n.OwinContext.Authentication.User.FindFirst("id_token").Value;
                                    n.ProtocolMessage.IdTokenHint = idTokenHint;
                                }
                            },
                    }
                });
        }
    }
    }
