using IdentityServerAzureSpike.Shared;
using IdentityServerAzureSpike.Shared.Controllers.CodeFlow;

namespace IdentityServerAzureSpike.SiteE.Controllers.Implicit
{
    public class AppCallbackController : CallbackControllerBase
	{
        public override string SiteName
        {
            get { return Constants.SiteEImplicitFlow; }
        }

        public override string SiteRedirect
        {
            get { return Constants.SiteEImplicitCallBackUri; }
        }
	}
}