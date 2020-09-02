REM cmd.exe /c "start cmd.exe /c pause"

:choice
set /P c= git pull ? [Y/N]
if /I "%c%" EQU "N" goto :somewhere
if /I "%c%" EQU "Y" goto :somewhere_else
goto :choice


:somewhere
git pull 
echo "ดึงจาก git เสร็จแล้ว ฮึฮึ"

iisreset /stop
echo "stop server แล้วเว้ยเออเอาดิ"

dotnet publish InspecWeb.csproj -o publish
echo "publish เสร็จแล้วรอแปบกำลัง start ใหม่"
iisreset /start
echo "start เสร็จแล้วเว้ย หึหึ"



pause 
exit

:somewhere_else

echo "นี่มึงไม่ดึงอย่างนั้นหรอแน่ใจแล้วใช่มั้ย"

iisreset /stop
echo "stop server แล้วเว้ยเออเอาดิ"

dotnet publish InspecWeb.csproj -o publish
echo "publish เสร็จแล้วรอแปบกำลัง start ใหม่"
iisreset /start
echo "start เสร็จแล้วเว้ย หึหึ"

pause