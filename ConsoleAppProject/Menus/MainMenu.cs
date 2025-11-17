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
        // double sample1 = InputHelpers.GetInputAsDouble(prompt);
        // double sample2 = InputHelpers.GetInputAsDouble(prompt, confirm: true);
        // double sample3 = InputHelpers.GetInputAsDouble(prompt, min, confirm:true);
        
        double result = InputHelpers.GetInputAsDouble(prompt, min, max, true);
        Console.WriteLine($"You entered {result}");

        prompt = "Guess a number between 1 and 7";
        int intMin = 1;
        int intMax = 7;
        int intResult = InputHelpers.GetInputAsInt(prompt, intMin, intMax, true);
        Console.WriteLine($"You guessed {intResult}");

        prompt = "Would you like to continue?";
        bool boolResult = InputHelpers.GetInputAsBool(prompt, true);
        Console.WriteLine($"continue: {(boolResult? "yes" : "no")}");

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
