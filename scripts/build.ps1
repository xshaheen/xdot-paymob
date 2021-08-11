. $PSScriptRoot\common.ps1

if (Test-Path $artifactsDir) { Remove-Item $artifactsDir -Force -Recurse }

Write-Host ""
Write-Host "###   Clean Sln   ###"
exec { & dotnet clean -c Release }

Write-Host ""
Write-Host "###   Build Sln   ###"
exec { & dotnet build -c Release }

Write-Host ""
Write-Host "###      Pack     ###"
exec { & dotnet pack -c Release -o $artifactsDir --no-build }
