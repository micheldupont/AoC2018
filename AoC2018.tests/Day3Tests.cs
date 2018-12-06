using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AoC2018.tests
{
    public class Day3Tests
    {
        [Fact]
        public void CanBuildDay3FromString()
        {
            var input = "#1 @ 126,902: 29x28";
            var day3 = Day3.Parse(input);

            Assert.Equal(1, day3.Id);
            Assert.Equal(126, day3.X);
            Assert.Equal(902, day3.Y);
            Assert.Equal(29, day3.W);
            Assert.Equal(28, day3.H);
        }

        private List<Day3> _parseInput(string input)
        {
            var lines = input.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            return lines.Select(Day3.Parse).ToList();
        }

        [Fact]
        public void CanDetectCollisionForACoord()
        {
            var input = Day3.Parse("#1 @ 1,3: 4x4");
            var result = input.IsCollidingWith(new Coord(2, 4));

            Assert.True(result);
        }

        [Fact]
        public void CanDetectNonCollisionForACoord()
        {
            var input = Day3.Parse("#1 @ 1,3: 4x4");
            var result = input.IsCollidingWith(new Coord(0, 4));

            Assert.False(result);
        }

        [Fact]
        public void CanGenerateItsCoordList()
        {
            var input = Day3.Parse("#1 @ 1,3: 2x2");
            var result = input.GetAllCoveredCoords();

            Assert.Contains(new Coord(1, 3), result);
            Assert.Contains(new Coord(2, 3), result);
            Assert.Contains(new Coord(1, 4), result);
            Assert.Contains(new Coord(2, 4), result);
        }

        [Fact]
        public void CanReturnCollisionCoordinatesWhenTwoInputCollides()
        {
            var inputA = Day3.Parse("#1 @ 1,3: 4x4");
            var inputB = Day3.Parse("#2 @ 3,1: 4x4");

            var result = inputA.CalculateCollisions(inputB);

            Assert.Contains(new Coord(3, 3), result);
            Assert.Contains(new Coord(3, 4), result);
            Assert.Contains(new Coord(4, 3), result);
            Assert.Contains(new Coord(4, 4), result);
        }

        [Fact]
        public void CanReturnCollisionListForTwoInput()
        {
            var input = new List<Day3>
            {
                Day3.Parse("#1 @ 1,3: 4x4"),
                Day3.Parse("#2 @ 3,1: 4x4")
            };
 
            var map = new Day3CollisionMap(input);

            var result = map.CalculateAllCollisions();

            Assert.Contains(new Coord(3, 3), result);
            Assert.Contains(new Coord(3, 4), result);
            Assert.Contains(new Coord(4, 3), result);
            Assert.Contains(new Coord(4, 4), result);
        }

        [Fact]
        public void CanReturnCollisionListForThreeInput()
        {
            var input = new List<Day3>
            {
                Day3.Parse("#1 @ 1,3: 4x4"),
                Day3.Parse("#2 @ 3,1: 4x4"),
                Day3.Parse("#3 @ 5,5: 2x2")
            };

            var map = new Day3CollisionMap(input);

            var result = map.CalculateAllCollisions();

            Assert.Contains(new Coord(3, 3), result);
            Assert.Contains(new Coord(3, 4), result);
            Assert.Contains(new Coord(4, 3), result);
            Assert.Contains(new Coord(4, 4), result);
        }

        [Fact]
        public void ValidationA()
        {
            var input = new List<Day3>
            {
                Day3.Parse("#1 @ 0,0: 4x4"),
                Day3.Parse("#2 @ 1,1: 2x2")
            };

            var map = new Day3CollisionMap(input);

            var result = map.CalculateAllCollisions();

            Assert.Equal(Day3.Parse("#2 @ 1,1: 2x2").GetAllCoveredCoords(), result);
        }

        [Fact]
        public void ValidationB()
        {
            var input = new List<Day3>
            {
                Day3.Parse("#1 @ 0,0: 4x4"),
                Day3.Parse("#2 @ 0,0: 4x4")
            };

            var map = new Day3CollisionMap(input);

            var result = map.CalculateAllCollisions();

            Assert.Equal(Day3.Parse("#2 @ 0,0: 4x4").GetAllCoveredCoords(), result);
        }

        [Fact]
        public void ValidationC()
        {
            var input = new List<Day3>
            {
                Day3.Parse("#1 @ 1,3: 4x4"),
                Day3.Parse("#2 @ 5,5: 2x2"),
                Day3.Parse("#3 @ 3,1: 4x4")
            };

            var map = new Day3CollisionMap(input);

            var result = map.CalculateAllCollisions();

            Assert.Contains(new Coord(3, 3), result);
            Assert.Contains(new Coord(3, 4), result);
            Assert.Contains(new Coord(4, 3), result);
            Assert.Contains(new Coord(4, 4), result);
        }

        [Fact]
        public void ValidationD()
        {
            var input = new List<Day3>
            {
                Day3.Parse("#1 @ 15,0: 1x400"),
                Day3.Parse("#2 @ 0,15: 400x1"),
            };

            var map = new Day3CollisionMap(input);

            var result = map.CalculateAllCollisions();

            Assert.Contains(new Coord(15, 15), result);
        }

        [Fact]
        public void ValidationE()
        {

            var day = Day3.Parse("#1 @ 15,0: 1x400");
                
            var coords = day.GetAllCoveredCoords();
            
           Assert.Equal(400, coords.Count);
        }

        [Fact]
        public void Q1()
        {
            var input = _parseInput(Inputs.Day3);
            var map = new Day3CollisionMap(input);

            var result = map.CalculateAllCollisions();

            Assert.Equal(118858, result.Count);
        }

        [Fact]
        public void Q2()
        {
            var input = _parseInput(Inputs.Day3);
            var map = new Day3CollisionMap(input);

            var result = map.GetNonCollidingDay3S();
            Assert.Single(result);
            Assert.Equal(1100, result[0].Id);
        }
    }
}