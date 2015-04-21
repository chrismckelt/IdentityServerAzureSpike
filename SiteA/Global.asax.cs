using IdentityServerAzureSpike.Shared;
using IdentityServerAzureSpike.Shared.Config;

namespace IdentityServerAzureSpike.SiteA
{
    public class Global : GlobalBase
    {
        protected override DemoSite DemoSite
        {
            get { return DemoSites.Instance.A; }
        }
    }
}