CertUtil -addstore -f -v root IdentityDemoRoot.cer
CertUtil -f -p "" -importpfx IdentityDemoLocalSSL.pfx