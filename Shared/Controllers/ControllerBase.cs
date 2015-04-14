using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace IdentityServerAzureSpike.Shared.Controllers
{
    public abstract class ControllerBase : Controller
    {
        public abstract string SiteName { get;  }
        public abstract string SiteRedirect { get;}


        /// <summary>
        /// temp cookie retrieval 
        /// </summary>
        /// <returns></returns>
        protected async Task<Tuple<string, string>> GetTempStateAsync()
        {
            var data = await Request.GetOwinContext().Authentication.AuthenticateAsync(Shared.Constants.Cookie.TempPassiveStateName);

            var state = data.Identity.FindFirst("state").Value;
            var nonce = data.Identity.FindFirst("nonce").Value;

            return Tuple.Create(state, nonce);
        }
    }
}
