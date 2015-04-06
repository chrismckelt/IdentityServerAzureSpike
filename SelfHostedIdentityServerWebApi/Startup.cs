
using System;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.SelfHost;
using Finsa.WebApi.HelpPage.AnyHost;
using IdentityServer3.Core.Configuration;
using IdentityServerAzureSpike.Shared;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Owin;
using SelfHostedIdentityServerWebApi.Config;
using IdentityServer3.Core.Services;
using SelfHostedIdentityServerWebApi.Extensions;

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


            //factory.ClaimsProvider =
            //  new Registration<IClaimsProvider>(typeof(CustomClaimsProvider));
            //factory.UserService =
            //    new Registration<IUserService>(typeof(CustomUserService));
            //factory.CustomGrantValidator =
            //    new Registration<ICustomGrantValidator>(typeof(CustomGrantValidator));

            var options = new IdentityServerOptions
            {
                IssuerUri = "https://identity.demo.local",
                SiteName = "identity server spike (OWIN Web API self hosted)",
                SigningCertificate = Certificate.Get(),
                Factory = factory,
                RequireSsl = false,
                AuthenticationOptions = new AuthenticationOptions()
                {
                    EnableLocalLogin = true,
                    EnableLoginHint = true,

                }
            };
            
            appBuilder.UseIdentityServer(options);
        }
    } 
}
