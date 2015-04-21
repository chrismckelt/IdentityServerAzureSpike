using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using Serilog;
using Thinktecture.IdentityModel.Client;

namespace IdentityServerAzureSpike.Shared.Controllers.CodeFlow
{
    [Authorize]
    [Route("App")]
    public abstract class AppController : ControllerBase
    {

        public string GetToken()
        {
            var principal = User as ClaimsPrincipal;
            var value = principal.FindFirst("access_token");
            if (value != null) return value.Value;

            return Convert.ToString(Session["access_token"]);
        }

        public ActionResult Index()
        {
            Log.Information("Index");
            return View("~/Views/CodeFlow/App/Index.cshtml");
        }

        public async Task<ActionResult> CallService()
        {
            Log.Information("CallService");

            var client = new HttpClient();
            
            client.SetBearerToken(GetToken());

            var result = await client.GetStringAsync(Shared.Constants.IdentityServerIdentityUri);

            return View("~/Views/CodeFlow/App/CallService.cshtml",JArray.Parse(result));
        }

        public async Task<ActionResult> RefreshToken()
        {
            var client = new OAuth2Client(
                new Uri(Shared.Constants.TokenEndpoint),
                DemoSite.Name,
                Shared.Constants.Secret);

            var principal = User as ClaimsPrincipal;
            var refreshToken = principal.FindFirst("refresh_token").Value;

            var response = await client.RequestRefreshTokenAsync(refreshToken);
            UpdateCookie(response);

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> RevokeAccessToken()
        {
            var accessToken = (User as ClaimsPrincipal).FindFirst("access_token").Value;
            var client = new HttpClient();
            client.SetBasicAuthentication("floop", Shared.Constants.Secret);

            var postBody = new Dictionary<string, string>
            {
                { "token", accessToken },
                { "token_type_hint", "access_token" }
            };

            var result = await client.PostAsync(Shared.Constants.TokenRevocationEndpoint, new FormUrlEncodedContent(postBody));

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> RevokeRefreshToken()
        {
            var refreshToken = (User as ClaimsPrincipal).FindFirst("refresh_token").Value;
            var client = new HttpClient();
            client.SetBasicAuthentication(DemoSite.Name,Shared.Constants.Secret);

            var postBody = new Dictionary<string, string>
            {
                { "token", refreshToken },
                { "token_type_hint", "refresh_token" }
            };

            var result = await client.PostAsync(Shared.Constants.TokenRevocationEndpoint, new FormUrlEncodedContent(postBody));

            return RedirectToAction("Index");
        }

        private void UpdateCookie(TokenResponse response)
        {
            if (response.IsError)
            {
                throw new Exception(response.Error);
            }

            var identity = (User as ClaimsPrincipal).Identities.First();
            var result = from c in identity.Claims
                         where c.Type != "access_token" &&
                               c.Type != "refresh_token" &&
                               c.Type != "expires_at"
                         select c;

            var claims = result.ToList();

            claims.Add(new Claim("access_token", response.AccessToken));
            claims.Add(new Claim("expires_at", (DateTime.UtcNow.ToEpochTime() + response.ExpiresIn).ToDateTimeFromEpoch().ToString()));
            claims.Add(new Claim("refresh_token", response.RefreshToken));

            var newId = new ClaimsIdentity(claims, Shared.Constants.Cookie.AuthenticationType);
            Request.GetOwinContext().Authentication.SignIn(newId);
        }
	}
}