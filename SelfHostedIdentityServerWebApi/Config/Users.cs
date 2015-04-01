using System.Collections.Generic;
using System.Security.Claims;
using IdentityServer3.Core;
using IdentityServer3.Core.Services.InMemory;

namespace SelfHostedIdentityServerWebApi.Config
{
    public class Users
    {
        public static List<InMemoryUser> Get()
        {
            return new List<InMemoryUser>
            {
                new InMemoryUser{Subject = "chris", Username = "chris", Password = "secretsauce", 
                    Claims = new Claim[]
                    {
                        new Claim(Constants.ClaimTypes.GivenName, "Chris"),
                        new Claim(Constants.ClaimTypes.FamilyName, "McKelt"),
                        new Claim(Constants.ClaimTypes.Email, "chris@mckelt.com"),
                    }
                },
                new InMemoryUser{Subject = "blair", Username = "blair", Password = "secretsauce", 
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
