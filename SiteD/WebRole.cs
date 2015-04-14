using IdentityServerAzureSpike.Shared;
using Microsoft.WindowsAzure.ServiceRuntime;

namespace IdentityServerAzureSpike.SiteD
{
    public class WebRole : RoleEntryPoint
    {
        public override bool OnStart()
        {
            LogUtil.SetupLogger("SiteD"); 
            return base.OnStart();
        }
    }
}
