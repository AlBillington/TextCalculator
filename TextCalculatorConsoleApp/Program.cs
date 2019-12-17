using System;
using TextCalculator;

namespace TextCalculatorConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var calculator = new StringCalculator();
                Console.WriteLine(calculator.PromptString);
                string line;
                string input = string.Empty;
                Console.WriteLine("Multiline strings may be entered. Input an empty line to terminate the input.");
                while (!String.IsNullOrWhiteSpace(line = Console.ReadLine()))
                {
                    input += line + "\n";
                }
                var result = calculator.Calculate(input, false).NumberSentence;
                Console.WriteLine($"Result: {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Calculating Value.  Error details: {ex.Message}");
            }
        }
    }
}
