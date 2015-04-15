using System;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Security;
using IdentityServerAzureSpike.Shared.Config;
using Microsoft.Owin;
using Newtonsoft.Json;

namespace IdentityServerAzureSpike.Shared.Utility
{
    public static class AuthUtil
    {
        private const string Purpose = "DemoPurpose";
        public const string DemoSiteCookieName = "DemoSiteCookie";

        public static void EnsureSiteAuthExists(IOwinContext ctx, DemoSite site)
        {
            //TODO check if OWIN setup supports cookies
            CreateCookie(ctx, DemoSiteCookieName, JsonConvert.SerializeObject(site));
        }

        public static DemoSite GetCurrentSite(IOwinContext ctx)
        {
            var cookie = ctx.Request.Cookies[DemoSiteCookieName];
            if (cookie == null) throw new CookieException();
            return JsonConvert.DeserializeObject<DemoSite>(cookie);
        }

        public static void CreateCookie(IOwinContext context, string cookieName, string value)
        {
            var cookie = context.Request.Cookies[cookieName];

            if (string.IsNullOrEmpty(cookie))
            {
               context.Response.Cookies.Delete(cookieName);
            }
            context.Response.Cookies.Append(cookieName, value);
        }


        //dead code below

        public static string Protect(string text)
        {
            if (string.IsNullOrEmpty(text))
                return null;

            byte[] stream = Encoding.UTF8.GetBytes(text);
            byte[] encodedValue = MachineKey.Protect(stream, Purpose);
            return HttpServerUtility.UrlTokenEncode(encodedValue);
        }

        public static string Unprotect(string text)
        {
            if (string.IsNullOrEmpty(text))
                return null;

            byte[] stream = HttpServerUtility.UrlTokenDecode(text);
            byte[] decodedValue = MachineKey.Unprotect(stream, Purpose);
            return Encoding.UTF8.GetString(decodedValue);
        }
    }
}
