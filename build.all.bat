@echo off

cd /d %~dp0

if ".%ZIP_TOOL%"=="." echo "Please set %%ZIP_TOOL%% with your 7za.exe location." & goto :eof

if NOT ".%~1"=="." set "LC_VER=_%~1"

FOR %%a in (cpp csharp dll) do (
    call build.%%a.bat
    call :ZIP %%a
)
goto :eof

:ZIP
    %ZIP_TOOL% a .\zip\LoopbackCapture_%~1%LC_VER%_x64_debug.zip .\bin\x64\Debug\*
    %ZIP_TOOL% a .\zip\LoopbackCapture_%~1%LC_VER%_x64_release.zip .\bin\x64\Release\*
    %ZIP_TOOL% a .\zip\LoopbackCapture_%~1%LC_VER%_x86_debug.zip .\bin\x86\Debug\*
    %ZIP_TOOL% a .\zip\LoopbackCapture_%~1%LC_VER%_x86_release.zip .\bin\x86\Release\*
    goto :eof
