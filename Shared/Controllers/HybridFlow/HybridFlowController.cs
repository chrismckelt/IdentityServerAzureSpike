using System.Security.Claims;
using System.Web.Mvc;

namespace IdentityServerAzureSpike.Shared.Controllers.HybridFlow
{
    [Route(Constants.RedirectUri.HybridFlow)]
    public class HybridFlowController : ControllerBase
    {

        [Route]
        public ActionResult BouncedFromIdentityServer()
        {
            return View((User as ClaimsPrincipal).Claims);
        }
       
    }
}