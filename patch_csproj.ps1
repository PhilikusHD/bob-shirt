$csprojFile = ".\Bob.Core\Bob.Core.csproj"

if (-Not (Test-Path $csprojFile)) {
    Write-Host "Error: $csprojFile not found!"
    exit 1
}

$csprojContent = Get-Content $csprojFile

# Replace <Resource Include= with <AvaloniaResource Include=
$patchedContent = $csprojContent -replace '<Resource Include=', '<AvaloniaResource Include='

# Save the modified content back to the file
$patchedContent | Set-Content $csprojFile -Encoding UTF8

Write-Host "Successfully patched Avalonia resources in $csprojFile!"
exit 0
