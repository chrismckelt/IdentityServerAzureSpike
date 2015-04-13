using IdentityServerAzureSpike.Shared;
using IdentityServerAzureSpike.Shared.Controllers;

namespace IdentityServerAzureSpike.SiteB.Controllers
{
    public class HomeController : HomeControllerBase
    {
        public override string SiteName
        {
            get { return Constants.SiteBHybrid; }
        }

        public override string SiteRedirect
        {
            get { return Constants.SiteBRedirectBouncedFromIdentityServerUri; }
        }
    }
}
