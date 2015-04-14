using IdentityServerAzureSpike.Shared;
using IdentityServerAzureSpike.Shared.Controllers.CodeFlow;

namespace IdentityServerAzureSpike.SiteA.Controllers.CodeFlow
{
    public class AppCallbackController : CallbackControllerBase
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