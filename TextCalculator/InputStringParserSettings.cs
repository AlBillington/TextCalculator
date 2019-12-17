using System;
using System.Collections.Generic;
using System.Text;

namespace TextCalculator
{
    public class InputStringParserSettings
    {
        public List<string> Delimiters { set; get; } = new List<string> { "," };
        public bool AllowNegativeValues { set; get; } = false;
        public int MaximumValue { set; get; } = 1000;
    }
}
