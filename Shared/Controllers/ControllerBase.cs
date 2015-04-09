using System.Web.Mvc;

namespace IdentityServerAzureSpike.Shared.Controllers
{
    public abstract class ControllerBase : Controller
    {
        public abstract string SiteName { get;  }
        public abstract string SiteFlow { get;  }
        public abstract string SiteRedirect { get;}
    }
}
