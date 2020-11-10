echo off
cd C:\Inspec\InspecWeb


git pull 

iisreset /stop
dotnet publish InspecWeb.csproj -o C:\publish
echo "publish success"
iisreset /start


pause

exit