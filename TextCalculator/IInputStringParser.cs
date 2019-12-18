using System.Collections.Generic;

namespace TextCalculator
{
    public interface IInputStringParser
    {
        InputStringParserSettings Settings { get; set; }

        List<int> GetAllNumbers(string rawString);
    }
}