using System.Collections.Generic;

namespace IdentityServerAzureSpike.Shared
{
    public static class Constants
    {
        public const string IdentityServer = "IdentitySite";
        public const string IdentityServerUri = "https://identity.demo.local";
        public const string IdentityServerCoreUri = "https://identity.demo.local/core";
        public const string IdentityServerIdentityUri = "https://identity.demo.local/identity";

        public const string SiteA = "SiteA";
        public const string SiteAUri = "http://siteA.demo.local:9556";
        public const string SiteARedirectUri = SiteAUri + "/claims";

        public const string SiteB = "SiteB";
        public const string SiteBUri = "http://siteB.demo.local:9557";
        
        public const string SiteC = "SiteC";
        public const string SiteCUri = "http://siteC.demo.local:9558";
        public const string Secret = "secret";

        public const string AuthorizeEndpoint = IdentityServerCoreUri + "/connect/authorize";
        public const string LogoutEndpoint = IdentityServerCoreUri + "/connect/endsession";
        public const string TokenEndpoint = IdentityServerCoreUri + "/connect/token";
        public const string UserInfoEndpoint = IdentityServerCoreUri + "/connect/userinfo";
        public const string IdentityTokenValidationEndpoint = IdentityServerCoreUri + "/connect/identitytokenvalidation";
        public const string TokenRevocationEndpoint = IdentityServerCoreUri + "/connect/revocation";

        public static readonly string[] RequiredScopes = new[] { "openid profile email phone" };
        
        public static readonly List<string> RedirectUris = new List<string>()
        {
            Constants.SiteARedirectUri
        };

    }
}
