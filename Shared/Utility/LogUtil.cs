using System;
using System.Globalization;
using System.IO;
using System.Text;
using Serilog;
using Thinktecture.IdentityServer.Core.Logging;
using Thinktecture.IdentityServer.Core.Logging.LogProviders;

namespace IdentityServerAzureSpike.Shared.Utility
{
    public static class LogUtil
    {
        public static void SetupLogger(string logname)
        {
            string file = @"c:\logs\" + CreateMeaningfulFileName(logname) + ".log";
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

        private static string CreateMeaningfulFileName(string friendlyName)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string s in friendlyName.Split(new char[] { ' '}))//remove spaces
            {
                sb.Append(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s.ToLower()));//capitalize each segment
            }
            sb.Append("_" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm"));//add date
            return sb.ToString();
        }
    }
}
