using IdentityServerAzureSpike.Shared;
using IdentityServerAzureSpike.Shared.Controllers;

namespace IdentityServerAzureSpike.SiteA.Controllers
{
    public class HomeController : HomeControllerBase
    {
        public override string SiteName
        {
            get { return Constants.Sites.A.Name; }
        }


        public override string SiteRedirect
        {
            get { return  Constants.Sites.A.RedirectUri; }
        }
    }
}
