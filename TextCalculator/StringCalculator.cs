using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TextCalculator
{
    public class StringCalculator
    {
        private IInputStringParser StringParser { set; get; }
        /// <summary>
        /// A summary to provide to a user details on how to format the input string.
        /// </summary>
        public string PromptString
        {
            get
            {
                var delimiterDisplayValues = new List<string>();
                foreach(var delimiter in StringParser.Settings.Delimiters)
                {
                    delimiterDisplayValues.Add($"'{Regex.Escape(delimiter)}'");
                }
                string delimitersList = String.Join(", ", delimiterDisplayValues);

                return $"Enter a string of numbers seperated by any of the following delimiters: {delimitersList}. " +
                    $"Use the syntax //[{{delimiter1}}][{{delimiter2}}][...]\\n{{numbers}} to specify custom delimiters before the string of numbers.\n" +
                    $"Negative number support: {StringParser.Settings.AllowNegativeValues}.\n" +
                    $"Maximum Value Supported: {StringParser.Settings.MaximumValue}\n";
            }
        }
        public StringCalculator(IInputStringParser stringParser)
        {
            StringParser = stringParser;
        }
        public StringCalculator()
        {
            StringParser = new InputStringParser();
        }
        public CalculatorResult Calculate(string inputString, IOperation operation)
        {
            string numbersString;
            StringParser.Settings.Delimiters.AddRange(GetCustomDelimiters(inputString, out numbersString));

            var values = StringParser.GetAllNumbers(numbersString);
            return new CalculatorResult(values, operation);
        }
        private List<string> GetCustomDelimiters(string inputString, out string numbersString)
        {
            var delimiters = new List<string>();
            numbersString = inputString;
            var customDelimiterMatch = Regex.Match(inputString, "^//(.+)\n");
            var customDelimiter = customDelimiterMatch.Groups[1].Value;
            if (!string.IsNullOrEmpty(customDelimiter))
            {
                numbersString = inputString.Substring(customDelimiterMatch.Value.Length);
                if (customDelimiter.Length == 1)
                {
                    delimiters.Add(customDelimiter);
                }
                else
                {
                    var multiCharacterDelimiters =
                        (from match in Regex.Matches(customDelimiter, @"\[(.+?)\]")
                        where !string.IsNullOrEmpty(match.Groups[1].Value)
                        select match.Groups[1].Value).ToList();
                    foreach (var delimiter in multiCharacterDelimiters)
                    {
                        delimiters.Add(delimiter);
                    }
                }
            }
            return delimiters;
        }
    }
}
