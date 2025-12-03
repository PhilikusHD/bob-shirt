using Avalonia;
using Bob.Core;
using Bob.Core.Database;
using Bob.Core.Domain;
using Bob.Core.Logging;
using Bob.Core.Services;
using LinqToDB;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
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
    public static async Task Main(string[] args)
    {
        OpenConsole();
        Logger.InitLog();

        Logger.Debug("Debug Test");
        Logger.Info("Info Test");
        Logger.Warning("Warning Test");
        Logger.Error("Error Test");
        Logger.Critical("Critical Test");

        // Quick DB connection test: fetch all customers and print them to the console.
        /*
        try
        {
            // using var db = new AppDataConnection();
            CustomerService customerService = new CustomerService();

            // Logger.Info("Created DB Connection.");
            var customers = await customerService.GetAllCustomersAsync();

            Logger.Info($"DB Connection OK — fetched {customers.Count} customers.");
            foreach (var c in customers)
            {
                Logger.Debug($"Customer: {c.Id} - {c.Name} {c.Surname} <{c.Email}>");
            }



            Customer newCustomer = new Customer(customers.Last().Id + 1, "Bob", "Bobby", "bobbybobob@email.com", 3, "000-0000", DateTime.Now);
            Logger.Info("Adding new Customer...");
            await customerService.AddCustomerAsync(newCustomer);

            customers = await customerService.GetAllCustomersAsync();
            Logger.Info($"DB Connection OK — fetched {customers.Count} customers.");
            foreach (var c in customers)
            {
                Logger.Debug($"Customer: {c.Id} - {c.Name} {c.Surname} <{c.Email}>");
            }
        }
        catch (Exception ex)
        {
            Logger.Error($"DB connection test failed: {ex.InnerException?.Message ?? ex.Message}");
            // Console.WriteLine($"DB connection test failed: {ex.Message}");
        }
        */

        BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);

    }


    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
}
