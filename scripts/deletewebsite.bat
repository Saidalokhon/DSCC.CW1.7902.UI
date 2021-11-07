%SystemRoot%\sysnative\WindowsPowerShell\v1.0\powershell.exe -command "Set-ExecutionPolicy Unrestricted -Force"


IF NOT EXIST c:\DSCC.CW1.7902.UI\scripts mkdir c:\DSCC.CW1.7902.UI\scripts
cd c:\DSCC.CW1.7902.UI\scripts
IF EXIST c:\DSCC.CW1.7902.UI\scripts\deletewebsite.ps1 %SystemRoot%\sysnative\WindowsPowerShell\v1.0\powershell.exe -command "deletewebsite.ps1"

%SystemRoot%\sysnative\WindowsPowerShell\v1.0\powershell.exe -command "rm c:\DSCC.CW1.7902.UI -Recurse -Force"
del /q "c:\DSCC.CW1.7902.UI\*.*"