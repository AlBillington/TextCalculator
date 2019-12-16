using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TextCalculator
{
    public class StringCalculator
    {
        private List<string> Delimiters { set; get; } = new List<string> {",", "\n"};

        /// <summary>
        /// A summary to provide to a user details on how to format the input string.
        /// </summary>
        public string PromptString
        {
            get
            {
                var delimiterDisplayValues = new List<string>();
                foreach(var delimiter in Delimiters)
                {
                    delimiterDisplayValues.Add($"'{Regex.Escape(delimiter)}'");
                }
                string delimitersList = String.Join(", ", delimiterDisplayValues);

                return $"Enter a string of numbers seperated by any of the following delimiters: {delimitersList}.";

            }
        }

        public int Calculate(string inputString, bool allowNegativeValues)
        {
            var stringParser = new InputStringParser(inputString, Delimiters, allowNegativeValues);
            return stringParser.GetAllNumbers().Sum();
        }
    }
}
