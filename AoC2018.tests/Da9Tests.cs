using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AoC2018.tests
{
    public class Da9Tests
    {
        [Fact]
        public void CanSimulateGameOfZero()
        {
            var day = new Day9(1,0);

            Assert.Equal(new []{0}, day.Marbles.Select(x=>x).ToArray());
        }

        [Fact]
        public void CanSimulateGameOfOne()
        {
            var day = new Day9(1, 1);

            Assert.Equal(new [] { 0, 1 }, day.Marbles.Select(x => x).ToArray());
        }

        [Fact]
        public void CanSimulateGameOfTwo()
        {
            var day = new Day9(1, 2);

            Assert.Equal(new [] { 0, 2, 1 }, day.Marbles.Select(x => x).ToArray());
        }

        [Fact]
        public void CanSimulateGameOfFive()
        {
            var day = new Day9(1, 5);

            Assert.Equal(new [] { 0, 4, 2, 5, 1, 3 }, day.Marbles.Select(x => x).ToArray());
        }

        [Fact]
        public void CanSimulateGameOf22()
        {
            var day = new Day9(1, 22);

            Assert.Equal(new [] { 0, 16, 8, 17, 4, 18, 9, 19, 2, 20, 10, 21, 5, 22, 11, 1, 12, 6, 13, 3, 14, 7 ,15 }, day.Marbles.Select(x => x).ToArray());
        }

        [Fact]
        public void CanSimulateGameOf23()
        {
            var day = new Day9(1, 23);

            Assert.Equal(new [] { 0, 16, 8, 17, 4, 18, 19, 2, 20, 10, 21, 5, 22, 11, 1, 12, 6, 13, 3, 14, 7, 15 }, day.Marbles.Select(x => x).ToArray());
        }

        [Fact]
        public void CanSimulateGameOf25()
        {
            var day = new Day9(1, 25);

            Assert.Equal(new [] { 0, 16, 8, 17, 4, 18, 19, 2, 24, 20, 25, 10, 21, 5, 22, 11, 1, 12, 6, 13, 3, 14, 7, 15 }, day.Marbles.Select(x => x).ToArray());
        }

        [Fact]
        public void CanGetScoreFOrGameOf25()
        {
            var day = new Day9(9, 25);

            var result = day.GetMaxScore();

            Assert.Equal(32, result);
        }

        [Fact]
        public void Ex2()
        {
            var day = new Day9(10, 1618);

            var result = day.GetMaxScore();

            Assert.Equal(8317, result);
        }


        [Fact]
        public void Ex3()
        {
            var day = new Day9(13, 7999);

            var result = day.GetMaxScore();

            Assert.Equal(146373, result);
        }


        [Fact]
        public void Ex4()
        {
            var day = new Day9(17, 1104);

            var result = day.GetMaxScore();

            Assert.Equal(2764, result);
        }


        [Fact]
        public void Ex5()
        {
            var day = new Day9(21, 6111);

            var result = day.GetMaxScore();

            Assert.Equal(54718, result);
        }

        [Fact]
        public void Ex6()
        {
            var day = new Day9(10, 1618);

            var result = day.GetMaxScore();

            Assert.Equal(8317, result);
        }

        [Fact]
        public void Q1()
        {
            var day = new Day9(479, 71035);

            var result = day.GetMaxScore();

            Assert.Equal(367634, result);
        }

        [Fact]
        public void Q2()
        {
            var day = new Day9(479, 7103500);
   
            var result = day.GetMaxScore();

            Assert.Equal(3020072891, result);
        }
    }
}