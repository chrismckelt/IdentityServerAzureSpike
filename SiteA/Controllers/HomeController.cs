using IdentityServerAzureSpike.Shared;
using IdentityServerAzureSpike.Shared.Controllers;
using Serilog;

namespace IdentityServerAzureSpike.SiteA.Controllers
{
    public class HomeController : HomeControllerBase
    {
        public override string SiteName
        {
            get
            {
                Log.Information("Constants.Sites.A.Name");
                return Constants.Sites.A.Name;
            }
        }


        public override string SiteRedirect
        {
            get
            {
                Log.Information("Constants.Sites.A.RedirectUri");
                return  Constants.Sites.A.RedirectUri;
            }
        }
    }
}
