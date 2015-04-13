using System.Web.Mvc;
using IdentityServerAzureSpike.Shared;

namespace IdentityServerAzureSpike.SiteE.Controllers.CodeFlow
{
    [Authorize]
    public class AppController : IdentityServerAzureSpike.Shared.Controllers.CodeFlow.AppControllerBase
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