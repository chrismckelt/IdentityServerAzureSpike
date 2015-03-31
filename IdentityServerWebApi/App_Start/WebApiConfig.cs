using System.Web.Http;
using System.Web.Http.Cors;
using Finsa.WebApi.HelpPage.AnyHost;

namespace IdentityServerAzureSpike.IdentityServerWebApi
{
    public static class WebApiConfig
    {
        public static HttpConfiguration Register()
        {
            // Web API configuration and services
            var config = new HttpConfiguration();
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            // Web API routes
            config.MapHttpAttributeRoutes(new HelpDirectRouteProvider());
            config.MapHttpAttributeRoutes();

            config.EnableCors();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}",
                defaults: new { id = RouteParameter.Optional }
            );

            return config;
        }
    }
}