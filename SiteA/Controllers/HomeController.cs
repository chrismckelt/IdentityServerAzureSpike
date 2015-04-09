using System;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using IdentityServerAzureSpike.Shared;
using Thinktecture.IdentityModel.Client;

namespace IdentityServerAzureSpike.SiteA.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LocalLogin()
        {
            return View();
        }


        [Authorize]
        [Route]
        public ActionResult LoginViaIdentityServerCodeFlow()
        {
            return View("Index");
        }

        
        [Route]
        public ActionResult BouncedFromIdentityServer()
        {
            return View((User as ClaimsPrincipal).Claims);
        }

        [HttpPost]
        public ActionResult LoginOnThisSite(string scopes)
        {
            var state = Guid.NewGuid().ToString("N");
            var nonce = Guid.NewGuid().ToString("N");
            SetTempState(state, nonce);

            var client = new OAuth2Client(new Uri(Constants.AuthorizeEndpoint));

            var url = client.CreateCodeFlowUrl(
                clientId: Constants.SiteBService,
                scope: scopes,
                redirectUri: Constants.SiteBRedirectCallbackUri,
                state: state,
                nonce: nonce);

            return Redirect(url);
        }

        private void SetTempState(string state, string nonce)
        {
            var tempId = new ClaimsIdentity("TempState");
            tempId.AddClaim(new Claim("state", state));
            tempId.AddClaim(new Claim("nonce", nonce));

            Request.GetOwinContext().Authentication.SignIn(tempId);
        }

        public ActionResult Signout()
        {
            // also possible to pass post logout redirect url via properties
            //var properties = new AuthenticationProperties
            //{
            //    RedirectUri = "http://SiteB.demo.local:9556"
            //};

            Request.GetOwinContext().Authentication.SignOut();
            return Redirect("/");
        }
    }
}
