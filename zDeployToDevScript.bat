REM @echo off
del winscpScriptLog01.txt

cd BuildArtifacts\_PublishedWebsites\playground
WinSCP.exe /script="..\..\..\winscpToDevScript.txt" /log=winscpScriptLog01.txt /console /parameter iis-01.dev %1
if %errorlevel% neq 0 exit /b %errorlevel%
