using IdentityServerAzureSpike.Shared;
using IdentityServerAzureSpike.Shared.Controllers;

namespace IdentityServerAzureSpike.SiteB.Controllers
{
    public class ImplicitFlowController : ImplicitFlowControllerBase
    {
        public override string SiteName
        {
            get { return Constants.SiteBImplicitFlow; }
        }

        public override string SiteFlow
        {
            get { return Constants.SiteBImplicitFlow; }
        }

        public override string SiteRedirect
        {
            get { return Constants.SiteBImplicitCallBackUri; }
        }
    }
}