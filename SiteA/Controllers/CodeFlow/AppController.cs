using System.Web.Mvc;
using IdentityServerAzureSpike.Shared;

namespace IdentityServerAzureSpike.SiteA.Controllers.CodeFlow
{
    [Authorize]
    public class AppController : Shared.Controllers.CodeFlow.AppControllerBase
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