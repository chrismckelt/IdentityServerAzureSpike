using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace IdentityServerAzureSpike.Shared.Config
{
    public static class Certificate
    {
        public static X509Certificate2 Get()
        {
            var assembly = typeof(Certificate).Assembly;
            using (var stream = assembly.GetManifestResourceStream("IdentityServerAzureSpike.Shared.IdentityDemoLocalSSL.pfx"))
            {
                return new X509Certificate2(ReadStream(stream));
            }
        }

        private static byte[] ReadStream(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}
