using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Logging;
using IdentityServerAzureSpike.IdentityServerWebApi;
using IdentityServerAzureSpike.IdentityServerWebApi.Configuration;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace IdentityServerAzureSpike.IdentityServerWebApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            //LogProvider.SetCurrentLogProvider(new DiagnosticsTraceLogProvider());

            var factory = InMemoryFactory.Create(
                users: Users.Get(),
                clients: Clients.Get());

            var options = new IdentityServerOptions
            {
                SigningCertificate = Certificate.Load(),
                Factory = factory,
            };

            appBuilder.UseIdentityServer(options);

            WebApiConfig.Register();

        }
    }
}