using System.Web.Mvc;
using IdentityServerAzureSpike.Shared;
using IdentityServerAzureSpike.Shared.Controllers;

namespace IdentityServerAzureSpike.SiteD.Controllers
{
    [Authorize]
    public class AppController : AppControllerBase
    {
        public override string SiteName
        {
            get { return Constants.SiteAImplicitFlow; }
        }

        public override string SiteFlow
        {
            get { return Constants.SiteAImplicitFlow; }
        }

        public override string SiteRedirect
        {
            get { return Constants.SiteAImplicitCallBackUri; }
        }
    }
}