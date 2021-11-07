%SystemRoot%\sysnative\WindowsPowerShell\v1.0\powershell.exe -command "Set-ExecutionPolicy Unrestricted -Force"

IF NOT EXIST c:\DSCC.CW1.7902.UI mkdir c:\DSCC.CW1.7902.UI
cd c:DSCC.CW1.7902.UI\scripts

IF EXIST c:\DSCC.CW1.7902.UI\scripts\installwebsite.ps1 %SystemRoot%\sysnative\WindowsPowerShell\v1.0\powershell.exe -command "installwebsite.ps1"