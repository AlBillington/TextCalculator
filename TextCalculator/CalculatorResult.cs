using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextCalculator
{
    public class CalculatorResult
    {
        private IOperation Operation { get; }
        public List<int> Values { get; }
        public int Result { get; }
        public string NumberSentence
        {
            get
            {
                string Operator = Operation.OperatorString;
                return $"{string.Join(Operator, Values)} = {Result}";
            }
        }


        public CalculatorResult(List<int> values, IOperation operation)
        {
            Values = values;
            Operation = operation;
            Result = operation.Calculate(values);
        }
    }
}
