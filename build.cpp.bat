@echo off

cd /d %~dp0

rmdir /s /q bin
rmdir /s /q obj

msbuild win32\cpp\LoopbackCapture.vcxproj /p:Configuration=Debug /p:Platform=x64 /t:Clean,Build
msbuild win32\cpp\LoopbackCapture.vcxproj /p:Configuration=Debug /p:Platform=x86 /t:Clean,Build
msbuild win32\cpp\LoopbackCapture.vcxproj /p:Configuration=Release /p:Platform=x64 /t:Clean,Build
msbuild win32\cpp\LoopbackCapture.vcxproj /p:Configuration=Release /p:Platform=x86 /t:Clean,Build
