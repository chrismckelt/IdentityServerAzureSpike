using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using IdentityServerAzureSpike.Shared.Config;
using IdentityServerAzureSpike.Shared.Utility;

namespace IdentityServerAzureSpike.Shared.Controllers
{
    public abstract class ControllerBase : Controller
    {
        public DemoSite DemoSite
        {
            get
            {
                return AuthUtil.GetCurrentSite(HttpContext.GetOwinContext());
            }
        }

        /// <summary>
        /// temp cookie retrieval 
        /// </summary>
        /// <returns></returns>
        protected async Task<Tuple<string, string>> GetTempStateAsync()
        {
            var data = await Request.GetOwinContext().Authentication
                .AuthenticateAsync(Shared.Constants.Cookie.AuthenticationType);

            var state = data.Identity.FindFirst("state").Value;
            var nonce = data.Identity.FindFirst("nonce").Value;

            return Tuple.Create(state, nonce);
        }
    }
}
