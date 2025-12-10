using ConsoleAppProject.Menus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ConsoleAppProject;

public class Application
{
    private readonly MainMenu _menu;
    private readonly IConfiguration _configuration;
    public const int LINE_LENGTH = 40;
    private readonly ILogger<Application> _logger;
    public Application(IConfiguration configuration, ILogger<Application> logger)
    {
        _menu = new MainMenu();
        _configuration = configuration;
        _logger = logger;

        var itWorks = _configuration["Test:Setting1"];
        Console.WriteLine($"Configuration Test in Application: ItWorks = {itWorks}");
        _logger.LogInformation("Application initialized with configuration setting ItWorks = {ItWorks}", itWorks);
    }

    public async Task DoWork()
    {
        Console.WriteLine("Welcome to the YourProjectNameHere");

        await _menu.ShowAsync("Main Menu");

        Console.WriteLine("Thank you for using the YourProjectNameHere");
    }
}
