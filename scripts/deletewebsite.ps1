Import-Module WebAdministration
$iisAppPoolName = "DSCC.CW1.7902.UI"
$iisAppName = "DSCC.CW1.7902.UI"

#navigate to the sites root
cd IIS:\Sites\

#check if the site exists
if (Test-Path $iisAppName -pathType container)
{
    Stop-WebSite $iisAppName
	Remove-Website $iisAppName
}

#navigate to the app pools root
cd IIS:\AppPools\

#check if the app pool exists
if (!(Test-Path $iisAppPoolName -pathType container))
{
	Remove-WebAppPool $iisAppPoolName
}