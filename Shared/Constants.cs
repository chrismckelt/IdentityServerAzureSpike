namespace IdentityServerAzureSpike.Shared
{
    public static class Constants
    {
        public const string IdentityServer = "ccIdentity";
        public const string SiteA = "Site A";
        public const string SiteB = "Site B";
        public const string SiteC = "Site C";
        public const string IdentityServerUri = "https://identity.demo.local";
        public const string SiteAUri = "https://siteA.demo.local";
        public const string SiteBUri = "https://siteB.demo.local";
        public const string SiteCUri = "https://siteC.demo.local";

        public const string Secret = "secret";

        public const string AuthorizeEndpoint = IdentityServer + "/connect/authorize";
        public const string LogoutEndpoint = IdentityServer + "/connect/endsession";
        public const string TokenEndpoint = IdentityServer + "/connect/token";
        public const string UserInfoEndpoint = IdentityServer + "/connect/userinfo";
        public const string IdentityTokenValidationEndpoint = IdentityServer + "/connect/identitytokenvalidation";
        public const string TokenRevocationEndpoint = IdentityServer + "/connect/revocation";

    }
}
