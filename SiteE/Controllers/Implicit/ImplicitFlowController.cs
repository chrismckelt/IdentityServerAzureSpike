using IdentityServerAzureSpike.Shared;
using IdentityServerAzureSpike.Shared.Controllers.Implicit;

namespace IdentityServerAzureSpike.SiteE.Controllers.Implicit
{
    public class ImplicitFlowController : ImplicitFlowControllerBase
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