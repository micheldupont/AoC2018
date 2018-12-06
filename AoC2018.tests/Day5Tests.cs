using Xunit;

namespace AoC2018.tests
{
	public class Day5Tests
	{
	    [Fact]
		public void CanReduceTwoPolymers()
		{
			var input = "jJ";

			var day5 = new Day5(input);
			var result = day5.Reduce();

			Assert.Equal("", result);
		}

		[Fact]
		public void CanReduceTwoPolymersFollowedByMore()
		{
			var input = "jJJJJJ";

			var day5 = new Day5(input);
			var result = day5.Reduce();

			Assert.Equal("JJJJ", result);
		}

		[Fact]
		public void CanReduceFunkyPolymersPart1()
		{
			var input = "AjJa";

			var day5 = new Day5(input);
			var result = day5.Reduce();

			Assert.Equal("", result);
		}

		[Fact]
		public void Q1()
		{
			var day5 = new Day5(Inputs.Day5);
			var result = day5.Reduce();

			Assert.Equal(9822, result.Length);
		}

		[Fact]
		public void CanOptimizeForAPolymer()
		{
			var day5 = new Day5("dabAcCaCBAcCcaDA");
			var result = day5.Optimize("a");

			Assert.Equal("dbCBcD", result);
		}

		[Fact]
		public void CanOptimizeForAllPolymers()
		{
			var day5 = new Day5("dabAcCaCBAcCcaDA");
			var result = day5.OptimizeForAllPolymers();

			Assert.Equal("daDA", result);
		}

		[Fact]
		public void Q2()
		{
			var day5 = new Day5(Inputs.Day5);
			var result = day5.OptimizeForAllPolymers();

			Assert.StartsWith("kYEdWFUoAxTPmFIVPxmFpELyibyPuLvpxwwJcZ", result);
			Assert.Equal(5726, result.Length);
		}
	}
}