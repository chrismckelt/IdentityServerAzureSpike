
$certExists = [bool](dir cert:\LocalMachine\My\ | ? { $_.subject -like "identity.demo.local" })
if ($certExist -eq 0)
{
	Write-Host "Adding certificate - identity.demo.local"
	New-SelfSignedCertificate -DnsName 'identity.demo.local' -CertStoreLocation cert:\LocalMachine\My
}else
{
	Write-Host "------"
	Write-Host "Not adding certificate"
	Write-Host "Certificate already exists - identity.demo.local"
}