using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using IdentityServerAzureSpike.SiteE;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace IdentityServerAzureSpike.SiteE
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {

        }
    }
    }
