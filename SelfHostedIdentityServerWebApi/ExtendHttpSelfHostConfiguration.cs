using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Web.Http.SelfHost;
using System.Web.Http.SelfHost.Channels;

namespace SelfHostedIdentityServerWebApi
{
    internal class ExtendHttpSelfHostConfiguration : HttpSelfHostConfiguration
    {
        public ExtendHttpSelfHostConfiguration(string baseAddress) : base(baseAddress)
        {
        }

        protected override BindingParameterCollection OnConfigureBinding(HttpBinding httpBinding)
        {
            httpBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Certificate;

            httpBinding.Security.Mode = HttpBindingSecurityMode.Transport;

            return base.OnConfigureBinding(httpBinding);
        }
    }
}
