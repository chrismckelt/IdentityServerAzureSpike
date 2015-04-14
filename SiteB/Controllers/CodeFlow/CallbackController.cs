using IdentityServerAzureSpike.Shared;
using IdentityServerAzureSpike.Shared.Controllers.CodeFlow;

namespace IdentityServerAzureSpike.SiteB.Controllers.CodeFlow
{
    public class CallbackController : CallbackControllerBase
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