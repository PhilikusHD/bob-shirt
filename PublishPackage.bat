@echo off
REM Dual-target publish: Desktop (win-x64) and WASM (browser-wasm)

REM Base output folder
set "BASE_DIR=.\build\"

REM Ensure directories exist
mkdir "%BASE_DIR%\Desktop"
mkdir "%BASE_DIR%\WASM"

REM Publish Desktop
echo Publishing Desktop (win-x64)...
dotnet publish Bob.Desktop\Bob.Desktop.csproj -c Release -r win-x64 --self-contained true -o "%BASE_DIR%\Desktop"
if errorlevel 1 (
    echo Desktop publish failed.
    exit /b 1
)
echo ✅ Desktop build complete at %BASE_DIR%\Desktop

REM Publish WASM
REM TODO: Figure out why AOT and browser-wasm are not working together.
echo Publishing WASM (browser-wasm)...
dotnet publish Bob.WASM\Bob.WASM.csproj -c Release -r browser-wasm --self-contained true -o "%BASE_DIR%\WASM"
if errorlevel 1 (
    echo WASM publish failed.
    exit /b 1
)
echo WASM build complete at %BASE_DIR%\WASM

echo =========================================================
echo All builds complete! Output folder: %BASE_DIR%
pause
