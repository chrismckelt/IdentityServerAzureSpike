﻿
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using Finsa.WebApi.HelpPage.AnyHost;
using IdentityServer3.Core.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Owin;
using SelfHostedIdentityServerWebApi.Config;

namespace SelfHostedIdentityServerWebApi
{
    public class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        {
            SetupWebApi(appBuilder);

            SetupIdentityServer(appBuilder);

        }

        private static void SetupWebApi(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
                );
            config.MapHttpAttributeRoutes(new HelpDirectRouteProvider());
            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());
            config.Formatters.JsonFormatter.SerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

            config.EnableCors();

            appBuilder.UseWebApi(config);
        }

        private void SetupIdentityServer(IAppBuilder appBuilder)
        {
            var factory = InMemoryFactory.Create(
               users: Users.Get(),
               clients: Clients.Get(),
               scopes: Scopes.Get());

            var options = new IdentityServerOptions
            {
                IssuerUri = "https://idsrv3.com",
                SiteName = "demo.identity.local (self host)",
                SigningCertificate = Certificate.Get(),
                Factory = factory,
                RequireSsl = false
            };
            
            appBuilder.UseIdentityServer(options);
        }
    } 
}