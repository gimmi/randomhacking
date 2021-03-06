<# 
# help del comando Get-ChildItem
Get-Help Get-ChildItem 

# esempi di uso per il comando Get-ChildItem
Get-Help Get-ChildItem -example 

# ispezionare una variabile
Get-member -InputObject $xx
(Get-Content .\template.html).GetType()

# tutti i files nella dir 'src' e sottodirectory che matchano '*.config'
Get-ChildItem src -Include packages.config -Recurse

# leggere il contenuto di un file, ritorna un array di stringhe (una per riga letta)
Get-Content $xx 

# caricare assemblies
# http://www.dougfinke.com/blog/index.php/2010/08/29/how-to-load-net-assemblies-in-a-powershell-session/
Add-Type -AssemblyName System.Web
Add-Type -Path "c:\windows\system32\inetsrv\microsoft.web.administration.dll"
Add-Type -TypeDefinition @"
public class Test
{
    public static int Add(int n1, int n2)
    {
        return n1 + n2;
    }
}
"@

# string manipulation
http://www.roelvanlisdonk.nl/?p=1153
http://tasteofpowershell.blogspot.it/2009/08/string-manipulation-splitting-and.html

#escape chars
"\n" in c# corrisponde a "`n" in powershell

# scrivere files
http://blogs.technet.com/b/gbordier/archive/2009/05/05/powershell-and-writing-files-how-fast-can-you-write-to-a-file.aspx
$a | out-file ".\test.txt"

# parametri per fnzioni/commandline
https://devcentral.f5.com/weblogs/Joe/archive/2009/01/13/powershell-abcs---p-is-for-parameters.aspx

# includere files dalla directoryu corrente
# http://stackoverflow.com/questions/4724290/powershell-run-command-from-scripts-directory
$currentPath=Split-Path ((Get-Variable MyInvocation -Scope 0).Value).MyCommand.Path
import-module "$currentPath\sqlps.ps1"

#>
<#
Get-ChildItem src -Include packages.config -Recurse | ForEach-Object {
    Write-Host $_.FullName
}

#>
param($SrcDir = ".\src", $WikiDir = ".\wiki", $TemplateFile = ".\template.html")

function BuildWikiPage($SrcFile, $TemplateFile, $DestFile) {
    $SrcFile = Resolve-Path $SrcFile
    $TemplateFile = Resolve-Path $TemplateFile
    $DestFile = Resolve-Path $DestFile
    Write-Host $SrcFile " => " $DestFile

    Add-Type -Path "MarkdownSharp.1.13.0.0\lib\35\MarkdownSharp.dll"
    $MarkdownOptions = New-Object MarkdownSharp.MarkdownOptions
    $Markdown = New-Object MarkdownSharp.Markdown $MarkdownOptions

    $TemplateContent = (Get-Content $TemplateFile) -join "`n"
    $SrcContent = (Get-Content $SrcFile) -join "`n"
    $SrcContent = $Markdown.Transform($SrcContent)
    $DestContent = $TemplateContent -f $SrcContent
    
    $DestContent | Out-File  $DestFile
}

$TemplateContent = (Get-Content $TemplateFile) -join "`n"

Get-ChildItem $SrcPath -Include *.md -Recurse | ForEach-Object {

    $DestFileName = Join-Path $WikiDir $_.BaseName
    $DestFileName += ".html"

    BuildWikiPage -SrcFile $_ -TemplateFile $TemplateFile -DestFile $DestFileName
}


