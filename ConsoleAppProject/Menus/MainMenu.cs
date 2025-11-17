using ConsoleHelpers;

namespace ConsoleAppProject.Menus;

public class MainMenu
{
    //private variables for various menus

    //get menu options


    //TODO: Inject Menu Dependencies
    public MainMenu()
    {
        string prompt = "Please enter your favorite constant";
        double min = 0;
        double max = 100;
        double result = InputHelpers.GetInputAsDouble(prompt, min, max, true);
        Console.WriteLine($"You entered {result}");
    }

    public async Task ShowAsync()
    {
        Console.WriteLine("Menu is not yet implemented...");
        await Task.Delay(1);  //delete me

        //should continue?

        //print menu and get user choice


    }

    //handle user choice
}
