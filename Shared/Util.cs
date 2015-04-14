using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Protocols;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Notifications;
using Microsoft.Owin.Security.OpenIdConnect;
using Serilog;
using Thinktecture.IdentityModel.Client;

namespace IdentityServerAzureSpike.Shared
{
    public static class Util
    {
        public static OpenIdConnectAuthenticationNotifications SetOpenIdConnectAuthenticationNotifications()
        {
            return new OpenIdConnectAuthenticationNotifications
            {
                AuthorizationCodeReceived = async n =>
                {
                    Log.Information("Authorization Code Received");
                    await Util.SetAuthorizationCodeReceivedAuthenticationTicket(n);
                },
                SecurityTokenValidated = async n =>
                {
                    Log.Debug("SecurityTokenValidated");
                    await Util.SetSecurityTokenValidatedAuthecticationTicket(n);
                },
                MessageReceived = async msg =>
                {
                    Log.Debug("MessageReceived");
                    //Log.Debug("Message Received => {@AuthorizationCodeReceivedNotification} ", msg);
                },
                SecurityTokenReceived = async notification =>
                {
                    Log.Debug("SecurityTokenReceived");
                    //Log.Debug("Security Token Received =>", notification);
                }
            };
        }

        public static async Task SetAuthorizationCodeReceivedAuthenticationTicket(AuthorizationCodeReceivedNotification n)
        {
            var claims = await GetUserInfoClient(n.AuthenticationTicket, n.ProtocolMessage.AccessToken);

            // get access and refresh token
            var tokenClient = new OAuth2Client(new Uri(Shared.Constants.TokenEndpoint),
                Shared.Constants.Sites.A.Name, Shared.Constants.Secret);

            var response = await tokenClient.RequestAuthorizationCodeAsync(n.Code, n.RedirectUri);
            n.AuthenticationTicket = new AuthenticationTicket(new ClaimsIdentity(claims), n.AuthenticationTicket.Properties);
        }

        public static async Task SetSecurityTokenValidatedAuthecticationTicket(SecurityTokenValidatedNotification<OpenIdConnectMessage, OpenIdConnectAuthenticationOptions> n)
        {
            var claims = await GetUserInfoClient(n.AuthenticationTicket,n.ProtocolMessage.AccessToken);

            n.AuthenticationTicket = new AuthenticationTicket(new ClaimsIdentity(claims), n.AuthenticationTicket.Properties);
        }

        private static async Task<List<Claim>> GetUserInfoClient(AuthenticationTicket ticket, string token)
        {
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

            var userInfoClient = new UserInfoClient(new Uri(Shared.Constants.UserInfoEndpoint),token);
            var userInfo = await userInfoClient.GetAsync();
            userInfo.Claims.ToList().ForEach(ui => claims.Add(new Claim(ui.Item1, ui.Item2)));

            claims.Add(new Claim("access_token", token));
            //claims.Add(new Claim("expires_at", DateTime.Now.AddSeconds(response.ExpiresIn).ToLocalTime().ToString()));
            //claims.Add(new Claim("refresh_token", response.RefreshToken));
            //claims.Add(new Claim("id_token", n.ProtocolMessage.IdToken));

            return claims;
        }

    }
}
