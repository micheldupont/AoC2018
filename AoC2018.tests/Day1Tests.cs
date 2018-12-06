using System;
using System.Linq;
using Xunit;

namespace AoC2018.tests
{
    public class Day1Tests
    {
        [Fact]
        public void FirstCase()
        {
            var input = new[] {1,-2,3,1};

            var day = new Day1();
            var result = day.Run(input);

            Assert.Equal(3, result);
        }

        [Fact]
        public void SecondCase()
        {
            var input = new[] { 1, 1, 1 };

            var day = new Day1();
            var result = day.Run(input);

            Assert.Equal(3, result);
        }

        [Fact]
        public void ThirdCase()
        {
            var input = new[] { 1, 1, -2 };

            var day = new Day1();
            var result = day.Run(input);

            Assert.Equal(0, result);
        }

        [Fact]
        public void ForthCase()
        {
            var input = new[] { -1, -2, -3 };

            var day = new Day1();
            var result = day.Run(input);

            Assert.Equal(-6, result);
        }

        [Fact]
        public void Q1()
        {
            var items = Inputs.Day1.Split(new[] {'\n'}, StringSplitOptions.RemoveEmptyEntries);
            var input = items.Select(i => int.Parse(i.Replace(" ", ""))).ToArray();

            var day = new Day1();
            var result = day.Run(input);

            Assert.Equal(587, result);
        }

        [Fact]
        public void Q2FirstTest()
        {
            var input = new[]{1,-1};

            var day = new Day1();
            var result = day.Run2(input);

            Assert.Equal(0, result);
        }

        [Fact]
        public void Q2SecondTest()
        {
            var input = new[] { 3, 3, 4, -2, -4 };

            var day = new Day1();
            var result = day.Run2(input);

            Assert.Equal(10, result);
        }

        [Fact]
        public void Q2SThirdTest()
        {
            var input = new[] { -6, 3, 8, 5, -6 };

            var day = new Day1();
            var result = day.Run2(input);

            Assert.Equal(5, result);
        }

        [Fact]
        public void Q2SForthTest()
        {
            var input = new[] { 7, 7, -2, -7, -4 };

            var day = new Day1();
            var result = day.Run2(input);

            Assert.Equal(14, result);
        }

          [Fact]
          public void Q2()
          {
              var items = Inputs.Day1.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
              var input = items.Select(i => int.Parse(i.Replace(" ", ""))).ToArray();

              var day = new Day1();
              var result = day.Run2(input);

              Assert.Equal(83130, result);
          }
    }
}