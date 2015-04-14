using IdentityServerAzureSpike.Shared;
using Microsoft.WindowsAzure.ServiceRuntime;

namespace IdentityServerAzureSpike.SiteE
{
    public class WebRole : RoleEntryPoint
    {
        public override bool OnStart()
        {
            LogUtil.SetupLogger("SiteE"); 

            return base.OnStart();
        }
    }
}
