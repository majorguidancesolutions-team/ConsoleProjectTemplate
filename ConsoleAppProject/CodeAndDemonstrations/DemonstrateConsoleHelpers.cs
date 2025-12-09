
using ConsoleHelpers;

namespace ConsoleAppProject.CodeAndDemonstrations;

public class DemonstrateConsoleHelpers 
{
    public void ShowFormattedMessages()
    {
        var prompt = "What is your name?";
        var response = InputHelpers.GetInputAsString(prompt);
        Console.WriteLine(OutputHelpers.BoxedMessage(response, '*'));
        Console.WriteLine(OutputHelpers.BoxedMessage(response, '-'));
        string tooLong = "Learning C# is like discovering a secret superpower. Each class, method, and interface unlocks new worlds. Stick with it, have fun, and watch your coding skills level up!";
        Console.WriteLine(OutputHelpers.BoxedMessage(tooLong, '-'));
        Console.WriteLine(new string('~', Application.LINE_LENGTH));
        Console.WriteLine(OutputHelpers.BoxedMessageWithTitle("Welcome", response));
    }

    public void ShowInputHelpers()
    {
        string prompt = "Please enter your favorite constant";
        double min = 0;
        double max = 100;

        double result = InputHelpers.GetInputAsDouble(prompt, min, max, true);
        Console.WriteLine($"You entered {result}");

        prompt = "Guess a number between 1 and 7";
        int intMin = 1;
        int intMax = 7;
        int intResult = InputHelpers.GetInputAsInt(prompt, intMin, intMax, true);
        Console.WriteLine($"You guessed {intResult}");

        prompt = "Would you like to continue?";
        bool boolResult = InputHelpers.GetInputAsBool(prompt, true);
        Console.WriteLine($"continue: {(boolResult ? "yes" : "no")}");

        var prompt2 = "IS C# the best language?";
        bool boolResult2 = InputHelpers.GetInputAsBool(prompt2);
        Console.WriteLine($"give up: {(boolResult2 ? "yes" : "no")}");

        // String method
        var prompt3 = "What is your name?";
        string stringResult = InputHelpers.GetInputAsString(prompt3);
        Console.WriteLine($"Hello {stringResult}!");

        var prompt4 = "Enter your name again";
        string stringResult2 = InputHelpers.GetInputAsString(prompt4, true);
        Console.WriteLine($"Hello again {stringResult2}!");
    }
}
