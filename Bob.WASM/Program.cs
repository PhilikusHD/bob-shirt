using Avalonia;
using Avalonia.Browser;
using Avalonia.Controls.ApplicationLifetimes;
using Bob.Core;
using Bob.Core.Logging;
using System;
using System.Runtime.Versioning;
using System.Threading.Tasks;

internal sealed partial class Program
{
    private static Task Main(string[] args)
    {
        Logger.InitLog();

        Logger.Debug("Debug Test");
        Logger.Info("Info Test");
        Logger.Warning("Warning Test");
        Logger.Error("Error Test");
        Logger.Critical("Critical Test");

        return BuildAvaloniaApp()
            .WithInterFont()
            .StartBrowserAppAsync("out");
    }

    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>();
}