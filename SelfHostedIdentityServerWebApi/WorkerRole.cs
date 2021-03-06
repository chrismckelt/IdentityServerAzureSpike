using System;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using IdentityServerAzureSpike.Shared.Utility;
using Microsoft.Owin.Hosting;
using Microsoft.WindowsAzure.ServiceRuntime;
using Serilog;

namespace IdentityServerAzureSpike.SelfHostedIdentityServerWebApi
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
           
            LogUtil.SetupLogger("IdentityDemo");

            try
            {
                // ssl
                var endpoint = RoleEnvironment.CurrentRoleInstance.InstanceEndpoints["SelfHostedIdentityServerWebApiEndpoint1"];
                string baseUri = String.Format("{0}://{1}", endpoint.Protocol, endpoint.IPEndpoint);
                var options = new StartOptions(url: baseUri);
                _app = WebApp.Start<Startup>(options);

            }
            catch (Exception ex)
            {
                Log.Error("SSL setup",ex);
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
            if (_app != null)
            {
                Trace.TraceInformation("WebApp disposing...");
                _app.Dispose();
                Trace.TraceInformation("WebApp disposed");
            }
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
