﻿using IdentityServerAzureSpike.Shared;
using IdentityServerAzureSpike.Shared.Controllers;

namespace IdentityServerAzureSpike.SiteD.Controllers
{
    public class HomeController : HomeControllerBase
    {
        public override string SiteName
        {
            get { return Constants.SiteAHybrid; }
        }

        public override string SiteFlow
        {
            get { return Constants.SiteAHybrid; }
        }

        public override string SiteRedirect
        {
            get { return Constants.SiteARedirectBouncedFromIdentityServerUri; }
        }
    }
}