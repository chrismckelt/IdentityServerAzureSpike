using IdentityServerAzureSpike.Shared;
using Microsoft.WindowsAzure.ServiceRuntime;

namespace IdentityServerAzureSpike.SiteC
{
    public class WebRole : RoleEntryPoint
    {
        public override bool OnStart()
        {
            LogUtil.SetupLogger("SiteC"); 

            return base.OnStart();
        }
    }
}
