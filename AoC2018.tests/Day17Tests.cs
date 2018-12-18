using System;
using System.IO;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace AoC2018.tests
{
    public class Day17Tests : IDisposable
    {
	    private readonly string _testInput = @"x=495, y=2..7
y=7, x=495..501
x=501, y=3..7
x=498, y=2..4
x=506, y=1..2
x=498, y=10..13
x=504, y=10..13
y=13, x=498..504";

	    protected internal StreamWriter Sw;

	    public Day17Tests(ITestOutputHelper output)
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
            var day = new Day17(_testInput);

            Assert.Equal(495, day.Minx);
            Assert.Equal(12 + 9, day.Map.GetLength(0));
            Assert.Equal(13, day.Map.GetLength(1));

           // Render(day.Map);
        }

	    [Fact]
        public void CanDropWater()
        {
            var day = new Day17(_testInput);

            var drop = day.DropWater();

            Assert.Equal(1, drop.X);
            Assert.Equal(5, drop.Y);

         //   Render(day.Map);
        }

	    [Fact]
        public void CanDropManyWater()
        {
            var day = new Day17(_testInput);

            day.DropManyWater(57);

            Assert.Equal('w', day.Map[1, 5]);
            Assert.Equal('w', day.Map[1, 4]);
            Assert.Equal('w', day.Map[5, 3]);

            //Render(day.Map);
        }

	    [Fact]
	    public void CanFlood()
	    {
		    var day = new Day17(_testInput);

		    var result = day.Flood();
		    //day.DropManyWater(57);
			Assert.Equal(57, result);
		    //Render(day.Map);
		}

	    [Fact]
	    public void Q1()
	    {
		    var day = new Day17(Inputs.Day17);

		    var result = day.Flood();

			Assert.Equal(37073, result);
		   // Render(day.Map);
	    }

	    [Fact]
	    public void Q2()
	    {
		    var day = new Day17(Inputs.Day17);

		    var result = day.FloodAndDry();

		    Assert.Equal(29289, result);
		    //Render(day.Map);
	    }
	}
}