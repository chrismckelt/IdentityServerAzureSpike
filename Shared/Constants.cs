using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin.Security.Cookies;

namespace IdentityServerAzureSpike.Shared
{
    public static class Constants
    {
        public const string IdentityServer = "IdentitySite";
        public const string IdentityServerUri = "https://identity.demo.local";
        public const string IdentityServerCoreUri = "https://identity.demo.local/core";
        public const string IdentityServerIdentityUri = "https://identity.demo.local/identity";

        public const string SiteAHybrid = "SiteA_Hybrid";
        public const string SiteACodeFlow = "SiteA_CodeFlow";
        public const string SiteAImplicitFlow = "SiteA_ImplicitFlow";
        public const string SiteAUri = "http://sitea.demo.local:9556";
        public const string SiteARedirectBouncedFromIdentityServerUri = SiteAUri + "/BouncedFromIdentityServer/";
        public const string SiteACodeFlowCallBackUri = SiteAUri + "/callback/";
        public const string SiteAImplicitCallBackUri = SiteAUri + "/callback/";

        public const string SiteBHybrid = "SiteB_Hybrid";
        public const string SiteBCodeFlow = "SiteB_CodeFlow";
        public const string SiteBImplicitFlow = "SiteB_ImplicitFlow";
        public const string SiteBUri = "http://SiteB.demo.local:9557";
        public const string SiteBRedirectBouncedFromIdentityServerUri = SiteBUri + "/BouncedFromIdentityServer/";
        public const string SiteBCodeFlowCallBackUri = SiteBUri + "/callback/";
        public const string SiteBImplicitCallBackUri = SiteBUri + "/callback/";
        
        public const string SiteC = "SiteC";
        public const string SiteCUri = "http://sitec.demo.local:9558";

        public const string Secret = "secret";

        public const string AuthorizeEndpoint = IdentityServerCoreUri + "/connect/authorize";
        public const string LogoutEndpoint = IdentityServerCoreUri + "/connect/endsession";
        public const string TokenEndpoint = IdentityServerCoreUri + "/connect/token";
        public const string UserInfoEndpoint = IdentityServerCoreUri + "/connect/userinfo";
        public const string IdentityTokenValidationEndpoint = IdentityServerCoreUri + "/connect/identitytokenvalidation";
        public const string TokenRevocationEndpoint = IdentityServerCoreUri + "/connect/revocation";
        public const string PersmissionsEndpoint = IdentityServerCoreUri + "/permissions";

        public static class Scopes
        {
            public const string Full = "openid email profile read write offline_access";
            public const string CodeFlow = "openid profile read write";
            public const string Implicit = "openid email";
        }

        public static readonly List<string> RequiredScopes = Scopes.Full.Split().ToList();
        
        public static readonly List<string> RedirectSiteAUris = new List<string>()
        {
            Constants.SiteAUri,
            Constants.SiteARedirectBouncedFromIdentityServerUri,
            Constants.SiteACodeFlowCallBackUri,
            Constants.SiteAImplicitCallBackUri
        };

        public static readonly List<string> RedirectSiteBUris = new List<string>()
        {
            Constants.SiteBUri,
            Constants.SiteBRedirectBouncedFromIdentityServerUri,
            Constants.SiteBCodeFlowCallBackUri,
            Constants.SiteBImplicitCallBackUri
        };

        public static class Cookie
        {
            public const string Name = "identity";
            public const string Domain = "demo.local";
            public const string Path = "identity";

            public static CookieAuthenticationOptions Build()
            {
                return new CookieAuthenticationOptions
                {
                    AuthenticationType = "Cookies",
                    CookieHttpOnly = false,
                    CookieSecure = CookieSecureOption.Never,
                    //ExpireTimeSpan = TimeSpan.FromHours(1),
                    CookieDomain = Shared.Constants.Cookie.Domain,
                };
            }
        }

       
    }
}
