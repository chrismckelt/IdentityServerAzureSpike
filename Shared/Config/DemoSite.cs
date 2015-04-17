using System.Collections.Generic;
using System.Linq;

namespace IdentityServerAzureSpike.Shared.Config
{
    public class DemoSite
    {
        private string _uri;

        public string Name { get; set; }

        public string Uri
        {
            get { return _uri; }
            set
            {
                _uri = value;
                SetRedirectUris();
            }
        }

        public string HybridUri
        {
            get { return RedirectUris.First(a => a.Contains(Constants.RedirectUri.HybridFlow)); }
        }

        public string ImplicitUri
        {
            get { return RedirectUris.First(a => a.Contains(Constants.RedirectUri.ImplicitFlow)); }
        }

        public string CodeUri
        {
            get { return RedirectUris.First(a => a.Contains(Constants.RedirectUri.CodeFlow)); }
        }

        public List<string> RedirectUris { get; private set; }

        private void SetRedirectUris()
        {
            var redirects = new List<string>
            {
                _uri,
                Constants.RedirectUri.Build(Constants.RedirectUri.FlowType.Hybrid, _uri),
                Constants.RedirectUri.Build(Constants.RedirectUri.FlowType.Implicit, _uri),
                Constants.RedirectUri.Build(Constants.RedirectUri.FlowType.Code, _uri)
            };

            RedirectUris = redirects;
        }
    }
}