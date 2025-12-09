using ConsoleAppProject.Menus;
using Microsoft.Extensions.Configuration;

namespace ConsoleAppProject;

public class Application
{
    private readonly MainMenu _menu;
    private readonly IConfiguration _configuration;
    public const int LINE_LENGTH = 40;

    public Application(IConfiguration configuration)
    {
        _menu = new MainMenu();
        _configuration = configuration;

        var itWorks = _configuration["Test:Setting1"];
        Console.WriteLine($"Configuration Test in Application: ItWorks = {itWorks}");
    }

    public async Task DoWork()
    {
        Console.WriteLine("Welcome to the YourProjectNameHere");

        await _menu.ShowAsync("Main Menu");

        Console.WriteLine("Thank you for using the YourProjectNameHere");
    }
}
