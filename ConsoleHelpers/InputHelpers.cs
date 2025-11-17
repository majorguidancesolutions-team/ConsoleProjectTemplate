
namespace ConsoleHelpers;

public class InputHelpers
{
    //TODO: Get user input from console with range validation to return a double
    //              , bonus: optional ask if they want to confirm the value


    /// <summary>
    /// <param name="prompt">The question to ask the user</param>
    /// <param name="min">Inclusive minimum value [defaults to double.MinValue]</param>
    /// <param name="max">Inclusive maximum value [defaults to double.MaxValue]</param>
    /// <param name="confirm">Optional asks them to confirm their input</param> 
    /// <returns>The valid double in the range</returns>
    /// </summary>
    public static double GetInputAsDouble(string prompt, double min = double.MinValue
                                         , double max = double.MaxValue
                                         , bool confirm = false)
    {
        bool success = false;
        double result = double.MinValue;
        while (!success)
        {
            Console.WriteLine(prompt);
            string number = Console.ReadLine() ?? string.Empty;
            success = double.TryParse(number, out result); 
            if (!success)
            {
                Console.WriteLine("Please enter a valid number");
                continue;
            } 
            if (result > max || result < min)
            {
                success = false;
                Console.WriteLine($"Please enter a value within defined parameters {min}, {max}");
                continue;
            }
            if (confirm)
            {
                Console.WriteLine($"You entered {result}, is this correct? (Y/N)");
                string confirmation = Console.ReadLine() ?? string.Empty;
                //if they say "y" || "Y" || "yes" || "Yes" || "YES" || "YeS" || "Yellow" 
                // we will assume they are saying we got it correct.
                success = confirmation.StartsWith("y", StringComparison.OrdinalIgnoreCase);
            }
        }
        return result;
    }
}
