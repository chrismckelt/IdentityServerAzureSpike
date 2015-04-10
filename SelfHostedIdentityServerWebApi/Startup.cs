using System.Collections.Generic;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using Finsa.WebApi.HelpPage.AnyHost;
using IdentityServerAzureSpike.SelfHostedIdentityServerWebApi.Config;
using IdentityServerAzureSpike.Shared;
using Microsoft.Owin.Security.OAuth;
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

            var allowedOrigins = SetupCors(factory);

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
                    EnablePostSignOutAutoRedirect = true,
                    InvalidSignInRedirectUrl = Shared.Constants.SiteAUri + "/INVALID_LOGIN=YOU",  
                    CookieOptions = new CookieOptions()
                    {
                        SecureMode = CookieSecureMode.SameAsRequest,
                    }
                },
                LoggingOptions = new LoggingOptions()
                {
                    EnableHttpLogging = true,
                    EnableWebApiDiagnostics = true,
                    IncludeSensitiveDataInLogs = true,
                    WebApiDiagnosticsIsVerbose = true
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
            SetFormatters(config);

            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            appBuilder.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            config.EnableCors();

            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
            config.EnableSystemDiagnosticsTracing();

            appBuilder.UseWebApi(config);
        }

        private static void SetFormatters(HttpConfiguration config)
        {
            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());
            config.Formatters.JsonFormatter.SerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/json"));
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/json"));
        }

        private static List<string> SetupCors(IdentityServerServiceFactory factory)
        {
            var allowedOrigins = new List<string>
            {
                "*",
                "/connect/consent",
                Shared.Constants.IdentityServerCoreUri,
                Shared.Constants.IdentityServerIdentityUri,

            };

            //[Warning] CORS request made for path: /connect/consent from origin: https://identity.demo.local but rejected because invalid CORS path
            //https://github.com/IdentityServer/IdentityServer3/issues/1138
            allowedOrigins.AddRange(Shared.Constants.RedirectSiteAUris);
            allowedOrigins.AddRange(Shared.Constants.RedirectSiteBUris);

            ICorsPolicyService corsPolicyService = new DefaultCorsPolicyService
            {
                AllowAll = true,
                AllowedOrigins = allowedOrigins
            };

            factory.CorsPolicyService = new Registration<ICorsPolicyService>(corsPolicyService);
            return allowedOrigins;
        }
    }
}
