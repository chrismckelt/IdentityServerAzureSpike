using IdentityServerAzureSpike.Shared;
using IdentityServerAzureSpike.Shared.Controllers;

namespace IdentityServerAzureSpike.SiteA.Controllers
{
    public class ImplicitFlowController : ImplicitFlowControllerBase
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