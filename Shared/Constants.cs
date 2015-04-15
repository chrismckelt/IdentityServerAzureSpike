using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Serilog;

namespace IdentityServerAzureSpike.Shared
{
    public static class Constants
    {
        public const string IdentityServer = "IdentitySite";
        public const string IdentityServerUri = "https://identity.demo.local";
        public const string IdentityServerCoreUri = "https://identity.demo.local/core";
        public const string IdentityServerIdentityUri = "https://identity.demo.local/identity";

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

        public static readonly List<string> FullScopes = Scopes.Full.Split().ToList();
        public static readonly List<string> CodeFlowScopes = Scopes.CodeFlow.Split().ToList();
        public static readonly List<string> ImplicitScopes = Scopes.Implicit.Split().ToList();


        public static class Cookie
        {
            public const string Name = "identity";
            public const string Domain = "demo.local";
            public const string TempPassiveStateAuthenticationType = "AuthenticationTypeCookies";
            public const string AuthenticationType = "Cookies";

            public static CookieAuthenticationOptions BuildActive()
            {
                return new CookieAuthenticationOptions
                {
                    CookieHttpOnly = false,
                    CookieSecure = CookieSecureOption.SameAsRequest,
                    ExpireTimeSpan = TimeSpan.FromHours(1),
                    CookieDomain = Domain,
                    AuthenticationMode = AuthenticationMode.Active,
                    //AuthenticationType = AuthenticationType,
                    //Provider = new CookieAuthenticationProvider //http://brockallen.com/2013/10/27/using-cookie-authentication-middleware-with-web-api-and-401-response-codes/
                    //{
                    //    OnException = err =>
                    //    {
                    //        Log.Error(err.Exception, "@Exception");
                    //    }
                    //}
                   
                };
            }

            public static CookieAuthenticationOptions BuildPassive()
            {
                return new CookieAuthenticationOptions
                {
                    CookieName = Name,
                    CookieHttpOnly = false,
                    CookieSecure = CookieSecureOption.SameAsRequest,
                    ExpireTimeSpan = TimeSpan.FromHours(1),
                    CookieDomain = Domain,
                    AuthenticationMode = AuthenticationMode.Passive,
                    AuthenticationType = TempPassiveStateAuthenticationType,
                    //Provider = new CookieAuthenticationProvider //http://brockallen.com/2013/10/27/using-cookie-authentication-middleware-with-web-api-and-401-response-codes/
                    //{
                    //    OnApplyRedirect = ctx =>
                    //    {
                    //        if (!IsAjaxRequest(ctx.Request))
                    //        {
                    //            ctx.Response.Redirect(ctx.RedirectUri);
                    //        }
                    //    },
                    //    OnException = err =>
                    //    {
                    //        Log.Error(err.Exception, "@Exception");
                    //    }
                    //}

                };
            }

            private static bool IsAjaxRequest(IOwinRequest request)
            {
                IReadableStringCollection query = request.Query;
                if ((query != null) && (query["X-Requested-With"] == "XMLHttpRequest"))
                {
                    return true;
                }
                IHeaderDictionary headers = request.Headers;
                return ((headers != null) && (headers["X-Requested-With"] == "XMLHttpRequest"));
            }
        
        }


    }
}
