using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace IdentityServerAzureSpike.SiteE.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FormLogin()
        {
            return View();
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
