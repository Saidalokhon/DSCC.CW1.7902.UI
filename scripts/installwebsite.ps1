Import-Module WebAdministration
$iisAppPoolName = "DSCC.CW1.7902.UI"
$iisAppPoolDotNetVersion = "v4.0"
$iisAppName = "DSCC.CW1.7902.UI"
$directoryPath = "C:\DSCC.CW1.7902.UI"

#stop the default web site so we can use port :80
Stop-WebSite 'DSCC.CW1.7902.UI'

#set the autostart property so we don't have the default site kick back on after a reboot
cd IIS:\Sites\
Set-ItemProperty 'DSCC.CW1.7902.UI' serverAutoStart False

#navigate to the app pools root
cd IIS:\AppPools\

#check if the app pool exists
if (!(Test-Path $iisAppPoolName -pathType container))
{
    #create the app pool
    $appPool = New-Item $iisAppPoolName
    $appPool | Set-ItemProperty -Name "managedRuntimeVersion" -Value $iisAppPoolDotNetVersion
}

#navigate to the sites root
cd IIS:\Sites\

#check if the site exists
if (Test-Path $iisAppName -pathType container)
{
    return
}

#create the site
$iisApp = New-Item $iisAppName -bindings @{protocol="http";bindingInformation=":80:"} -physicalPath $directoryPath
$iisApp | Set-ItemProperty -Name "applicationPool" -Value $iisAppPoolName
Set-ItemProperty $iisAppName serverAutoStart True