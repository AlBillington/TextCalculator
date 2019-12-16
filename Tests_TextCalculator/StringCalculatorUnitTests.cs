using System;
using Xunit;
using TextCalculator;

namespace Tests_TextCalculator
{
    public class StringCalculatorUnitTests
    {
        [Theory]
        [InlineData("20", 20)]
        [InlineData("1,5000", 5001)]
        [InlineData("4,-3", 1)]
        [InlineData("5,tytyt", 5)]
        [InlineData("20,40", 60)]
        [InlineData("20,0", 20)]
        [InlineData("0,40", 40)]
        [InlineData("20,a", 20)]
        [InlineData("20,34i", 20)]
        [InlineData("0,0", 0)]
        [InlineData(" ", 0)]
        [InlineData(",", 0)]


        public void Calculator_GetsSumForString(string inputString, int expectedSum)
        {
            var sut = new StringCalculator();
            var sum = sut.Calculate(inputString);
            Assert.Equal(sum, expectedSum);
        }

        [Theory]
        [InlineData("1,2,3")]
        [InlineData("0,0,0")]
        [InlineData("1,2,")]
        [InlineData("0,,1")]
        [InlineData(",,")]

        public void Calculator_ThrowsExceptionWhenTooManyValues(string inputString)
        {
            var sut = new StringCalculator();
            Assert.Throws<TooManyValuesException>(() => sut.Calculate(inputString));
        }
    }
}
