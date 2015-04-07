REM Clearing old certs
DEL *.cer
DEL *.pfx
DEL *.pvk

REM Generating the self signed root authority
makecert -n "CN=IdentityDemoRoot" -len 2048 -a sha1 -r -sv IdentityDemoRoot.pvk IdentityDemoRoot.cer
pvk2pfx.exe -pvk IdentityDemoRoot.pvk -spc IdentityDemoRoot.cer -pfx IdentityDemoRoot.pfx

REM Creating the local SSL cert for Terminal Gateway
makecert -iv IdentityDemoRoot.pvk -n "CN=identity.demo.local" -len 2048 -a sha1 -ic IdentityDemoRoot.cer -sky exchange -pe -sv IdentityDemoLocalSSL.pvk IdentityDemoLocalSSL.cer
pvk2pfx.exe -pvk IdentityDemoLocalSSL.pvk -spc IdentityDemoLocalSSL.cer -pfx IdentityDemoLocalSSL.pfx

pause