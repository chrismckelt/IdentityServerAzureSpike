using IdentityServerAzureSpike.Shared;
using IdentityServerAzureSpike.Shared.Controllers;

namespace IdentityServerAzureSpike.SiteD.Controllers
{
    public class CallbackController : CallbackControllerBase
	{
        public override string SiteName
        {
            get { return Constants.SiteACodeFlow; }
        }

        public override string SiteFlow
        {
            get { return Constants.SiteACodeFlow; }
        }

        public override string SiteRedirect
        {
            get { return Constants.SiteACodeFlowCallBackUri; }
        }
	}
}