using IdentityServerAzureSpike.Shared;
using IdentityServerAzureSpike.Shared.Controllers;

namespace IdentityServerAzureSpike.SiteB.Controllers
{
    public class HomeController : HomeControllerBase
    {
        public override string SiteName
        {
            get { return Constants.Sites.B.Name; }
        }

        public override string SiteRedirect
        {
            get { return Constants.Sites.B.RedirectUri; }
        }
    }
}
