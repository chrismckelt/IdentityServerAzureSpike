using IdentityServerAzureSpike.Shared;

namespace IdentityServerAzureSpike.SiteA.Controllers.CodeFlow
{
    public class CodeFlowController : IdentityServerAzureSpike.Shared.Controllers.CodeFlow.CodeFlowControllerBase
    {
        public override string SiteName
        {
            get { return Constants.Sites.A.Name; }
        }

        public override string SiteRedirect
        {
            get { return  Constants.Sites.A.PostbackUri; }
        }
    }
}