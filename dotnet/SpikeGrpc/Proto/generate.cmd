SETLOCAL
SET TOOLS_PATH=%userprofile%\.nuget\packages\grpc.tools\1.13.1\tools\windows_x64
"%TOOLS_PATH%\protoc.exe" "--proto_path=%~dp0." "--csharp_out=%~dp0." "--plugin=protoc-gen-grpc=%TOOLS_PATH%\grpc_csharp_plugin.exe" "--grpc_out=%~dp0." contracts.proto 
