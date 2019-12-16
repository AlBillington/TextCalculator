using System;
using System.Collections.Generic;
using System.Text;

namespace TextCalculator
{
    /// <summary>
    /// Extracts numbers from a delimited input string
    /// </summary>
    public class InputStringParser
    {
        public string RawString { get; }
        private string Delimiter { get; }
        private int MaxNumberOfValues { set; get; }

        /// <summary>
        /// Extracts numbers from a delimited input string
        /// </summary>
        /// <param name="rawString">The delimited string to parse</param>
        /// <param name="maxNumberOfValues">The maximum number of values allowed in the string.</param>
        /// <param name="delimiter">the delimiter which seperates each numeric entry in the string</param>
        public InputStringParser(string rawString, string delimiter, int maxNumberOfValues)
        {
            RawString = rawString;
            MaxNumberOfValues = maxNumberOfValues;
            Delimiter = delimiter;
        }


        /// <summary>
        /// Create a list of values from the input string
        /// </summary>
        /// <returns>The list of integer values</returns>
        public List<int> GetAllNumbers()
        {
            var numericValues = new List<int>();
            var SplitString = RawString.Split(Delimiter);
            if (SplitString.Length > MaxNumberOfValues)
            {
                throw new TooManyValuesException($"The input string contains more than {MaxNumberOfValues} values, the maximum number of values");
            }
            foreach (var item in SplitString)
            {
                var numericValue = 0;
                if(int.TryParse(item, out numericValue))
                {
                    numericValues.Add(numericValue);
                }
                // for anything unrecognized, add a '0'
                else
                {
                    numericValues.Add(0);
                }
            }
            return numericValues;
        }
       
    }
}
