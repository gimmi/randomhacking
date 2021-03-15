## Some refs

- [Understand and invoke direct methods from IoT Hub](https://docs.microsoft.com/en-us/azure/iot-hub/iot-hub-devguide-direct-methods)
- [Communicate with edgeAgent using built-in direct methods](https://docs.microsoft.com/en-us/azure/iot-edge/how-to-edgeagent-direct-method?view=iotedge-2020-11)

## Build

```
$Version = Get-Date -Format 'yyy.MM.dd'
dotnet publish IotHubCli.csproj `
    --configuration Release `
    --runtime win-x64 `
    "/property:InformationalVersion=$Version"
    
Compress-Archive -Force `
    -Path .\bin\Release\net5.0\win-x64\publish\* `
    -DestinationPath IotHubCli.$Version.zip
```