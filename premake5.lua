-- Solution Configuration
workspace "BobSolution"
    configurations { "Debug", "Release" }
    platforms { "Any CPU" }

    -- Global settings
    filter "configurations:Debug"
        defines { "DEBUG" }
        symbols "On"

    filter "configurations:Release"
        defines { "NDEBUG" }
        optimize "On"

    filter {}

-- C# Frontend Project
project "Bob"
    kind "WindowedApp"
    language "C#"
    dotnetframework "net8.0-windows"
    targetdir "bin/%{cfg.buildcfg}/x64"
    objdir "bin-int/%{cfg.buildcfg}/x64"
    location "Bob"

    files { "Bob/**.cs", "Bob/assets/**" }
    
    -- Force real folder structure instead of VS filters
    vpaths {
        ["*"] = "Bob/**",  -- Ensures all files use real folders
    }

    -- Avoid generating duplicate AssemblyInfo attributes
    clr "Off"
    flags { "ShadowedVariables" }
    linktimeoptimization "On"
    defines { "WINDOWS" }

    -- NuGet Dependencies
    nuget {
        "Avalonia:11.2.1",
        "Avalonia.Desktop:11.2.1",
        "Avalonia.Themes.Fluent:11.2.1",
        "Avalonia.Fonts.Inter:11.2.1",
        "Avalonia.Diagnostics:11.2.1",
        "SteamWebAPI2:4.4.1",
        "DotNetEnv:3.1.1"
    }

    -- Ensure assets are treated as resources
    filter { "files:Bob/assets/**" }
        buildaction "Resource"
    filter {}
