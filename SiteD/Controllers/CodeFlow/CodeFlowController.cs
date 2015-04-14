using System.Web.Mvc;
using IdentityServerAzureSpike.Shared;

namespace IdentityServerAzureSpike.SiteD.Controllers.CodeFlow
{
    public class CodeFlowController : Shared.Controllers.CodeFlow.CodeFlowControllerBase
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