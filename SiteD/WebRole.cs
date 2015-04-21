using System;
using System.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;

namespace IdentityServerAzureSpike.SiteD
{
    public class WebRole : RoleEntryPoint
    {
        public override bool OnStart()
        {
           Trace.WriteLine("WebRole.OnStart..." + DateTime.Now.ToShortTimeString());

            return base.OnStart();
        }
    }
}