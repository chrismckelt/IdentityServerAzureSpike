using IdentityServerAzureSpike.Shared;
using IdentityServerAzureSpike.Shared.Controllers;

namespace IdentityServerAzureSpike.SiteD.Controllers
{
    public class HomeController : HomeControllerBase
    {
        public override string SiteName
        {
            get { return Constants.Sites.D.Name; }
        }

        public override string SiteRedirect
        {
            get { return Constants.Sites.D.PostbackUri; }
        }
    }
}
