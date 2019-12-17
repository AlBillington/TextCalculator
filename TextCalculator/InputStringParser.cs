using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextCalculator
{
    /// <summary>
    /// Extracts numbers from a delimited input string
    /// </summary>
    public class InputStringParser
    {
        public string RawString { get; }
        private List<string> Delimiters { get; }
        private bool AllowNegativeValues { set;  get; }
        private int maximumAllowedValue = 1000;

        /// <summary>
        /// Extracts numbers from a delimited input string
        /// </summary>
        /// <param name="rawString">The delimited string to parse</param>
        /// <param name="maxNumberOfValues">The maximum number of values allowed in the string.</param>
        /// <param name="delimiter">the delimiter which seperates each numeric entry in the string</param>
        public InputStringParser(string rawString, string delimiter, bool allowNegativeValues)
            : this(rawString, new List<string> { delimiter }, allowNegativeValues)
        { 
        }
        public InputStringParser(string rawString, List<string> delimiters, bool allowNegativeValues)
        {
            RawString = rawString;
            Delimiters = delimiters;
            AllowNegativeValues = allowNegativeValues;
        }

        /// <summary>
        /// Create a list of values from the input string
        /// </summary>
        /// <returns>The list of integer values</returns>
        public List<int> GetAllNumbers()
        {
            var numericValues = new List<int>();
            var splitString = RawString.Split(Delimiters.ToArray(), StringSplitOptions.None);
            foreach (var item in splitString)
            {
                var numericValue = 0;
                if(int.TryParse(item, out numericValue))
                {
                    if (numericValue <= maximumAllowedValue)
                    {
                        numericValues.Add(numericValue);
                    }
                    else
                    {
                        numericValues.Add(0);
                    }

                }
                // for anything unrecognized, add a '0'
                else
                {
                    numericValues.Add(0);
                }
            }
            if(!AllowNegativeValues)
            {
                var negativeValues = (from numeric in numericValues
                where numeric < 0
                select numeric).ToList();
                if(negativeValues.Count > 0)
                {
                    throw new NegativeValuesNotSupportedException(
                        $"Negative values are not supported, but the following negative values are present in the input: {String.Join(",", negativeValues)}"
                        );
                }
            }
            return numericValues;
        }   
    }
}
