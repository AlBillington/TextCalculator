﻿using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using TextCalculator;

namespace TextCalculatorConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var parser = new InputStringParser();

            var argValues = CommandLine.Parser.Default.ParseArguments<CommandLineArgumentOptions>(args);
            if (!argValues.Errors.Any())
            {
                // Values are available here
                if(!string.IsNullOrEmpty(argValues.Value.AlternateDelimiter))
                {
                    parser.Settings.Delimiters.Add(argValues.Value.AlternateDelimiter);
                }
                parser.Settings.AllowNegativeValues = argValues.Value.AllowNegativeValues;
                parser.Settings.MaximumValue = argValues.Value.MaximumValue;
            }
            else
            {
                return;
            }

            var calculator = new StringCalculator(parser);
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
                    var result = calculator.Calculate(input.Trim()).NumberSentence;
                    Console.WriteLine($"Result: {result}");

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error Calculating Value.  Error details: {ex.Message}");
                }
                Console.WriteLine("\n");
            }
        }
        class CommandLineArgumentOptions
        {
            [Option("delimiter", Required = false,
            HelpText = "An alternative delimiter to support in addition to ','.")]
            public string AlternateDelimiter { get; set; } = "\n";

            [Option("allowNegativeValues", Required = false,
             HelpText = "Whether to allow negative numbers in the operation.")]
            public bool AllowNegativeValues { get; set; } = false;
            [Option("upperBound", Required = false,
             HelpText = "The maximum value for each number in the string.  Larger numbers will be ignored.")]
            public int MaximumValue { get; set; } = 1000;
        }
    }
}
