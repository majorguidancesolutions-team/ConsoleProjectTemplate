
namespace ConsoleHelpers;

public class InputHelpers
{
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

    /// <summary>
    /// Gets an integer input from the user
    /// </summary>
    /// <param name="prompt">The question to ask the user</param>
    /// <param name="min">min value to allow [defaults to int.MinValue]</param>
    /// <param name="max">max value to allow [defaults to int.MaxValue]</param>
    /// <param name="confirm">Allow hook to ask for confirmation of input</param>
    /// <returns>Valid integer input from the user</returns>
    public static int GetInputAsInt(string prompt, 
                                    int min = int.MinValue, 
                                    int max = int.MaxValue, 
                                    bool confirm = false)
    {
        double result = GetInputAsDouble(prompt, min, max, confirm);
        
        //if (result is not a whole integer number)
        // ask again...
        while(result % 1.0 != 0)
        {
            Console.WriteLine("Please enter a non-decimal value");
            result = GetInputAsDouble(prompt, min, max, confirm);
        }

        return Convert.ToInt32(result); 
    }
    
    /// <summary>
    /// Gets a boolean input from the user
    /// </summary>
    /// <param name="prompt">The question to ask the user</param>
    /// <param name="confirm">Hook to allow user to validate their choice [optional]</param>
    /// <returns>Choice of true/false from prompt</returns>
    public static bool GetInputAsBool(string prompt,
                                        bool confirm = false)
    {
        bool startsWithN = false;
        bool startsWithY = false;
        bool success = false;
        while(!success)
        {
            Console.WriteLine($"{prompt}(Y/N)?");
            string confirmation = Console.ReadLine() ?? string.Empty;
            startsWithY = confirmation.StartsWith("y", StringComparison.OrdinalIgnoreCase);
            startsWithN = confirmation.StartsWith("n", StringComparison.OrdinalIgnoreCase);
            if(startsWithY || startsWithN)
            {
                success = true;
                break;
            }
            Console.WriteLine("invalid input");
            
        }

        //TODO: Need to confirm if asked to confirm
        

        //true if they say "y" || "Y" || "yes" || "Yes" || "YES" || "YeS" || "Yellow" 
        return startsWithY;
          
    }
    
    //TODO: Input as string
}
