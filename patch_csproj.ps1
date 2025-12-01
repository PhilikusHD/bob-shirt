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


Write-Host "Patching WebAssembly SDK in Bob.WASM..."

$csprojFileWASM = ".\Bob.WASM\Bob.WASM.csproj"
if (-Not (Test-Path $csprojFileWASM)) {
    Write-Host "Error: $csprojFileWASM not found!"
    exit 1
}

$csprojContentWASM = Get-Content $csprojFileWASM

# Replace <Project Sdk="Microsoft.NET.Sdk"> with <Project Sdk="Microsoft.NET.Sdk.WebAssembly">
$patchedContentWASM = $csprojContentWASM -replace '<Project Sdk="Microsoft.NET.Sdk">', '<Project Sdk="Microsoft.NET.Sdk.WebAssembly">'
# Save the modified content back to the file
$patchedContentWASM | Set-Content $csprojFileWASM -Encoding UTF8

Write-Host "Successfully patched WebAssembly SDK in $csprojFileWASM!"

exit 0
