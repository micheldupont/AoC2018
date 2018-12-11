using Xunit;

namespace AoC2018.tests
{
	public class Day11Tests
	{
		[Fact]
		public void CanCalculatePowerLevels()
		{
			var cellA = new Day11(8).BuildCell(3, 5);
			var cellB = new Day11(57).BuildCell(122, 79);
			var cellC = new Day11(39).BuildCell(217, 196);
			var cellD = new Day11(71).BuildCell(101, 153);

			Assert.Equal(4, cellA.PowerLevel);
			Assert.Equal(-5, cellB.PowerLevel);
			Assert.Equal(0, cellC.PowerLevel);
			Assert.Equal(4, cellD.PowerLevel);
		}

		[Fact]
		public void CanFindExampleBestSquare()
		{
			var day11 = new Day11(18);

			var result = day11.FindBestSquare();

			Assert.Equal(29, result.TotalPower);
			Assert.Equal(33, result.Corner.X);
			Assert.Equal(45, result.Corner.Y);
		}

		[Fact]
		public void CanFindExample2BestSquare()
		{
			var day11 = new Day11(42);

			var result = day11.FindBestSquare();

			Assert.Equal(30, result.TotalPower);
			Assert.Equal(21, result.Corner.X);
			Assert.Equal(61, result.Corner.Y);
		}

		[Fact]
		public void Q1()
		{
			var day11 = new Day11(5093);

			var result = day11.FindBestSquare();

			Assert.Equal(31, result.TotalPower);
			Assert.Equal(243, result.Corner.X);
			Assert.Equal(49, result.Corner.Y);
		}

		[Fact]
		public void CanGenerateSquaresOfSizeOne()
		{
			var day11 = new Day11(5093);

			var result = day11.GenerateSquares(1);

			Assert.Equal(90000, result.Count);
		}

		[Fact]
		public void CanGenerateSquaresOfSizeTwo()
		{
			var day11 = new Day11(5093);

			var result = day11.GenerateSquares(2);

			Assert.Equal(89401, result.Count);
		}

		[Fact]
		public void CanGenerateSquaresOfSize300()
		{
			var day11 = new Day11(5093);

			var result = day11.GenerateSquares(300);

			Assert.Equal(1, result.Count);
		}

		[Fact]
		public void CanGenerateSquaresOfSize299()
		{
			var day11 = new Day11(5093);

			var result = day11.GenerateSquares(299);

			Assert.Equal(4, result.Count);
		}

		[Fact]
		public void CanFindExample3BestSquare()
		{
			var day11 = new Day11(18);

			var result = day11.FindBestSquareOfAnySize();

			Assert.Equal(113, result.TotalPower);
			Assert.Equal(90, result.Corner.X);
			Assert.Equal(269, result.Corner.Y);
			Assert.Equal(16, result.Size);
		}

		[Fact]
		public void CanFindExample4BestSquare()
		{
			var day11 = new Day11(42);

			var result = day11.FindBestSquareOfAnySize();

			Assert.Equal(119, result.TotalPower);
			Assert.Equal(232, result.Corner.X);
			Assert.Equal(251, result.Corner.Y);
			Assert.Equal(12, result.Size);
		}

		[Fact]
		public void Q2()
		{
			var day11 = new Day11(5093);

			var result = day11.FindBestSquareOfAnySize();

			Assert.Equal(84, result.TotalPower);
			Assert.Equal(285, result.Corner.X);
			Assert.Equal(169, result.Corner.Y);
			Assert.Equal(15, result.Size);
		}
	}
}