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

                return $"Enter a string of numbers seperated by any of the following delimiters: {delimitersList}.  Use the syntax //{{delimiter}}\n{{numbers}} to specify a custom delimiter before the string of numbers.";
            }
        }



        public int Calculate(string inputString, bool allowNegativeValues)
        {
            var delimiters = DefaultDelimiters;
            AddCustomDelimiters(ref inputString, ref delimiters);

            var stringParser = new InputStringParser(inputString, delimiters, allowNegativeValues);
            return stringParser.GetAllNumbers().Sum();
        }

        private void AddCustomDelimiters(ref string inputString, ref List<string> delimiters)
        {
            var customDelimiterRegexGroups = Regex.Match(inputString, "^//(.)\n").Groups;
            var customDelimiter = customDelimiterRegexGroups[1].Value;
            if (!string.IsNullOrEmpty(customDelimiter))
            {
                delimiters.Add(customDelimiter);
                inputString = inputString.Substring(customDelimiterRegexGroups[1].Value.Length);
            }
        }
    }
}
