# Common Paths
$rootPath = (Get-Item -Path "$PSScriptRoot/..").FullName
$artifactsDir = ".artifacts"
$solutionsDirs = ("")


<#
.SYNOPSIS
Helper function for executing command-line programs.
.DESCRIPTION
This is a helper function that runs a scriptblock and checks the PS variable $lastexitcode to see if an error occcured.
If an error is detected then an exception is thrown.
This function allows you to run command-line programs without having to explicitly check fthe $lastexitcode variable.

Taken from psake https://github.com/psake/psake
.EXAMPLE
exec { svn info $repository_trunk } "Error executing SVN. Please verify SVN command-line client is installed"
#>
function Exec {
  [CmdletBinding()]
  param(
    [Parameter(Position = 0, Mandatory = 1)][scriptblock]$cmd,
    [Parameter(Position = 1, Mandatory = 0)][string]$errorMessage = ($msgs.error_bad_command -f $cmd)
  )
  & $cmd
  if ($lastexitcode -ne 0) {
    throw ("Exec: " + $errorMessage)
  }
}
