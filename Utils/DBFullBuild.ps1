param(
    [string]$ServerInstance = "CW-INTEG\MSSQLSERVER01"
)

$scripts = @(
    @{path = "..\Scripts\Schema\Drop_Database.sql"; description = "Drop existing Database"; database = "master"},
    @{path = "..\Scripts\Schema\Create_Database.sql"; description = "Create New Database"; database = "master"},
    @{path = "..\Scripts\Schema\PrimaryKeys.sql"; description = "Create Primary Keys"; database = "bobshirt"},
    @{path = "..\Scripts\Schema\ForeignKeys.sql"; description = "Create Foreign Keys"; database = "bobshirt"},
    @{path = "..\Scripts\Seed\Fill_Customer.sql"; description = "Fill Customer Data"; database = "bobshirt"}
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
foreach ($script in $scripts) {
    Invoke-SqlScript @script
}