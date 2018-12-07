using System;
using System.Collections.Generic;
using Xunit;

namespace AoC2018.tests
{
	public class Day6Tests
	{
		private readonly List<Day6.Coordinate> _testInput = new List<Day6.Coordinate> {
			new Day6.Coordinate(1, 1),
			new Day6.Coordinate(1, 6),
			new Day6.Coordinate(8, 3),
			new Day6.Coordinate(3, 4),
			new Day6.Coordinate(5, 5),
			new Day6.Coordinate(8, 9)
		};

		[Fact]
		public void CanCalculateManhattanDistances()
		{
			var inputA = new Day6.Coordinate(1, 1);
			var inputB = new Day6.Coordinate(2, 2);

			var result = Day6.GetDistance(inputA, inputB);

			Assert.Equal(2, result);
		}

		[Fact]
		public void CanCalculateManhattanDistancesRegardlessOfDirection()
		{
			var inputA = new Day6.Coordinate(10, 2);
			var inputB = new Day6.Coordinate(2, 2);

			var inputC = new Day6.Coordinate(2, 2);
			var inputD = new Day6.Coordinate(2, 10);

			var resultA = Day6.GetDistance(inputA, inputB);
			var resultB = Day6.GetDistance(inputB, inputA);

			var resultC = Day6.GetDistance(inputC, inputD);
			var resultD = Day6.GetDistance(inputD, inputC);

			Assert.Equal(8, resultA);
			Assert.Equal(8, resultB);
			Assert.Equal(8, resultC);
			Assert.Equal(8, resultD);
		}

		[Fact]
		public void CanFindTheSmallestBox()
		{
			var input = _testInput;

			var result = Day6.GetMinBoxCoordinates(input);

			var expected = new Day6.Box
			{
				TopLeft = new Day6.Coordinate(1,1),
				BottomRight = new Day6.Coordinate(8,9)
			};
			Assert.Equal(expected, result);
		}

		[Fact]
		public void CanEvaluateIfCoordinateHasAFiniteBoundingBox()
		{
			var input = _testInput;

			var results = Day6.EvaluateCoordinates(input);

			Assert.False(results[0].IsFinite);
			Assert.False(results[1].IsFinite);
			Assert.False(results[2].IsFinite);
			Assert.True(results[3].IsFinite);
			Assert.True(results[4].IsFinite);
			Assert.False(results[5].IsFinite);
		}

		[Fact]
		public void CanEvaluateTheDimensionOfABoundedArea()
		{
			var input = _testInput;
			var results = Day6.EvaluateCoordinates(input);

		
			var resultA = Day6.GetDimension(results[3]);
			var resultB = Day6.GetDimension(results[4]);

			Assert.Equal(9, resultA);
			Assert.Equal(17, resultB);
		}

	    [Fact]
	    public void CanFindTheBiggestBoundedArea()
	    {
	        var input = _testInput;
	        var result = Day6.GetMaxDimension(input);

	        Assert.Equal(17, result);
	    }

	    private List<Day6.Coordinate> _parse(string input)
	    {
	        var lines = input.Split(new[] {'\r', '\n'}, StringSplitOptions.RemoveEmptyEntries);
	        var list = new List<Day6.Coordinate>();

	        foreach (var line in lines)
	        {
	            var parts = line.Split(new[] {',', ' '}, StringSplitOptions.RemoveEmptyEntries);

                list.Add(new Day6.Coordinate(int.Parse(parts[0]), int.Parse(parts[1])));
	        }

	        return list;
	    }

	    [Fact]
        public void Q1()
		{
			//arrange
			var input = _parse(Inputs.Day6);

			//act
			var result = Day6.GetMaxDimension(input);

			//assert
			Assert.Equal(4754, result);

		}

	    [Fact]
	    public void CanFindTheBiggestSafeArea()
	    {
	        var input = _testInput;
	        var result = Day6.GetSafeDimension(input, 32);

	        Assert.Equal(16, result);
	    }

	    [Fact]
	    public void Q2()
	    {
	        //arrange
	        var input = _parse(Inputs.Day6);

	        var result = Day6.GetSafeDimension(input, 10000);

	        Assert.Equal(42344, result);
        }
	}
}