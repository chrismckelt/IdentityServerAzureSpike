using IdentityServerAzureSpike.Shared;
using Microsoft.WindowsAzure.ServiceRuntime;

namespace IdentityServerAzureSpike.SiteA
{
    public class WebRole : RoleEntryPoint
    {
        public override bool OnStart()
        {
            LogUtil.SetupLogger("SiteA"); 

            return base.OnStart();
        }
    }
}
