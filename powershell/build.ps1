<# 
# help del comando Get-ChildItem
Get-Help Get-ChildItem 

# esempi di uso per il comando Get-ChildItem
Get-Help Get-ChildItem -example 

# tutti i files nella dir 'src' e sottodirectory che matchano '*.config'
Get-ChildItem src -Include packages.config -Recurse
#>

Get-ChildItem src -Include packages.config -Recurse | ForEach-Object { 
    Write-Host $_.FullName
}


