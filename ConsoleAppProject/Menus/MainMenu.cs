using ConsoleHelpers;

namespace ConsoleAppProject.Menus;

public class MainMenu : BaseMenu
{
    private ConsoleHelpersDemonstrationsMenu _consoleHelpersDemoMenu;

    public MainMenu()
    {
        _consoleHelpersDemoMenu = new ConsoleHelpersDemonstrationsMenu();
    }

    public override async Task<bool> HandleMenuChoiceAsync(int choice)
    {
        IAsyncDemo nextDemo = _consoleHelpersDemoMenu;
        string title = "Console Helpers Demonstrations";
        switch (choice)
        {
            case 1:
                nextDemo = _consoleHelpersDemoMenu;
                title = "Console Helpers Demonstrations";
                break;
            case 2:
            //add additional menus here, and break out:
            //title = "Other Menu";
            default:
                return false;
        }

        await nextDemo.ShowAsync(title);

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        return true;
    }

    //get menu options
    public override List<string> MenuOptions() => new List<string>() {
            "Console Helpers Demo",
            "Exit"
    };
}
