[CmdletBinding(DefaultParameterSetName="DBConnection")]
param
(
    [Parameter(ParameterSetName="DBConnection", Mandatory=$true)]
    [Alias("si")]
    [string]$SQLServerInstance,

    [Parameter(ParameterSetName="DBConnection", Mandatory=$true)]
    [Alias("dn")]
    [string]$DatabaseName,

    [Parameter(ParameterSetName="DBConnection", Mandatory=$true)]
    [Alias("du")]
    [string]$DatabaseUser,

    [Parameter(ParameterSetName="DBConnection")]
    [Alias("dp")]
    [string]$DatabasePassword = 'PROMPT',

    [ValidateSet("update", "updateSQL", "rollback", "clearchecksums", "status", "validate", "releaselocks", "generateChangeLog", "generateChangeLogData")]
    [Alias("c")]
    [string]$Command = "update",

    [Parameter(ParameterSetName="ConnectionString", Mandatory=$true)]
    [Alias("cs")]
    [string]$ConnectionString,
	
    [Alias("q")]
    [switch]$Quiet = $false
)


# Halt on any cmdlet error
$ErrorActionPreference = "Stop"


# Configuration
Split-Path -Parent $MyInvocation.MyCommand.Definition | Set-Variable BASE_PATH -Option Constant
Join-Path "$BASE_PATH" "bin\liquibase-3.5.5\liquibase.jar" | Set-Variable LIQUIBASE_PATH
Join-Path "$BASE_PATH" "bin\sqljdbc-6.0\sqljdbc41.jar" | Set-Variable JDBC_DRIVER_PATH
###############


# Functions
function New-DatabaseUrl($dbServer, $dbName)
{
    return "jdbc:sqlserver://${dbServer};databaseName=${dbName}"
}

function New-ChangeLogFile($myCommand)
{
    switch ($myCommand)
    {
        "generateChangeLog"
        {
            return "${BASE_PATH}\ChangeLogFilesGenerated\${DatabaseName}SchemaExport_$(Get-Date -format "yyyyMMddHHmm").xml"
        }
        "generateChangeLogData"
        {
            return "${BASE_PATH}\ChangeLogFilesGenerated\${DatabaseName}DataExport_$(Get-Date -format "yyyyMMddHHmm").xml"
        }
        default
        {
            return "${BASE_PATH}\ChangeLogFiles\Master.xml"
        }
    }
}

function New-LiquibaseCommand($dbUser, $dbPassword, $myCommand)
{
	$commandLine = "java -jar `"${LIQUIBASE_PATH}`""

	# Not the -cp/-classpath option for java, this is an argument to liquibase.jar!
	$commandLine += " --classpath=`"${JDBC_DRIVER_PATH}`""

	if ($ConnectionString) {
		$commandLine += " --url=`"jdbc:sqlserver://${ConnectionString}`""
	}
	else {
		$dbUrl = New-DatabaseUrl $SQLServerInstance $DatabaseName
		$commandLine += " --url=`"${dbUrl}`""
		$commandLine += " --username=`"${DatabaseUser}`""
		$commandLine += " --password=`"${DatabasePassword}`""
	}
	$changeLogFile = New-ChangeLogFile $myCommand
	$commandLine += " --changeLogFile=`"${changeLogFile}`""

        if ($Quiet)
        {
            $commandLine += " --logLevel=warning"
        }
        else
        {
            $commandLine += " --logLevel=info"
        }

	$liquibaseCommand = ConvertTo-LiquibaseCommand $myCommand
	$commandLine += " ${liquibaseCommand}"

	return $commandLine
}

function ConvertTo-LiquibaseCommand($myCommand)
{
    switch ($myCommand)
    {
        #Updates database to current version
        "update" { return "update" }

        #Rolls back the last one change sets applied to the database
        "rollback" { return "rollbackCount 1" }
    }
}
###########


# Main script
$commandLine = New-LiquibaseCommand $DatabaseUser $DatabasePassword $Command

Write-Host $commandLine
Invoke-Expression $commandLine

if ($LASTEXITCODE -Ne 0)
{
	Write-Error "Failed with exit code ${LASTEXITCODE}"
}
