using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace ConsoleAppProject;

public class Program
{
    public static async Task Main(string[] args)
    {
        var env = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? string.Empty;
        var logConsole = Environment.GetEnvironmentVariable("LOG_TO_CONSOLE") ?? "false";
        var logFile = Environment.GetEnvironmentVariable("LOG_TO_FILE") ?? "false";
        var logToConsole = logConsole.Equals("true", StringComparison.OrdinalIgnoreCase);
        var logToFile = logFile.Equals("true", StringComparison.OrdinalIgnoreCase);
        var timeStamp = DateTime.Now.ToString("yyyyMMddHHmmss");
        var appSettingsFile = string.IsNullOrWhiteSpace(env)
            ? "appsettings.json"
            : $"appsettings.{env}.json";

        if (logToFile)
        {
            SetupLogging(env, logToConsole, timeStamp);
        }

        var host = Host.CreateDefaultBuilder(args)
            .UseSerilog()
            .ConfigureAppConfiguration((context, config) =>
            {
                var env = context.HostingEnvironment;
                config.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);
                config.AddEnvironmentVariables();
                config.AddUserSecrets<Program>();
            })
            .ConfigureServices((context, services) =>
            {
                // Add other services here if needed
                services.AddTransient<Application>();
            }).Build();

        // Banner for current config
        Console.WriteLine("--------------------------------------------------");
        Console.WriteLine($"Environment:     {env}");
        Console.WriteLine($"Console logging: {(logToConsole ? "ON" : "OFF")}");
        Console.WriteLine($"Log file:        {GetLogPath(timeStamp)}");
        Console.WriteLine("--------------------------------------------------");
        Console.WriteLine("Press any key to continue");
        Console.ReadKey();

        using var scope = host.Services.CreateScope();
        var app = scope.ServiceProvider.GetRequiredService<Application>();
        await app.DoWork();

        if (logToFile)
        {
            Log.CloseAndFlush();
        }
    }

    private static void SetupLogging(string env, bool logToConsole, string timeStamp)
    {
        var logPath = GetLogPath(timeStamp);
        var loggerConfig = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.File(logPath, rollingInterval: RollingInterval.Day);

        Log.Logger = loggerConfig.CreateLogger();
        if (logToConsole)
        {
            loggerConfig = loggerConfig.WriteTo.Console();
        }
    }

    private static string GetLogPath(string timeStamp)
    {
        var directory = @"C:\Logs";
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
        return $@"{directory}\logfile_{timeStamp}.txt"; 
    }
}