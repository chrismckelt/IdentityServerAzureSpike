using System.Collections.Generic;
using IdentityServer3.Core.Models;
using IdentityServerAzureSpike.Shared;

namespace IdentityServerAzureSpike.SelfHostedIdentityServerWebApi.Config
{
    public class Clients
    {
        public static List<Client> Get()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = Constants.SiteA,
                    ClientName = Constants.SiteA,
                    ClientUri = Constants.SiteAUri,
                    Enabled = true,

                    ClientSecrets = new List<Secret>
                    {
                        new Secret(Constants.Secret)
                    },

                    Flow = Flows.Hybrid,
                    
                    RedirectUris = Shared.Constants.RedirectUris,
                    PostLogoutRedirectUris =  Shared.Constants.RedirectUris,
                },

            };
        }
    }
}