using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using IdentityServerAzureSpike.Shared.Config;

namespace IdentityServerAzureSpike.Shared.Controllers.ImplicitFlow
{
    [Route(Constants.RedirectUri.ImplicitFlow)]
    public class ImplicitFlowController : ControllerBase
    {

        [HttpPost]
        [ActionName("Index")]
        public ActionResult ImplicitFormPostedDetails()
        {
            var state = Guid.NewGuid().ToString("N");
            var nonce = Guid.NewGuid().ToString("N");

            var url = Constants.AuthorizeEndpoint +
                "?client_id=" + DemoSite.Name +
                "&response_type=" + Thinktecture.IdentityServer.Core.Constants.ResponseTypes.IdTokenToken + 
                "&scope=openid email" +
                "&redirect_uri=" + DemoSite.ImplicitUri +
                "&response_mode=form_post" +
                "&state=" + state +
                "&nonce=" + nonce;

            SetTempCookie(state, nonce);
            return Redirect(url);
        }


        [HttpPost]
        public async Task<ActionResult>CallBack()
        {
            var token = Request.Form["id_token"];
            var state = Request.Form["state"];

            var claims = await ValidateIdentityTokenAsync(token, state);

            var id = new ClaimsIdentity(claims, Shared.Constants.Cookie.AuthenticationType);
            Request.GetOwinContext().Authentication.SignIn(id);

            return Redirect("/");
        }

        private async Task<IEnumerable<Claim>> ValidateIdentityTokenAsync(string token, string state)
        {

            var result = await Request
                .GetOwinContext()
                .Authentication
                .AuthenticateAsync(Shared.Constants.Cookie.TempPassiveStateAuthenticationType);

            if (result == null)
            {
                throw new InvalidOperationException(Shared.Constants.Cookie.TempPassiveStateAuthenticationType + "not found - No temp cookie");
            }

            if (state != result.Identity.FindFirst("state").Value)
            {
                throw new InvalidOperationException("invalid state");
            }

            var parameters = new TokenValidationParameters
            {
                ValidAudience = DemoSite.Name,
                ValidIssuer = Constants.IdentityServerUri,
                IssuerSigningToken = new X509SecurityToken(Certificate.Get())
            };

            var handler = new JwtSecurityTokenHandler();
            SecurityToken jwt;
            var id = handler.ValidateToken(token, parameters, out jwt);

            if (id.FindFirst("nonce").Value !=
                result.Identity.FindFirst("nonce").Value)
            {
                throw new InvalidOperationException("Invalid nonce");
            }

            Request
                .GetOwinContext()
                .Authentication
                .SignOut(Shared.Constants.Cookie.TempPassiveStateAuthenticationType);

            return id.Claims;
        }


        private void SetTempCookie(string state, string nonce)
        {
            var tempId = new ClaimsIdentity(Shared.Constants.Cookie.TempPassiveStateAuthenticationType);
            tempId.AddClaim(new Claim("state", state));
            tempId.AddClaim(new Claim("nonce", nonce));

            Request.GetOwinContext().Authentication.SignIn(tempId);
        }
    }
}