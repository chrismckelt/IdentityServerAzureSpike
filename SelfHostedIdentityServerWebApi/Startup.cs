using System.Collections.Generic;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using Finsa.WebApi.HelpPage.AnyHost;
using IdentityServerAzureSpike.SelfHostedIdentityServerWebApi.Config;
using IdentityServerAzureSpike.Shared;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Owin;
using Thinktecture.IdentityServer.Core.Configuration;
using Thinktecture.IdentityServer.Core.Services;
using Thinktecture.IdentityServer.Core.Services.Default;

namespace IdentityServerAzureSpike.SelfHostedIdentityServerWebApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            SetupIdentityServer(appBuilder);

            SetupWebApi(appBuilder);
        }

        private void SetupIdentityServer(IAppBuilder appBuilder)
        {
            var factory = InMemoryFactory.Create(Users.Get(), Clients.Get(), Scopes.Get());

            var allowedOrigins = new List<string>
            {
                "*",
                "/connect/consent",
                Shared.Constants.IdentityServerCoreUri,
                Shared.Constants.IdentityServerIdentityUri
            };

            //[Warning] CORS request made for path: /connect/consent from origin: https://identity.demo.local but rejected because invalid CORS path
            //https://github.com/IdentityServer/IdentityServer3/issues/1138
            ICorsPolicyService corsPolicyService = new DefaultCorsPolicyService
            {
                AllowAll = true,
                AllowedOrigins = allowedOrigins
            };

            factory.CorsPolicyService = new Registration<ICorsPolicyService>(corsPolicyService);

            var options = new IdentityServerOptions
            {
                IssuerUri = Constants.IdentityServerUri,
                SiteName = Constants.IdentityServer,
                SigningCertificate = Certificate.Get(),
                Factory = factory,
                RequireSsl = false,
                CorsPolicy = new CorsPolicy
                {
                    AllowedOrigins = allowedOrigins,
                    PolicyCallback = s => new Task<bool>(() => true)
                },
                AuthenticationOptions = new AuthenticationOptions
                {
                    EnableLocalLogin = true,
                    EnableLoginHint = true,
                    EnablePostSignOutAutoRedirect = true,
                    CookieOptions = new CookieOptions
                    {
                        AllowRememberMe = true,
                        Path = "/",
                        IsPersistent = true,
                        SecureMode = CookieSecureMode.SameAsRequest
                    }
                }
            };

            appBuilder.Map("/core", builder =>
            {
                builder.UseIdentityServer(options);

                var bearerOptions = new IdentityServer3.AccessTokenValidation.IdentityServerBearerTokenAuthenticationOptions()
                {
                    Authority = Constants.IdentityServerCoreUri,
                };

                builder.UseIdentityServerBearerTokenAuthentication(bearerOptions);
            });
        }

        private static void SetupWebApi(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new {id = RouteParameter.Optional});
            config.MapHttpAttributeRoutes(new HelpDirectRouteProvider());
            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());
            config.Formatters.JsonFormatter.SerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/json"));
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/json"));
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
            config.EnableCors();
            appBuilder.UseWebApi(config);
        }
    }
}
