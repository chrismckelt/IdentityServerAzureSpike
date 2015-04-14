using IdentityServerAzureSpike.Shared;

namespace IdentityServerAzureSpike.SiteB.Controllers.CodeFlow
{
    public class CodeFlowController : IdentityServerAzureSpike.Shared.Controllers.CodeFlow.CodeFlowControllerBase
    {
        public override string SiteName
        {
            get { return Constants.Sites.B.Name; }
        }

        public override string SiteRedirect
        {
            get { return Constants.Sites.B.PostbackUri; }
        }
    }
}