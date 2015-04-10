using IdentityServerAzureSpike.Shared;
using IdentityServerAzureSpike.Shared.Controllers;

namespace IdentityServerAzureSpike.SiteB.Controllers
{
    public class CodeFlowController : CodeFlowControllerBase
    {
        public override string SiteName
        {
            get { return Constants.SiteBHybrid; }
        }

        public override string SiteFlow
        {
            get { return Constants.SiteBHybrid; }
        }

        public override string SiteRedirect
        {
            get { return Constants.SiteBCodeFlowCallBackUri; }
        }
    }
}