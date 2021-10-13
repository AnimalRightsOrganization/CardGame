@echo off
rem echo %cd% 当前目录

set root=%cd%
echo root path is: %root%

set src=%root%/Proto
echo src path is: %src%

set output_client=%root%/Client/Assets/Scripts/Shared/Messages
echo output_client path is: %output_client%

set output_server=%root%/Server/NetCoreApp/Messages
echo output_server path is: %output_server%

rem set output=%cd%/Proto
rem echo output path is: %output%
rem if not exist %output% (md Proto) else (echo exist)
rem pause

rem cd include/google/protobuf
cd Proto

for %%i in (*.proto) do (
    protoc --csharp_out=%output_client%/ %%i
    protoc --csharp_out=%output_server%/ %%i
    rem 从这里往下都是注释，可忽略
    echo From %%i To %%~ni.cs Successfully!  
)
pause