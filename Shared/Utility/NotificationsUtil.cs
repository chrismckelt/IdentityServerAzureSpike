using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.IdentityModel.Protocols;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Notifications;
using Microsoft.Owin.Security.OpenIdConnect;
using Serilog;
using Thinktecture.IdentityModel.Client;

namespace IdentityServerAzureSpike.Shared.Utility
{
    public static class Util
    {
        public static OpenIdConnectAuthenticationNotifications SetOpenIdConnectAuthenticationNotifications(string siteName)
        {
            return new OpenIdConnectAuthenticationNotifications
            {
                AuthorizationCodeReceived = async n =>
                {
                    Log.Information("Authorization Code Received");
                    await SetAuthorizationCodeReceivedAuthenticationTicket(n, siteName);
                },
                SecurityTokenValidated = async n =>
                {
                    Log.Information("SecurityTokenValidated");
                    await SetSecurityTokenValidatedAuthecticationTicket(n);
                },
                MessageReceived = async msg =>
                {
                    Log.Information("MessageReceived");
                    //Log.Debug("Message Received => {@AuthorizationCodeReceivedNotification} ", msg);
                },
                SecurityTokenReceived = async notification =>
                {
                    Log.Information("SecurityTokenReceived");
                    //Log.Debug("Security Token Received =>", notification);
                },
                //RedirectToIdentityProvider = (context) =>
                //{
                //    // This ensures that the address used for sign in and sign out is picked up dynamically from the request
                //    // this allows you to deploy your app (to Azure Web Sites, for example)without having to change settings
                //    // Remember that the base URL of the address used here must be provisioned in Azure AD beforehand.
                //    string appBaseUrl = context.Request.Scheme + "://" + context.Request.Host + context.Request.PathBase;
                //    context.ProtocolMessage.RedirectUri = appBaseUrl;
                //    //This will need changing to the web site home page once it is live
                //    context.ProtocolMessage.PostLogoutRedirectUri = DemoSites.Instance.E.Uri;
                //    return Task.FromResult(0);
                //},
            };
        }

        public static async Task SetAuthorizationCodeReceivedAuthenticationTicket(AuthorizationCodeReceivedNotification n, string siteName)
        {
            var identity = await CreateClaimsIdentity(n.AuthenticationTicket, n.ProtocolMessage.AccessToken);

            // get access and refresh token
            var tokenClient = new OAuth2Client(new Uri(Constants.TokenEndpoint), siteName,Constants.Secret);

            var response = await tokenClient.RequestAuthorizationCodeAsync(n.Code, n.RedirectUri);
            if (response != null)
            {
                identity.AddClaim(new Claim("expires_at", DateTime.Now.AddSeconds(response.ExpiresIn).ToLocalTime().ToString()));
                identity.AddClaim(new Claim("refresh_token", response.RefreshToken));
                identity.AddClaim(new Claim("id_token", response.IdentityToken));
            }
            n.AuthenticationTicket = new AuthenticationTicket(identity, n.AuthenticationTicket.Properties);
            
        }

        public static async Task SetSecurityTokenValidatedAuthecticationTicket(SecurityTokenValidatedNotification<OpenIdConnectMessage, OpenIdConnectAuthenticationOptions> n)
        {
            var identity = await CreateClaimsIdentity(n.AuthenticationTicket, n.ProtocolMessage.AccessToken);
            n.AuthenticationTicket = new AuthenticationTicket(identity, n.AuthenticationTicket.Properties);

        }

        private static async Task<ClaimsIdentity> CreateClaimsIdentity(AuthenticationTicket ticket, string token)
        {

            var id = ticket.Identity;

            // create new identity and set name and role claim type
            var nid = new ClaimsIdentity(
                id.AuthenticationType,
                Thinktecture.IdentityServer.Core.Constants.ClaimTypes.GivenName,
                Thinktecture.IdentityServer.Core.Constants.ClaimTypes.Role);


            // filter "protocol" claims
            var claims = new List<Claim>(from c in ticket.Identity.Claims
                where c.Type != "iss" &&
                      c.Type != "aud" &&
                      c.Type != "nbf" &&
                      c.Type != "exp" &&
                      c.Type != "iat" &&
                      c.Type != "nonce" &&
                      c.Type != "c_hash" &&
                      c.Type != "at_hash"
                select c);

            var userInfoClient = new UserInfoClient(new Uri(Constants.UserInfoEndpoint), token);
            var userInfo = await userInfoClient.GetAsync();
            userInfo.Claims.Distinct().ToList().ForEach(ui => claims.Add(new Claim(ui.Item1, ui.Item2)));
            nid.AddClaims(claims.Distinct());
            nid.AddClaim(new Claim("access_token", token));
            HttpContext.Current.Session["access_token"] = token;
            return nid;
        }
    }
}