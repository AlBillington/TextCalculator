using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextCalculator
{
    public class CalculatorResult
    {
        public List<int> Values { get; }
        public int Result { get; }
        public string NumberSentence
        {
            get
            {
                string Operator = "+";
                return $"{string.Join(Operator, Values)} = {Result}";
            }
        }


        public CalculatorResult(List<int> values, int result)
        {
            Values = values;
            Result = result;
        }
    }
}
