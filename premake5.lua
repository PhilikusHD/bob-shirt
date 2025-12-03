workspace "BobSolution"
    configurations { "Debug", "Release" }
    platforms { "Any CPU" }

    -- Global configuration filters
    filter "configurations:Debug"
        defines { "DEBUG" }
        symbols "On"
    filter "configurations:Release"
        defines { "NDEBUG" }
        optimize "On"
    filter {}


project "Bob.Core"
    kind "SharedLib"
    language "C#"
    dotnetframework "net9.0"
    targetdir "bin/%{cfg.buildcfg}/x64/Core"
    objdir "bin-int/%{cfg.buildcfg}/x64/Core"
    location "Bob.Core"

    files { "Bob.Core/**.cs", "Bob.Core/assets/**" }

    -- Treat assets and XAML as resources
    filter { "files:Bob.Core/Assets/**" }
        buildaction "Resource"
    filter { "files:**.xaml" }
        buildaction "Page"
    filter {}

    nuget {
        "Avalonia:11.3.9",
        "Avalonia.Themes.Fluent:11.3.9",
        "Avalonia.Fonts.Inter:11.3.9",
        "DotNetEnv:3.1.1",
        "CommunityToolkit.Mvvm:8.2.0",
        "linq2db:5.4.1.9",
        "Microsoft.Data.SqlClient:5.1.3"
    }

project "Bob.Desktop"
    kind "WindowedApp"
    language "C#"
    dotnetframework "net9.0-windows"
    targetdir "bin/%{cfg.buildcfg}/x64/Desktop"
    objdir "bin-int/%{cfg.buildcfg}/x64/Desktop"
    location "Bob.Desktop"

    files { "Bob.Desktop/**.cs" }

    vpaths { ["*"] = "Bob.Desktop/**" }

    clr "Off"
    flags { "ShadowedVariables" }
    linktimeoptimization "On"
    defines { "WINDOWS" }

    nuget {
        "Avalonia.Desktop:11.3.9",
        "Avalonia.Diagnostics:11.3.9"
    }

    -- Reference shared Core
    links { "Bob.Core" }


project "Bob.WASM"
    kind "ConsoleApp"
    language "C#"
    dotnetframework "net9.0-browser"
    
    targetdir "bin/%{cfg.buildcfg}/x64/WASM"
    objdir "bin-int/%{cfg.buildcfg}/x64/WASM"
    location "Bob.WASM"

    files { "Bob.WASM/**.cs" }
    vpaths { ["*"] = "Bob.WASM/**" }

    clr "Off"
    flags { "ShadowedVariables" }
    linktimeoptimization "On"
    defines { "WASM" }

    nuget {
        "Avalonia.Browser:11.3.9",
        "Microsoft.NET.Sdk.WebAssembly.Pack:10.0.0"
    }

    links { "Bob.Core" }