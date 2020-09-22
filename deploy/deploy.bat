
cd C:\Inspec\InspecWeb


git pull 

iisreset /stop
dotnet publish InspecWeb.csproj -o publish
echo "publish success"
iisreset /start



exit