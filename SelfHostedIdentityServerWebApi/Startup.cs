using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using Finsa.WebApi.HelpPage.AnyHost;
using IdentityServer3.AccessTokenValidation;
using IdentityServer3.Core.Configuration;
using IdentityServerAzureSpike.SelfHostedIdentityServerWebApi.Config;
using IdentityServerAzureSpike.Shared;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Owin;

namespace IdentityServerAzureSpike.SelfHostedIdentityServerWebApi
{
    public class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        {
            SetupIdentityServer(appBuilder);

            SetupWebApi(appBuilder);

        }

        private void SetupIdentityServer(IAppBuilder appBuilder)
        {
            var factory = InMemoryFactory.Create(
               users: Users.Get(),
               clients: Clients.Get(),
               scopes: Scopes.Get());

            var options = new IdentityServerOptions
            {
                IssuerUri = Constants.IdentityServerUri,
                SiteName = Constants.IdentityServer,
                SigningCertificate = Certificate.Get(),
                Factory = factory,
                RequireSsl = false,
                AuthenticationOptions = new AuthenticationOptions()
                {
                    EnableLocalLogin = true,
                    EnableLoginHint = true,
                    EnablePostSignOutAutoRedirect = true,
                    CookieOptions = new CookieOptions()
                    {
                        AllowRememberMe = true,
                        Path = "identity",
                        IsPersistent = true,
                        SecureMode = CookieSecureMode.SameAsRequest
                    },
                },
                
            };

            appBuilder.Map("/core", builder =>
            {
                builder.UseIdentityServer(options);

                //var bearerOptions = new IdentityServerBearerTokenAuthenticationOptions
                //{
                //    Authority = Constants.IdentityServerCoreUri,
                //};

                //builder.UseIdentityServerBearerTokenAuthentication(bearerOptions);
            });
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

    } 
}
