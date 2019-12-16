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
                var input = Console.ReadLine();
                var result = calculator.Calculate(input);
                Console.WriteLine($"Result: {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Calculating Value.  Error details: {ex.Message}");
            }
        }
    }
}
