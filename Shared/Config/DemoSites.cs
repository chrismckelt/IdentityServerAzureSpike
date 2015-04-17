using System.Collections.Generic;

namespace IdentityServerAzureSpike.Shared.Config
{
    public class DemoSites
    {
        public static readonly DemoSites _instance = new DemoSites();

        public static DemoSites Instance
        {
            get { return _instance; }
        }

        static DemoSites()
        {
        }

        private DemoSites()
        {
            Create();
        }

        public DemoSite A { get; private set; }
        public DemoSite B { get; private set; }
        public DemoSite C { get; private set; }
        public DemoSite D { get; private set; }
        public DemoSite E { get; private set; }

        public void Create()
        {
            A = new DemoSite
            {
                Name = "SiteA_Hybrid",
                Uri = "http://sitea.demo.local:9556"
            };
            B = new DemoSite
            {
                Name = "SiteB_Hybrid",
                Uri = "http://SiteB.demo.local:9557"
            };
            C = new DemoSite
            {
                Name = "SiteC_ImplicitFlow",
                Uri = "http://sitec.demo.local:9558"
            };
            D = new DemoSite
            {
                Name = "SiteD_CodeFlow",
                Uri = "http://SiteD.demo.local:9559"
            };
            E = new DemoSite
            {
                Name = "SiteE_CodeFlow",
                Uri = "http://sitee.demo.local:9560"
            };
        }
    }

}
