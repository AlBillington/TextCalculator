using System;
using System.Collections.Generic;
using System.Text;

namespace TextCalculator
{
    public class NegativeValuesNotSupportedException : Exception
    {
        public NegativeValuesNotSupportedException()
        {
        }

        public NegativeValuesNotSupportedException(string message)
            : base(message)
        {
        }

        public NegativeValuesNotSupportedException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
