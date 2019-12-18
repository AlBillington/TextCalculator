using System;
using Xunit;
using TextCalculator;
using System.Collections.Generic;

namespace Tests_TextCalculator
{
    public class StringCalculatorUnitTests
    {
        [Theory]
        //general tests
        [InlineData("20", 20)]
        [InlineData("1,5000", 1)]
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
        [InlineData("1,2,3", 6)]
        [InlineData("0,0,0", 0)]
        [InlineData("1,2,", 3)]
        [InlineData("0,,1", 1)]
        [InlineData(",,", 0)]
        [InlineData("\n,", 0)]
        [InlineData("1,2,3,4,5\n6,7,8,9,10\n11,12", 78)]
        [InlineData("1\n2\n3", 6)]
        [InlineData("4,\n-3", 1)]
        [InlineData("4000,\n1", 1)]
        [InlineData("999,1000\n1001", 1999)]
        //custom delimiter tests
        [InlineData("//#\n2#5", 7)]
        [InlineData("//#\n2#5,7", 14)]
        [InlineData("//##\n2##5,7", 7)]
        [InlineData("//%\n2#5%7\n9", 16)]
        [InlineData("_//#\n2#5", 0)]
        //multicharacter custom delimiter tests
        [InlineData("//[###]\n2###5", 7)]
        [InlineData("//[123]\n21235", 7)]
        [InlineData("//[,,]\n2,,5\n6,7", 20)]
        [InlineData("//[*][!!][r9r]\n11r9r22*hh*33!!44", 110)]
        [InlineData("//[123][456]\n212354564", 11)]
        public void Calculator_GetsSumForString(string inputString, int expectedSum)
        {
            var parser = new InputStringParser();
            parser.Settings.AllowNegativeValues = true;
            parser.Settings.Delimiters.Add("\n");

            var sut = new StringCalculator(parser);
            var sum = sut.Calculate(inputString, new AdditionOperation()).Result;

            Assert.Equal(expectedSum, sum);
        }

        [Theory]
        //general tests
        [InlineData("20", 20)]
        [InlineData("5,2", 3)]
        [InlineData("4,-3,6", 1)]
        public void Calculator_GetsDifferenceForString(string inputString, int expectedDifference)
        {
            var parser = new InputStringParser();
            parser.Settings.AllowNegativeValues = true;
            parser.Settings.Delimiters.Add("\n");

            var sut = new StringCalculator(parser);
            var difference = sut.Calculate(inputString, new SubtractionOperation()).Result;

            Assert.Equal(expectedDifference, difference);
        }

        [Theory]
        //general tests
        [InlineData(",", 0)]
        [InlineData("20", 20)]
        [InlineData("5,2", 10)]
        [InlineData("4,-3,6", 4*-3*6)]
        public void Calculator_GetsProductForString(string inputString, int expectedProduct)
        {
            var parser = new InputStringParser();
            parser.Settings.AllowNegativeValues = true;
            parser.Settings.Delimiters.Add("\n");

            var sut = new StringCalculator(parser);
            var product = sut.Calculate(inputString, new MultiplicationOperation()).Result;

            Assert.Equal(expectedProduct, product);
        }

        [Theory]
        //general tests
        [InlineData("20", 20)]
        [InlineData("6,2", 6/2)]
        [InlineData("24,-3,6", 24/-3/6)]
        public void Calculator_GetsQuotientForString(string inputString, int expectedQuotient)
        {
            var parser = new InputStringParser();
            parser.Settings.AllowNegativeValues = true;
            parser.Settings.Delimiters.Add("\n");

            var sut = new StringCalculator(parser);
            var quotient = sut.Calculate(inputString, new DivisionOperation()).Result;

            Assert.Equal(expectedQuotient, quotient);
        }

        [Theory]
        [InlineData("20", "20 = 20")]
        [InlineData("1,5000", "1+0 = 1")]
        [InlineData("//[*][!!][r9r]\n11r9r22*hh*33!!44", "11+22+0+33+44 = 110")]
        [InlineData("2, 4, rrrr, 1001, 6", "2+4+0+0+6 = 12")]

        public void Calculator_GetsSumNumberSentenceForString(string inputString, string expectedNumberSentence)
        {
            var parser = new InputStringParser();
            parser.Settings.AllowNegativeValues = true;

            var sut = new StringCalculator(parser);          
            var numberSentence = sut.Calculate(inputString, new AdditionOperation()).NumberSentence;

            Assert.Equal(expectedNumberSentence, numberSentence);
        }
        [Theory]
        [InlineData("4,\n-3,\n 0", new int[] { -3 })]
        [InlineData("1,-2,3,4,-5\n6,-7,8,-9,10\n11,12, yh", new int[] { -2, -5, -9 })]
        public void Calculator_ShouldThrowExceptionForNegativeValues(string inputString, int[] valuesInExceptionMessage)
        {
            var parser = new InputStringParser();
            parser.Settings.AllowNegativeValues = false;
            parser.Settings.Delimiters.Add("\n");

            var sut = new StringCalculator(parser);

            var ex = Assert.Throws<NegativeValuesNotSupportedException>(() => sut.Calculate(inputString, new AdditionOperation()));
            foreach (var number in valuesInExceptionMessage)
            {
                Assert.Contains(number.ToString(), ex.Message);
            }
        }
    }
}
