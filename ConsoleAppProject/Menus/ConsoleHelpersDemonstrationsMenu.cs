
using ConsoleAppProject.CodeAndDemonstrations;
using ConsoleHelpers;

namespace ConsoleAppProject.Menus;

public class ConsoleHelpersDemonstrationsMenu : IAsyncDemo
{
    private readonly DemonstrateConsoleHelpers _demoConsoleHelpers;

    public ConsoleHelpersDemonstrationsMenu()
    {
        _demoConsoleHelpers = new DemonstrateConsoleHelpers();   
    }

    public List<string> MenuOptions() => new List<string> {
        "Show Formatted Messages",
        "Show Input Helpers",
        "Exit"
    };

    public async Task ShowAsync()
    {
        bool shouldContinue = true;
        while (shouldContinue)
        {
            Console.Clear();

            var menuText = MenuGenerator.GenerateMenu("Demonstrate Console Helpers", "Please select an operation", MenuOptions(), 40);

            // Show menu and get user choice
            int choice = InputHelpers.GetInputAsInt(menuText, confirm: true, min: 1, max: MenuOptions().Count);

            try
            {
                shouldContinue = await HandleMenuChoiceAsync(choice);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }
    }

    public async Task<bool> HandleMenuChoiceAsync(int choice)
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
