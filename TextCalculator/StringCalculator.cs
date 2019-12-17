using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TextCalculator
{
    public class StringCalculator
    {
        public List<string> DefaultDelimiters { private set; get; } = new List<string> {",", "\n"};

        /// <summary>
        /// A summary to provide to a user details on how to format the input string.
        /// </summary>
        public string PromptString
        {
            get
            {
                var delimiterDisplayValues = new List<string>();
                foreach(var delimiter in DefaultDelimiters)
                {
                    delimiterDisplayValues.Add($"'{Regex.Escape(delimiter)}'");
                }
                string delimitersList = String.Join(", ", delimiterDisplayValues);

                return $"Enter a string of numbers seperated by any of the following delimiters: {delimitersList}.  Use the syntax //[{{delimiter1}}][{{delimiter2}}][...]\\n{{numbers}} to specify custom delimiters before the string of numbers.";
            }
        }

        public CalculatorResult Calculate(string inputString, bool allowNegativeValues)
        {
            var delimiters = DefaultDelimiters;
            AddCustomDelimiters(ref inputString, ref delimiters);

            var stringParser = new InputStringParser(inputString, delimiters, allowNegativeValues);
            var values = stringParser.GetAllNumbers();
            return new CalculatorResult(values, values.Sum());
        }


        private void AddCustomDelimiters(ref string inputString, ref List<string> delimiters)
        {
            var customDelimiterMatch = Regex.Match(inputString, "^//(.+)\n");
            var customDelimiter = customDelimiterMatch.Groups[1].Value;
            if (!string.IsNullOrEmpty(customDelimiter))
            {
                inputString = inputString.Substring(customDelimiterMatch.Value.Length);
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
        }
    }
}
