using System;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using IdentityServer3.Core.Logging;
using Microsoft.Owin.Hosting;
using Microsoft.WindowsAzure.ServiceRuntime;

namespace SelfHostedIdentityServerWebApi
{
    public class WorkerRole : RoleEntryPoint
    {
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private readonly ManualResetEvent _runCompleteEvent = new ManualResetEvent(false);
        private IDisposable _app = null;

        public override void Run()
        {
            Trace.TraceInformation("SelfHostedIdentityServerWebApi is running");

            try
            {
                this.RunAsync(this._cancellationTokenSource.Token).Wait();
            }
            finally
            {
                this._runCompleteEvent.Set();
            }
        }

        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections
            ServicePointManager.DefaultConnectionLimit = 12;

            LogProvider.SetCurrentLogProvider(new DiagnosticsTraceLogProvider());

            try
            {
                var endpoint = RoleEnvironment.CurrentRoleInstance.InstanceEndpoints["SelfHostedIdentityServerWebApiEndpoint1"];
                string baseUri = String.Format("{0}://{1}", endpoint.Protocol, endpoint.IPEndpoint);
                var options = new StartOptions(url: baseUri);
                
           //     options.Urls.Add("https://+:443/");
                //options.Urls.Add("http://+:80/");
               // options.Urls.Add("http://+:9555/");
                _app = WebApp.Start<Startup>(options);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            Trace.TraceInformation("SelfHostedIdentityServerWebApi has been started");

            return base.OnStart();
        }

        public override void OnStop()
        {
            Trace.TraceInformation("SelfHostedIdentityServerWebApi is stopping");

            this._cancellationTokenSource.Cancel();
            this._runCompleteEvent.WaitOne();

            base.OnStop();

            Trace.TraceInformation("SelfHostedIdentityServerWebApi has stopped");
        }

        private async Task RunAsync(CancellationToken cancellationToken)
        {        
            while (!cancellationToken.IsCancellationRequested)
            {
                Trace.TraceInformation("Heartbeat " + DateTime.Now.ToShortTimeString());
                await Task.Delay(1000);
            }
        }

    }
}
