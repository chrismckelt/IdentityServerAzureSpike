using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace IdentityServerAzureSpike.SiteA.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        [Route]
        public ActionResult Claims()
        {
            return View((User as ClaimsPrincipal).Claims);
        }

        public ActionResult Signout()
        {
            // also possible to pass post logout redirect url via properties
            //var properties = new AuthenticationProperties
            //{
            //    RedirectUri = "http://sitea.demo.local:9556"
            //};

            Request.GetOwinContext().Authentication.SignOut();
            return Redirect("/");
        }
    }
}
