using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextCalculator
{
    public interface IOperation
    {
        string OperatorString { get; }
        int Calculate(List<int> values);

    }

    public class AdditionOperation : IOperation
    {
        public string OperatorString { get; } = "+";

        public int Calculate(List<int> values)
        {
            return values.Sum();
        }
    }

    public class SubtractionOperation : IOperation
    {
        public string OperatorString { get; } = "-";

        public int Calculate(List<int> values)
        {
            if (values.Count == 0)
            {
                return 0;
            }
            if (values.Count == 1)
            {
                return values[0];
            }
            return values[0] - values.Skip(1).Sum();
        }
    }

    public class MultiplicationOperation : IOperation
    {
        public string OperatorString { get; } = "*";

        public int Calculate(List<int> values)
        {
            if (values.Count == 0)
            {
                return 0;
            }
            int product = 1;
            foreach (var value in values)
            {
                product *= value;
            }
            return product;
        }
    }

    public class DivisionOperation : IOperation
    {
        public string OperatorString { get; } = "/";

        public int Calculate(List<int> values)
        {
            if (values.Count == 0)
            {
                return 0;
            }
            if (values.Count == 1)
            {
                return values[0];
            }
            int product = 1;
            foreach (var value in values.Skip(1))
            {
                product *= value;
            }
            return values[0] / product;
        }
    }
}
