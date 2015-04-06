// Copyright 2015 Finsa S.p.A. <finsa@finsa.it>
// 
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except
// in compliance with the License. You may obtain a copy of the License at:
// 
// "http://www.apache.org/licenses/LICENSE-2.0"
// 
// Unless required by applicable law or agreed to in writing, software distributed under the License
// is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express
// or implied. See the License for the specific language governing permissions and limitations under
// the License.

using System.Web.Http;
using Finsa.WebApi.HelpPage.AnyHost;

namespace IdentityServerAzureSpike.SiteA.Controllers
{
    /// <summary>
    ///   The HELP controller.
    /// </summary>
    /// <remarks>Adjust routing prefix according to your own needs.</remarks>
    [RoutePrefix("api/help")]
    public sealed class HelpController : HelpControllerBase
    {
        /// <summary>
        ///   Override here default info.
        /// </summary>
        public HelpController()
        {
            Info.MyServiceName = "wsExample";
        }
    }
}