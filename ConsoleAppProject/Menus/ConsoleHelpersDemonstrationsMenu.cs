
using ConsoleAppProject.CodeAndDemonstrations;

namespace ConsoleAppProject.Menus;

public class ConsoleHelpersDemonstrationsMenu : BaseMenu
{
    private readonly DemonstrateConsoleHelpers _demoConsoleHelpers;

    public ConsoleHelpersDemonstrationsMenu()
    {
        _demoConsoleHelpers = new DemonstrateConsoleHelpers();   
    }

    public override List<string> MenuOptions() => new List<string> {
        "Show Formatted Messages",
        "Show Input Helpers",
        "Exit"
    };

    public override async Task<bool> HandleMenuChoiceAsync(int choice)
    {
        
        switch (choice)
        {
            case 1:
                _demoConsoleHelpers.ShowFormattedMessages();
                break;
            case 2:
                _demoConsoleHelpers.ShowInputHelpers();
                break;
            default:
                return false;
        }

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        return true;
    }
}
