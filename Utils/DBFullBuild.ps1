param(
    [string]$ServerInstance
)

$scripts = @(
    @{path = "Scripts\Schema\Drop_Database.sql"; description = "Drop existing Database"; database = "master"},
    @{path = "Scripts\Schema\Create_Database.sql"; description = "Create New Database"; database = "master"},
    @{path = "Scripts\Schema\PrimaryKeys.sql"; description = "Create Primary Keys"; database = "bobshirt"},
    @{path = "Scripts\Schema\ForeignKeys.sql"; description = "Create Foreign Keys"; database = "bobshirt"},
    @{path = "Scripts\Seed\Fill_Customer.sql"; description = "Fill Customer Data"; database = "bobshirt"}
)

function Invoke-SqlScript ($path, $description, $database){
    Write-Host "`n=== Running: $description ==="
     try {
        $sqlScript = Get-Content -Path $path -Raw
        Invoke-Sqlcmd `
            -Query $sqlScript `
            -ServerInstance $ServerInstance `
            -Database $database `
            -TrustServerCertificate `        
    }
    catch {
        Write-Error "$description failed: $_"
        exit 1
    }
}


if (-not $ServerInstance) {
    $services = Get-Service | Where-Object { $_.Name -like "MSSQL$*" -or $_.Name -eq "MSSQLSERVER" }
    if ($services.Count -eq 0) {
        Write-Error "No SQL Server instance found."
        exit 1
    }

    # pick the first running service
    $service = $services | Where-Object { $_.Status -eq "Running" } | Select-Object -First 1
    if ($service.Name -eq "MSSQLSERVER") {
        $ServerInstance = "localhost"
    } else {
        $instanceName = $service.Name -replace "^MSSQL\$", ""
        $ServerInstance = "localhost\$instanceName"
    }
    Write-Host "Using detected SQL Server instance: $ServerInstance"
}


foreach ($script in $scripts) {
    Invoke-SqlScript @script
}