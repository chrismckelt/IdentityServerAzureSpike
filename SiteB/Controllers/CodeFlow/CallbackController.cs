using IdentityServerAzureSpike.Shared;
using IdentityServerAzureSpike.Shared.Controllers.CodeFlow;

namespace IdentityServerAzureSpike.SiteB.Controllers.CodeFlow
{
    public class CallbackController : CallbackControllerBase
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