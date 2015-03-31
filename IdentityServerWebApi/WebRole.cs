using IdentityServer3.Core.Configuration;
using IdentityServerWebApi.Configuration;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(IdentityServerWebApi.Startup))]

namespace IdentityServerWebApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var factory = InMemoryFactory.Create(
                users: Users.Get(),
                clients: Clients.Get());

            var options = new IdentityServerOptions
            {
                SigningCertificate = Certificate.Load(),
                Factory = factory,
            };

            appBuilder.UseIdentityServer(options);
        }
    }
}