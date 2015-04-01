$scriptPath = $MyInvocation.MyCommand.Path
$scriptDirectory = Split-Path $scriptPath
$hostsFile = "C:\Windows\System32\drivers\etc\hosts"
$msBuild = "C:\Windows\Microsoft.NET\Framework64\v4.0.30319\msbuild.exe"
$config_iisIpAddress = "127.0.0.1"

#demo specific
$identity_server = "identity.demo.local"
$site_a = "siteA.demo.local"
$site_b = "siteB.demo.local"
$site_c = "siteC.demo.local"
#$identity_server_port = "9555"
#$site_a_port = "9556"
#$site_b_port = "9557"
#$site_c_port = "9558"
$identity_server_port = ""
$site_a_port = ""
$site_b_port = ""
$site_c_port = ""


function elevate-script($path) {

    # Get the ID and security principal of the current user account
    $myWindowsID=[System.Security.Principal.WindowsIdentity]::GetCurrent()
    $myWindowsPrincipal=new-object System.Security.Principal.WindowsPrincipal($myWindowsID)
 
    # Get the security principal for the Administrator role
    $adminRole=[System.Security.Principal.WindowsBuiltInRole]::Administrator
 
    # Check to see if we are currently running "as Administrator"
    if ($myWindowsPrincipal.IsInRole($adminRole))
    {
        # We are running "as Administrator" - so change the title and background color to indicate this
        $Host.UI.RawUI.WindowTitle = $path + "(Elevated)"
        clear-host
    }
    else
    {
        # We are not running "as Administrator" - so relaunch as administrator
        Write-Host "Your are not elevated. Launching window with administrator privileges"
   
        # Create a new process object that starts PowerShell
        $newProcess = new-object System.Diagnostics.ProcessStartInfo "PowerShell";
   
        # Specify the current script path and name as a parameter
        $newProcess.Arguments = $path

        # Indicate that the process should be elevated
        $newProcess.Verb = "runas";
   
        # Start the new process
        $swallow = [System.Diagnostics.Process]::Start($newProcess);
   
        # Exit from the current, unelevated, process
        break
    }
}

elevate-script -path ($myInvocation.MyCommand.Definition + " " + $args)

function print-usage() {
    Write-Warning "Usage:  .\ApplicationInstallation.ps1"
    Write-Warning " "
}

#Administrator privileges check
If (-NOT ([Security.Principal.WindowsPrincipal] [Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole(`
    [Security.Principal.WindowsBuiltInRole] "Administrator"))
{
    Write-Warning "You do not have Administrator rights to run this script!`nPlease re-run this script as an Administrator!"
    exit
}

# Adds an IP and hostname mapping to the given hosts file
function add-host([string]$filename, [string]$ip, [string]$hostname, [string]$port) {
    Write-Host "Adding hosts file entry:" $ip `t`t $hostName `t`t $port 	
    remove-host $filename $hostname	
	$ip + "`t`t" + $hostname + "`t`t" + $port | Out-File -encoding ASCII -append $filename
}

# Removes a specified hostname from the given hosts file
function remove-host([string]$filename, [string]$hostname) {
	$c = Get-Content $filename
	$newLines = @()

	foreach ($line in $c) {
		$bits = [regex]::Split($line, "\t+")
		if ($line -match $hostname) {
			
		} else {
			$newLines += $line
		}
	}

	# Write file
	Clear-Content $filename
	foreach ($line in $newLines) {
		$line | Out-File -encoding ASCII -append $filename
	}
}

# Add sites
add-host -filename $hostsFile -ip $config_iisIpAddress -hostname $identity_server $identity_server_port
add-host -filename $hostsFile -ip $config_iisIpAddress -hostname $site_a $site_a_port 
add-host -filename $hostsFile -ip $config_iisIpAddress -hostname $site_b $site_b_port
add-host -filename $hostsFile -ip $config_iisIpAddress -hostname $site_c $site_c_port

$certExists = [bool](dir cert:\LocalMachine\My\ | ? { $_.subject -like "cn=identity.demo.local*" })
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


netsh http add urlacl url=https://identity.demo.local/ user=everyone
netsh http add urlacl url=https://sitea.demo.local/ user=everyone
netsh http add urlacl url=https://siteb.demo.local/ user=everyone
netsh http add urlacl url=https://sitec.demo.local/ user=everyone
netsh firewall add portopening TCP 443 IISExpressWeb enable ALL

Write-Host You have been kickstarted!
