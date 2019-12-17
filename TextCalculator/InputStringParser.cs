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
        public InputStringParserSettings Settings { set; get; } = new InputStringParserSettings();


        /// <summary>
        /// Create a list of values from the input string
        /// </summary>
        /// <returns>The list of integer values</returns>
        public List<int> GetAllNumbers(string rawString)
        {
            var numericValues = new List<int>();
            var splitString = rawString.Split(Settings.Delimiters.ToArray(), StringSplitOptions.None);
            foreach (var item in splitString)
            {
                var numericValue = 0;
                if(int.TryParse(item, out numericValue))
                {
                    if (numericValue <= Settings.MaximumValue)
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
            if(!Settings.AllowNegativeValues)
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
