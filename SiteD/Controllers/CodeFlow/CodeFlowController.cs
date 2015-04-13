using IdentityServerAzureSpike.Shared;

namespace IdentityServerAzureSpike.SiteD.Controllers.CodeFlow
{
    public class CodeFlowController : Shared.Controllers.CodeFlow.CodeFlowControllerBase
    {
        public override string SiteName
        {
            get { return Constants.SiteDCodeFlow; }
        }

        public override string SiteRedirect
        {
            get { return Constants.SiteDCallBackUri; }
        }
    }
}