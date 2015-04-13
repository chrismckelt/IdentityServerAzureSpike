using System;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Thinktecture.IdentityModel.Client;

namespace IdentityServerAzureSpike.Shared.Controllers
{
    public abstract class HomeControllerBase : ControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FormLogin()
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
