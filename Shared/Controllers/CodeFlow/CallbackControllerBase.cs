using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using Thinktecture.IdentityModel.Client;

namespace IdentityServerAzureSpike.Shared.Controllers.CodeFlow
{
    [Route("callback")]
	public abstract class CallbackControllerBase : ControllerBase
	{
        [ActionName("Index")]
		public async Task<ActionResult> Index()
		{
            ViewBag.Code = Request.QueryString["code"] ?? "none";
            
            var state = Request.QueryString["state"];
            var tempState = await GetTempStateAsync();

            if (state.Equals(tempState.Item1, StringComparison.Ordinal))
            {
                ViewBag.State = state + " (valid)";
            }
            else
            {
                ViewBag.State = state + " (invalid)";
            }

            ViewBag.Error = Request.QueryString["error"] ?? "none";

            return View("~/Views/CodeFlow/Callback/Index.cshtml");
		}

        [HttpPost]
        [ActionName("CallBack")]
        public async Task<ActionResult> CallBack()
        {
            var client = new OAuth2Client(
                new Uri(Constants.TokenEndpoint),
                SiteName,
                Constants.Secret);

            var code = Request.QueryString["code"];
            var tempState = await GetTempStateAsync();
            Request.GetOwinContext().Authentication.SignOut("TempState");

            var response = await client.RequestAuthorizationCodeAsync(
                code,
                SiteRedirect);

            await ValidateResponseAndSignInAsync(response, tempState.Item2);

            if (!string.IsNullOrEmpty(response.IdentityToken))
            {
                ViewBag.IdentityTokenParsed = ParseJwt(response.IdentityToken);
            }
            if (!string.IsNullOrEmpty(response.AccessToken))
            {
                ViewBag.AccessTokenParsed = ParseJwt(response.AccessToken);
            }

            return View("~/Views/CodeFlow/Callback/Index.cshtml", response);
        }

        private async Task ValidateResponseAndSignInAsync(TokenResponse response, string nonce)
        {
            if (!string.IsNullOrWhiteSpace(response.IdentityToken))
            {
                var tokenClaims = ValidateToken(response.IdentityToken, nonce);
                var claims = new List<Claim>();

                if (!string.IsNullOrWhiteSpace(response.AccessToken))
                {
                    claims.AddRange(await GetUserInfoClaimsAsync(response.AccessToken));

                    claims.Add(new Claim("access_token", response.AccessToken));
                    claims.Add(new Claim("expires_at", (DateTime.UtcNow.ToEpochTime() + response.ExpiresIn).ToDateTimeFromEpoch().ToString()));
                }

                if (!string.IsNullOrWhiteSpace(response.RefreshToken))
                {
                    claims.Add(new Claim("refresh_token", response.RefreshToken));
                }

                var id = new ClaimsIdentity(claims, "Cookies");
                Request.GetOwinContext().Authentication.SignIn(id);
            }
        }

        private List<Claim> ValidateToken(string token, string nonce)
        {
            
            var parameters = new TokenValidationParameters
            {
                ValidAudience = SiteName,
                ValidIssuer = Constants.IdentityServerUri,
                IssuerSigningToken = new X509SecurityToken(Certificate.Get())
            };

            SecurityToken jwt;
            var principal = new JwtSecurityTokenHandler().ValidateToken(token, parameters, out jwt);

            // validate nonce
            var nonceClaim = principal.FindFirst("nonce");

            if (!string.Equals(nonceClaim.Value, nonce, StringComparison.Ordinal))
            {
                throw new Exception("invalid nonce");
            }

            return principal.Claims.ToList();
        }

        private async Task<IEnumerable<Claim>> GetUserInfoClaimsAsync(string accessToken)
        {
            var userInfoClient = new UserInfoClient(new Uri(Constants.UserInfoEndpoint), accessToken);

            var userInfo = await userInfoClient.GetAsync();

            var claims = new List<Claim>();
            userInfo.Claims.ToList().ForEach(ui => claims.Add(new Claim(ui.Item1, ui.Item2)));

            return claims;
        }

        private string ParseJwt(string token)
        {
            if(!token.Contains("."))
            {
                return token;
            }

            var parts = token.Split('.');
            var part = Encoding.UTF8.GetString(Base64Url.Decode(parts[1]));

            var jwt = JObject.Parse(part);
            return jwt.ToString();
        }
	}
}