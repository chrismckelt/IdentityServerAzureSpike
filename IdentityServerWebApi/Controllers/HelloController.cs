using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace IdentityServerAzureSpike.IdentityServerWebApi.Controllers
{
    [RoutePrefix("hello")]
    public sealed class HelloController : ApiController
    {

        public string Get()
        {
            return "Hello World";
        }
    }
}