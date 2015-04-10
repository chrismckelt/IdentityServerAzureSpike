﻿using IdentityServerAzureSpike.Shared;
using IdentityServerAzureSpike.Shared.Controllers;

namespace IdentityServerAzureSpike.SiteB.Controllers
{
    public class CallbackController : CallbackControllerBase
	{
        public override string SiteName
        {
            get { return Constants.SiteBCodeFlow; }
        }

        public override string SiteFlow
        {
            get { return Constants.SiteBCodeFlow; }
        }

        public override string SiteRedirect
        {
            get { return Constants.SiteBCodeFlowCallBackUri; }
        }
	}
}