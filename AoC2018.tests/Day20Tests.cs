using System;
using System.IO;
using System.Text;
using Xunit;

namespace AoC2018.tests
{
	public class Day20Tests : IDisposable
	{
		protected internal StreamWriter Sw;

		public Day20Tests()
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
		public void CanParseAndExpandExpressions()
		{
			var input = @"^ENWWW(NEEE|SSE(EE|N))$";
			
			var day = new Day20(input);

			day.BuildMap();

		//	Render(day.Map);
			Assert.Equal('X', day.Map[6,6]);
		}

		[Fact]
		public void CanParseMyInput()
		{
			var day = new Day20(Inputs.Day20);

			day.BuildMap();

		//	Render(day.Map);
			Assert.Equal('#', day.Map[0, 0]);
		}

		[Fact]
		public void CanFindLongestPathBasic()
		{
			var input = @"^WNE$";

			var day = new Day20(input);

			day.BuildMap();
			var result = day.SearchForLongestPath();

		//	Render(day.Map);
			Assert.Equal(3, result);
		}

		[Fact]
		public void CanFindLongestPathA()
		{
			var input = @"^ENWWW(NEEE|SSE(EE|N))$";

			var day = new Day20(input);

			day.BuildMap();
			var result = day.SearchForLongestPath();

		//	Render(day.Map);
			Assert.Equal(10, result);
		}

		[Fact]
		public void CanFindLongestPathB()
		{
			var input = @"^ENNWSWW(NEWS|)SSSEEN(WNSE|)EE(SWEN|)NNN$";

			var day = new Day20(input);

			day.BuildMap();
			var result = day.SearchForLongestPath();

		//	Render(day.Map);
			Assert.Equal(18, result);
		}

		[Fact]
		public void CanFindLongestPathC()
		{
			var input = @"^ESSWWN(E|NNENN(EESS(WNSE|)SSS|WWWSSSSE(SW|NNNE)))$";

			var day = new Day20(input);

			day.BuildMap();
			var result = day.SearchForLongestPath();

		//	Render(day.Map);
			Assert.Equal(23, result);
		}

		[Fact]
		public void CanFindLongestPathD()
		{
			var input = @"^WSSEESWWWNW(S|NENNEEEENN(ESSSSW(NWSW|SSEN)|WSWWN(E|WWS(E|SS))))$";

			var day = new Day20(input);

			day.BuildMap();
			var result = day.SearchForLongestPath();

		//	Render(day.Map);
			Assert.Equal(31, result);
		}

		[Fact]
		public void Q1()
		{
			var day = new Day20(Inputs.Day20);

			day.BuildMap();
			var result = day.SearchForLongestPath();

			Render(day.Map);
			Assert.Equal(4214, result);
		}

		[Fact]
		public void Q2()
		{
			var day = new Day20(Inputs.Day20);

			day.BuildMap();
			var result = day.SearchFarRooms();

			Render(day.Map);
			Assert.Equal(8492, result);
		}
	}
}