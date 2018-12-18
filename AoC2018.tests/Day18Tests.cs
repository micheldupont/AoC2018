using System;
using System.IO;
using System.Text;
using Xunit;

namespace AoC2018.tests
{
	public class Day18Tests : IDisposable
	{
		private string _testInput = @".#.#...|#.
.....#|##|
.|..|...#.
..|#.....#
#.#|||#|#|
...#.||...
.|....|...
||...#|.#|
|.||||..|.
...#.|..|.";

		protected internal StreamWriter Sw;

		public Day18Tests()
		{
			Sw = new StreamWriter("./out.txt");

		}

		public void Dispose()
		{
			Sw.Close();
			Sw.Dispose();
		}

		private void Render(char[,] map)
		{
			var xLenght = map.GetLength(0);
			var yLenght = map.GetLength(1);

			for (int y = 0; y < yLenght; y++)
			{
				var builder = new StringBuilder();
				for (int x = 0; x < xLenght; x++)
				{
					builder.Append(map[x, y]);
				}

				Sw.WriteLine(builder.ToString());
			}
		}

		[Fact]
		public void CanParse()
		{
			var day = new Day18(_testInput);

			Assert.Equal(10, day.Map.GetLength(0));
			Assert.Equal(10, day.Map.GetLength(1));
			Assert.Equal('#', day.Map[5,1]);

			//Render(day.Map);
		}

		[Fact]
		public void CanDoOneGen()
		{
			var day = new Day18(_testInput);
			day.Mutate();

			Assert.Equal('.', day.Map[5, 1]);
			Assert.Equal('#', day.Map[8, 0]);

			//Render(day.Map);
		}

		[Fact]
		public void Q1Test()
		{
			var day = new Day18(_testInput);
			var result = day.GetMutationTenScore();

			Assert.Equal(1147, result);
		}

		[Fact]
		public void Q1()
		{
			var day = new Day18(Inputs.Day18);
			var result = day.GetMutationTenScore();

			Assert.Equal(574590, result);
			//Render(day.Map);
		}

		[Fact]
		public void Q2()
		{
			var day = new Day18(Inputs.Day18);
			var result = day.GetMutationStableScore();

			Assert.Equal(183787, result);
			//Render(day.Map);
		}
	}
}