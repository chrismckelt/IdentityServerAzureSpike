using System.Collections.Generic;
using System.Linq;

namespace IdentityServerAzureSpike.Shared
{
    public static class Constants
    {
        public const string IdentityServer = "IdentitySite";
        public const string IdentityServerUri = "https://identity.demo.local";
        public const string IdentityServerCoreUri = "https://identity.demo.local/core";
        public const string IdentityServerIdentityUri = "https://identity.demo.local/identity";

        public const string SiteA = "SiteA";
        public const string SiteAService = "SiteA_Service";
        public const string SiteAUri = "http://siteA.demo.local:9556";
        public const string SiteARedirectBouncedFromIdentityServerUri = SiteAUri + "/BouncedFromIdentityServer";
        public const string SiteARedirectCallbackUri = SiteAUri + "/callback";

        public const string SiteB = "SiteB";
        public const string SiteBService = "SiteB_Service";
        public const string SiteBUri = "http://siteB.demo.local:9557";
        public const string SiteBRedirectBouncedFromIdentityServerUri = SiteBUri + "/BouncedFromIdentityServer";
        public const string SiteBRedirectCallbackUri = SiteBUri + "/callback";
        
        public const string SiteC = "SiteC";
        public const string SiteCUri = "http://siteC.demo.local:9558";

        public const string Secret = "secret";

        public const string AuthorizeEndpoint = IdentityServerCoreUri + "/connect/authorize";
        public const string LogoutEndpoint = IdentityServerCoreUri + "/connect/endsession";
        public const string TokenEndpoint = IdentityServerCoreUri + "/connect/token";
        public const string UserInfoEndpoint = IdentityServerCoreUri + "/connect/userinfo";
        public const string IdentityTokenValidationEndpoint = IdentityServerCoreUri + "/connect/identitytokenvalidation";
        public const string TokenRevocationEndpoint = IdentityServerCoreUri + "/connect/revocation";
        public const string PersmissionsEndpoint = IdentityServerCoreUri + "/permissions";

        public const string RequiredScopesString = "openid email profile read write offline_access";
        public static readonly List<string> RequiredScopes = RequiredScopesString.Split().ToList();
        
        public static readonly List<string> RedirectUris = new List<string>()
        {
            Constants.SiteAUri,
            Constants.SiteARedirectBouncedFromIdentityServerUri,
            Constants.SiteARedirectCallbackUri,
            Constants.SiteBUri,
            Constants.SiteBRedirectBouncedFromIdentityServerUri,
            Constants.SiteBRedirectCallbackUri
        };

        public static class Cookie
        {
            public const string Name = "identity";
            public const string Domain = "identity.demo.local";
            public const string Path = "identity.demo.local";
        }
    }
}
