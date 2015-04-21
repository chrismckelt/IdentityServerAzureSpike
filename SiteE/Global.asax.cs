using IdentityServerAzureSpike.Shared;
using IdentityServerAzureSpike.Shared.Config;

namespace IdentityServerAzureSpike.SiteE
{
    public class Global : GlobalBase
    {
        protected override DemoSite DemoSite
        {
            get { return DemoSites.Instance.E; }
        }
    }
}
