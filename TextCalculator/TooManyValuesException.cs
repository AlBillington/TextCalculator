using System;
using System.Collections.Generic;
using System.Text;

namespace TextCalculator
{
    public class TooManyValuesException : Exception
    {
        public TooManyValuesException()
        {
        }

        public TooManyValuesException(string message)
            : base(message)
        {
        }

        public TooManyValuesException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
