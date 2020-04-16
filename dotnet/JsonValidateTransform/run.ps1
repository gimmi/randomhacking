$ExePath = (Get-Command -Name dotnet -CommandType Application).Source
$PrjPath = "$PSScriptRoot\JsonValidateTransform.csproj"
$InPath = "$PSScriptRoot\in"
$OutPath = "$PSScriptRoot\out"

$RunArgs = @('run', '--configuration', 'Release', '--project', $PrjPath, '--', '--in', $InPath, '--out', $OutPath) + $args
& $ExePath $RunArgs
