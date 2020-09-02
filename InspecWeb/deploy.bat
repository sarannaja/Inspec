REM cmd.exe /c "start cmd.exe /c pause"

:choice
set /P c= git pull ? [Y/N]
if /I "%c%" EQU "N" goto :somewhere
if /I "%c%" EQU "Y" goto :somewhere_else
goto :choice


:somewhere
git pull 
echo "git pull"

iisreset /stop
echo "stop server"

dotnet publish InspecWeb.csproj -o publish
echo "publish success"
iisreset /start
echo "start server"



pause 
exit

:somewhere_else

echo "not pull git"

iisreset /stop
echo "stop server"

dotnet publish InspecWeb.csproj -o publish
echo "publish success"
iisreset /start
echo "start server"

pause