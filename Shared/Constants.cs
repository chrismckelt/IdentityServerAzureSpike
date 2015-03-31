namespace IdentityServerAzureSpike.Shared
{
    public static class Constants
    {
        public const string IdentityServer = "https://identity.demo.local";
        public const string SiteA = "https://siteA.demo.local";
        public const string SiteB = "https://siteB.demo.local";
        public const string SiteC = "https://siteC.demo.local";

        public const string AuthorizeEndpoint = IdentityServer + "/connect/authorize";
        public const string LogoutEndpoint = IdentityServer + "/connect/endsession";
        public const string TokenEndpoint = IdentityServer + "/connect/token";
        public const string UserInfoEndpoint = IdentityServer + "/connect/userinfo";
        public const string IdentityTokenValidationEndpoint = IdentityServer + "/connect/identitytokenvalidation";
        public const string TokenRevocationEndpoint = IdentityServer + "/connect/revocation";

    }
}
