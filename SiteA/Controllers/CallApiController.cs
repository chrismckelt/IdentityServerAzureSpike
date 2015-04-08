using System;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;
using IdentityServer3.Core.Models;
using IdentityServerAzureSpike.Shared;
using Newtonsoft.Json.Linq;
using Thinktecture.IdentityModel.Client;
using TokenResponse = Thinktecture.IdentityModel.Client.TokenResponse;

namespace IdentityServerAzureSpike.SiteA.Controllers
{
    public class CallApiController : Controller
    {
        // GET: CallApi/ClientCredentials
        public async Task<ActionResult> ClientCredentials()
        {
            var response = await GetTokenAsync();
            var result = await CallApi(response.AccessToken);

            ViewBag.Json = result;
            return View("ShowApiResult");
        }

        // GET: CallApi/UserCredentials
        public async Task<ActionResult> UserCredentials()
        {
            var user = User as ClaimsPrincipal;
            var token = user.FindFirst("access_token").Value;
            var result = await CallApi(token);

            ViewBag.Json = result;
            return View("ShowApiResult");
        }

        private async Task<string> CallApi(string token)
        {
            var client = new HttpClient();
            client.SetBearerToken(token);

            var json = await client.GetStringAsync(Constants.IdentityServerIdentityUri);
            return JArray.Parse(json).ToString();
        }

        private async Task<TokenResponse> GetTokenAsync()
        {
            var client = new OAuth2Client(
                new Uri(Constants.TokenEndpoint),
                Constants.SiteAService,
                Constants.Secret);

            return await client.RequestClientCredentialsAsync("read");
        }
    }
}