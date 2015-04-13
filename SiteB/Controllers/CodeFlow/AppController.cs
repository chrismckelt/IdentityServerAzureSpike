﻿using System.Web.Mvc;
using IdentityServerAzureSpike.Shared;

namespace IdentityServerAzureSpike.SiteB.Controllers.CodeFlow
{
    [Authorize]
    public class AppController : IdentityServerAzureSpike.Shared.Controllers.CodeFlow.AppControllerBase
    {
        public override string SiteName
        {
            get { return Constants.SiteBHybrid; }
        }

        public override string SiteRedirect
        {
            get { return Constants.SiteBRedirectBouncedFromIdentityServerUri; }
        }
    }
}