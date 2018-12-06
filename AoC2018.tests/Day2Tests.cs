using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AoC2018.tests
{
    public class Day2Tests
    {
        private readonly string _myInputText = Inputs.Day2;
        readonly List<string> _testInputs = new List<string> { "abcdef", "bababc", "abbcde", "abcccd", "aabcdd", "abcdee", "ababab" };

        private List<string> _parseInput()
        {
            return _myInputText.Split(new[] {'\r', '\n'}, StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        [Fact]
        public void Day2CanDetectWordWithTwoOfTheSameChar()
        {
            var day2 = new Day2(null);

            var result0 = day2.TestForXOfSameChar(_testInputs[0], 2);
            var result1 = day2.TestForXOfSameChar(_testInputs[1], 2);
            var result2 = day2.TestForXOfSameChar(_testInputs[2], 2);
            var result3 = day2.TestForXOfSameChar(_testInputs[3], 2);
            var result4 = day2.TestForXOfSameChar(_testInputs[4], 2);
            var result5 = day2.TestForXOfSameChar(_testInputs[5], 2);
            var result6 = day2.TestForXOfSameChar(_testInputs[6], 2);

            Assert.False(result0);
            Assert.True(result1);
            Assert.True(result2);
            Assert.False(result3);
            Assert.True(result4);
            Assert.True(result5);
            Assert.False(result6);
        }

        [Fact]
        public void Day2CanDetectWordWithThreeOfTheSameChar()
        {
            var day2 = new Day2(null);

            var result0 = day2.TestForXOfSameChar(_testInputs[0], 3);
            var result1 = day2.TestForXOfSameChar(_testInputs[1], 3);
            var result2 = day2.TestForXOfSameChar(_testInputs[2], 3);
            var result3 = day2.TestForXOfSameChar(_testInputs[3], 3);
            var result4 = day2.TestForXOfSameChar(_testInputs[4], 3);
            var result5 = day2.TestForXOfSameChar(_testInputs[5], 3);
            var result6 = day2.TestForXOfSameChar(_testInputs[6], 3);

            Assert.False(result0);
            Assert.True(result1);
            Assert.False(result2);
            Assert.True(result3);
            Assert.False(result4);
            Assert.False(result5);
            Assert.True(result6);
        }

        [Fact]
        public void Day2CanDoTheCheckSum()
        {
            var day2 = new Day2(_testInputs);
            var result = day2.RunQ1();

            Assert.Equal(12, result);
        }


        [Fact]
        public void Q1()
        {
            var day2 = new Day2(_parseInput());
            var result = day2.RunQ1();

            Assert.Equal(3952, result);
        }

        [Fact]
        public void Day2CanCountHowManyMisMatchBetweenTwoStrings()
        {
            var day2 = new Day2(null);

            var resultA = day2.CountCharMissMatch("abcde", "axcye");
            var resultB = day2.CountCharMissMatch("abcde", "fghij");
            var resultC = day2.CountCharMissMatch("fghij", "fguij");
            var resultD = day2.CountCharMissMatch("pqrst", "pqrst");

            Assert.Equal(2, resultA);
            Assert.Equal(5, resultB);
            Assert.Equal(1, resultC);
            Assert.Equal(0, resultD);
        }

        [Fact]
        public void CanReturnTheMatchString()
        {
            var day2 = new Day2(null);

            var result = day2.GetMatchString("fghij", "fguij");

            Assert.Equal("fgij", result);
        }

        [Fact]
        public void Day2CanDetectCloseId()
        {
            var input = new List<string>()
            {
                "abcde",
                "fghij",
                "klmno",
                "pqrst",
                "fguij",
                "axcye",
                "wvxyz"
            };

            var day2 = new Day2(input);
            var result = day2.RunQ2();

            Assert.Equal("fgij", result);
        }

        [Fact]
        public void Q2()
        {
            var day2 = new Day2(_parseInput());
            var result = day2.RunQ2();

            Assert.Equal("vtnikorkulbfejvyznqgdxpaw", result);
        }
    }
}