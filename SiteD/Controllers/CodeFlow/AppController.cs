using System.Web.Mvc;
using IdentityServerAzureSpike.Shared;

namespace IdentityServerAzureSpike.SiteD.Controllers.CodeFlow
{
    [Authorize]
    public class AppController : Shared.Controllers.CodeFlow.AppControllerBase
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