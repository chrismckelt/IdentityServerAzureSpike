using System.Web.Mvc;
using IdentityServerAzureSpike.Shared;
using IdentityServerAzureSpike.Shared.Controllers.CodeFlow;

namespace IdentityServerAzureSpike.SiteD.Controllers.CodeFlow
{
    public class CallbackController : CallbackControllerBase
	{
        public override string SiteName
        {
            get { return Constants.Sites.D.Name; }
        }

        public override string SiteRedirect
        {
            get { return Constants.Sites.D.PostbackUri; }
        }
	}
}