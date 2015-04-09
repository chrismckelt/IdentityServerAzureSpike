using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using IdentityServerAzureSpike.Shared;
using IdentityServerAzureSpike.Shared.Controllers;
using Newtonsoft.Json.Linq;
using Thinktecture.IdentityModel.Client;

namespace IdentityServerAzureSpike.SiteA.Controllers
{
    public class CallbackController : CallbackControllerBase
	{
        public override string SiteName
        {
            get { return Constants.SiteACodeFlow; }
        }

        public override string SiteFlow
        {
            get { return Constants.SiteACodeFlow; }
        }

        public override string SiteRedirect
        {
            get { return Constants.SiteACodeFlowCallBackUri; }
        }
	}
}