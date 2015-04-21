using System.Web.Optimization;

namespace IdentityServerAzureSpike.Shared
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery")
                .Include("~/Scripts/jquery-{version}.js")
                .Include("~/Scripts/jquery-ui-{version}.js"));


            bundles.Add(new StyleBundle("~/Content/jquerystyle").IncludeDirectory("~/Content/themes/base/", "*.css", true));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            // custom
            bundles.Add(new ScriptBundle("~/scripts/sites").Include(
                        "~/scripts/sites.js"));

            bundles.Add(new ScriptBundle("~/scripts/app").Include(
                        "~/scripts/app.js"));
            
        }
    }
}
