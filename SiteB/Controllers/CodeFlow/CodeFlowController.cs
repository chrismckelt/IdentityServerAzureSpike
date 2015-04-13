using IdentityServerAzureSpike.Shared;

namespace IdentityServerAzureSpike.SiteB.Controllers.CodeFlow
{
    public class CodeFlowController : IdentityServerAzureSpike.Shared.Controllers.CodeFlow.CodeFlowControllerBase
    {
        public override string SiteName
        {
            get { return Constants.SiteBHybrid; }
        }

        public override string SiteRedirect
        {
            get { return Constants.SiteBCallBackUri; }
        }
    }
}