using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using IdentityServerAzureSpike.Shared.Config;
using IdentityServerAzureSpike.Shared.Utility;
using Newtonsoft.Json;
using Serilog;

namespace IdentityServerAzureSpike.Shared
{
    public abstract class GlobalBase : HttpApplication
    {
        protected abstract DemoSite DemoSite { get; }

        protected void Application_Start(object sender, EventArgs e)
        {
            LogUtil.SetupLogger(DemoSite.Name);             
            Log.Information(JsonConvert.SerializeObject(DemoSite.Name));

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            HttpContext.Current.Session["Workaround"] = 0;
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            Log.Debug("Application_BeginRequest");
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            Log.Error(exception, "Application_Error");
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}
