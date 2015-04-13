using System.Web.Mvc;
using IdentityServerAzureSpike.Shared;

namespace IdentityServerAzureSpike.SiteD.Controllers.CodeFlow
{
    [Authorize]
    public class AppController : IdentityServerAzureSpike.Shared.Controllers.CodeFlow.AppControllerBase
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