using System;
using System.Collections.Generic;
using System.Linq;
using Thinktecture.IdentityServer.Core.Models;

namespace IdentityServerAzureSpike.Shared.Config
{
    public class Scopes
    {
        public static string DemoScopeNames()
        {
            const string delimeter = " ";
            string names =  Get().Select(x => x.Name).Aggregate((i, j) => i + delimeter + j);
            Console.WriteLine("-----------------");
            Console.WriteLine(names);
            Console.WriteLine("-----------------");
            return names;
        }

        public static IEnumerable<Scope> Get()
        {
            return new[]
                {
                    ////////////////////////
                    // identity scopes
                    ////////////////////////

                    StandardScopes.OpenId,
                    StandardScopes.Profile,
                    StandardScopes.Email,
                    StandardScopes.Address,
                    StandardScopes.OfflineAccess,
                    StandardScopes.RolesAlwaysInclude,
                    StandardScopes.AllClaims,

                    ////////////////////////
                    // resource scopes
                    ////////////////////////

                    new Scope
                    {
                        Name = "read",
                        DisplayName = "Read data",
                        Type = ScopeType.Resource,
                        Emphasize = false,
                    },
                    new Scope
                    {
                        Name = "write",
                        DisplayName = "Write data",
                        Type = ScopeType.Resource,
                        Emphasize = true,
                    },
                    new Scope
                    {
                        Name = "idmgr",
                        DisplayName = "IdentityManager",
                        Type = ScopeType.Resource,
                        Emphasize = true,
                        ShowInDiscoveryDocument = false,
                        
                        Claims = new List<ScopeClaim>
                        {
                            new ScopeClaim(Thinktecture.IdentityServer.Core.Constants.ClaimTypes.Name),
                            new ScopeClaim(Thinktecture.IdentityServer.Core.Constants.ClaimTypes.Role)
                        }
                    }
                };
        }
    }
}
