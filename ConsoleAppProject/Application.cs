using ConsoleAppProject.Menus;

namespace ConsoleAppProject;

public class Application
{
    private readonly MainMenu _menu;

    public Application()
    {
        _menu = new MainMenu();
    }

    public async Task DoWork()
    {
        Console.WriteLine("Welcome to the YourProjectNameHere");

        await _menu.ShowAsync();

        Console.WriteLine("Thank you for using the YourProjectNameHere");
    }
}
