using System.IO;
using Serilog;
using Thinktecture.IdentityServer.Core.Logging;
using Thinktecture.IdentityServer.Core.Logging.LogProviders;

namespace IdentityServerAzureSpike.Shared
{
    public static class LogUtil
    {
        public static void SetupLogger(string logname)
        {
            string file = @"c:\logs\" + logname + ".log";
            File.Delete(file);

            // serilog to azure console & file
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo
            .ColoredConsole(outputTemplate: "{Timestamp:HH:mm} [{Level}] ({Name}) {NewLine} {Message}{NewLine}{Exception}")
            .WriteTo.File(file)
            .CreateLogger();

            var provider = new SerilogLogProvider();
            LogProvider.SetCurrentLogProvider(provider);
            Log.Debug(logname); 
        }
    }
}
