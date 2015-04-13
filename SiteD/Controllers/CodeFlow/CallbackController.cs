using System.Web.Mvc;
using IdentityServerAzureSpike.Shared;
using IdentityServerAzureSpike.Shared.Controllers.CodeFlow;

namespace IdentityServerAzureSpike.SiteD.Controllers.CodeFlow
{
    public class CallbackController : CallbackControllerBase
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