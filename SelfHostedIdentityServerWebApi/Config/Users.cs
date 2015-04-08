using System.Collections.Generic;
using System.Security.Claims;
using Thinktecture.IdentityServer.Core;
using Thinktecture.IdentityServer.Core.Services.InMemory;

namespace IdentityServerAzureSpike.SelfHostedIdentityServerWebApi.Config
{
    public class Users
    {
        public static List<InMemoryUser> Get()
        {
            return new List<InMemoryUser>
            {
                new InMemoryUser{Subject = "chris", Username = "chris", Password = "password", 
                    Claims = new Claim[]
                    {
                        new Claim(Constants.ClaimTypes.GivenName, "Chris"),
                        new Claim(Constants.ClaimTypes.FamilyName, "McKelt"),
                        new Claim(Constants.ClaimTypes.Email, "chris@mckelt.com")
                    }
                },
                new InMemoryUser{Subject = "blair", Username = "blair", Password = "password", 
                    Claims = new Claim[]
                    {
                        new Claim(Constants.ClaimTypes.GivenName, "Blair"),
                        new Claim(Constants.ClaimTypes.FamilyName, "Davidson"),
                        new Claim(Constants.ClaimTypes.Email, "blair.joel.davidson@gmail.com"),
                    }
                },
            };
        }
    }
}
