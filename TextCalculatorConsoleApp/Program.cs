using System;
using TextCalculator;

namespace TextCalculatorConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var calculator = new StringCalculator();
            Console.WriteLine(calculator.PromptString);
            Console.WriteLine("Multiline strings may be entered. Input an empty line to terminate the input.  " +
                "The application will repeat execution until the console is closed or terminated with 'Ctrl+C'");
            while (true)
            {
                try
                {
                    string line;
                    string input = string.Empty;
                    while (!String.IsNullOrWhiteSpace(line = Console.ReadLine()))
                    {
                        input += line + "\n";
                    }
                    var result = calculator.Calculate(input.Trim(), false).NumberSentence;
                    Console.WriteLine($"Result: {result}");

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error Calculating Value.  Error details: {ex.Message}");
                }
                Console.WriteLine("\n");
            }
        }
    }
}
