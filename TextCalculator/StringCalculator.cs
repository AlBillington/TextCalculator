using System;
using System.Linq;

namespace TextCalculator
{
    public class StringCalculator
    {
        private string Delimiter { set; get; } = ",";
        private int MaximumNumberOfValues { set; get; } = 2;

        /// <summary>
        /// A summary to provide to a user details on how to format the input string.
        /// </summary>
        public string PromptString
        {
            get
            {
               return $"Enter a string of numbers delimited by '{Delimiter}'. The string must have at most {MaximumNumberOfValues} values.";
            }
        }

        public int Calculate(string inputString)
        {
            var stringParser = new InputStringParser(inputString, Delimiter, MaximumNumberOfValues);
            return stringParser.GetAllNumbers().Sum();
        }
    }
}
