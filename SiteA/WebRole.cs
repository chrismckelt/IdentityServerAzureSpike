using Microsoft.WindowsAzure.ServiceRuntime;
using Serilog;
using Thinktecture.IdentityServer.Core.Logging;
using Thinktecture.IdentityServer.Core.Logging.LogProviders;

namespace IdentityServerAzureSpike.SiteA
{
    public class WebRole : RoleEntryPoint
    {
        public override bool OnStart()
        {
            // serilog to azure console & file
            Log.Logger = new LoggerConfiguration()
            .WriteTo
            .ColoredConsole(outputTemplate: "{Timestamp:HH:mm} [{Level}] ({Name}) {NewLine} {Message}{NewLine}{Exception}")
            .WriteTo.File(@"c:\logs\sitea.log")
            .CreateLogger();

            LogProvider.SetCurrentLogProvider(new SerilogLogProvider());

            return base.OnStart();
        }
    }
}
