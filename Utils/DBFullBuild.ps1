param(
    [string]$ServerInstance = "CW-INTEG\MSSQLSERVER01"
)

$scripts = @(
    @{path = Join-Path $PSScriptRoot "..\Scripts\Schema\Drop_Database.sql"; description =  "Drop existing Database"},
    @{path = Join-Path $PSScriptRoot "..\Scripts\Schema\Create_Database.sql"; description =  "create New Database"},
    @{path = Join-Path $PSScriptRoot "..\Scripts\Schema\ForeignKeys.sql"; description =  "create Primary Keys"}
    @{path = Join-Path $PSScriptRoot "..\Scripts\Schema\PrimaryKeys.sql"; description =  "create Foreign Keys"}
    @{path = Join-Path $PSScriptRoot "..\Scripts\Seed\Fill_Customer.sql"; description =  "fill Customer Data"}
)

function Invoke-SqlScript ($path, $description){
    Write-Host "`n=== Running: $description ==="

     try {
        $sqlScript = Get-Content -Path $path -Raw
        Invoke-Sqlcmd `
            -Query $sqlScript `
            -ServerInstance $ServerInstance `
            -Database "bobshirt" `
            -TrustServerCertificate `
}
catch {
        Write-Error "$description failed: $_"
        exit 1
    }

foreach ($script in $scripts) {
    Invoke-SqlScript @script
}