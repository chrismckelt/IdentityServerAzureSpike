using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace IdentityServerAzureSpike.Shared.Controllers
{
    public class HomeController : ControllerBase
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
