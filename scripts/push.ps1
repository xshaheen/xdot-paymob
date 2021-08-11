. $PSScriptRoot\common.ps1

$scriptName = $MyInvocation.MyCommand.Name

if ([string]::IsNullOrEmpty($env:NUGET_API_URL)) {
  throw "${scriptName}: NUGET_API_URL is empty or not set. Skipped pushing package(s)."
}

if ([string]::IsNullOrEmpty($env:NUGET_API_KEY)) {
  throw "${scriptName}: NUGET_API_KEY is empty or not set. Skipped pushing package(s)."
}

Push-Location

$path = Join-Path $rootPath $artifactsDir
Set-Location $path

Get-ChildItem $path -Filter "*.nupkg" | ForEach-Object {
  Write-Host "$($scriptName): Pushing $($_.Name)"
  exec { & dotnet nuget push $_ --source $env:NUGET_API_URL --api-key $env:NUGET_API_KEY }
}

Pop-Location
