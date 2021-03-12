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