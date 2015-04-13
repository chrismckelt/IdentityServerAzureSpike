using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Thinktecture.IdentityModel.Client;

namespace IdentityServerAzureSpike.Shared.Controllers.CodeFlow
{
    [Route("codeflow")]
    public abstract class CodeFlowControllerBase : ControllerBase
    {

        public const string CookieName = "TempState";

        [HttpPost]
        [ActionName("Index")]
        public ActionResult CodeFlowPostedDetails(string scopes)
        {
            var state = Guid.NewGuid().ToString("N");
            var nonce = Guid.NewGuid().ToString("N");
            SetTempCookie(state, nonce);

            var client = new OAuth2Client(new Uri(Constants.AuthorizeEndpoint));

            var url = client.CreateCodeFlowUrl(
                clientId: SiteName,
                scope: scopes,
                redirectUri: SiteRedirect,
                state: state,
                nonce: nonce);

            return Redirect(url);
        }


        private void SetTempCookie(string state, string nonce)
        {
            var tempId = new ClaimsIdentity(CookieName);
            tempId.AddClaim(new Claim("state", state));
            tempId.AddClaim(new Claim("nonce", nonce));

            Request.GetOwinContext().Authentication.SignIn(tempId);
        }
    }
}