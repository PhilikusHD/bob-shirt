using Avalonia;
using Bob.Core;
using Bob.Core.Logging;
using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

sealed class Program
{
    [DllImport("kernel32.dll")]
    private static extern bool AllocConsole();

    private static void OpenConsole()
    {
        // Open a new console window
        if (AllocConsole())
        {
            Console.WriteLine("Console opened! You can write here.");
        }
    }

    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args)
    {
        OpenConsole();
        Logger.InitLog();

        BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
    }


    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
}
