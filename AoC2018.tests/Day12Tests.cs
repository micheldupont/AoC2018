using Xunit;

namespace AoC2018.tests
{
	public class Day12Tests
	{
		private readonly string _testInput = @"initial state: #..#.#..##......###...###

...## => #
..#.. => #
.#... => #
.#.#. => #
.#.## => #
.##.. => #
.#### => #
#.#.# => #
#.### => #
##.#. => #
##.## => #
###.. => #
###.# => #
####. => #";

		[Fact]
		public void CanParseSpreadRule()
		{
			var input = @"...## => #";

			var result = Day12.ParseRule(input);

			Assert.Equal(new[]{false,false,false,true,true}, result.Pattern);
			Assert.True(result.YieldsPlant);
		}

		[Fact]
		public void CanParseSpreadRules()
		{
			var result = Day12.ParseInput(_testInput);

			Assert.Equal(14, result.Rules.Count);
		}

		[Fact]
		public void CanParseInitialState()
		{
			var result = Day12.ParseInput(_testInput);

			Assert.Equal(new []{ '#','.','.','#','.','#','.','.','#','#','.','.','.','.','.','.','#','#','#','.','.','.','#','#','#' }, result.InitialState);
		}

		[Fact]
		public void RuleCanMatchAPattern()
		{
			var input = @"...## => #";
			var rule = Day12.ParseRule(input);

			var result = rule.IsMatch(new[] {'.', '.', '.', '#', '#'});

			Assert.True(result);
		}

		[Fact]
		public void RuleCanNotMatchAPattern()
		{
			var input = @"...## => #";
			var rule = Day12.ParseRule(input);

			var result = rule.IsMatch(new[] { '.', '#', '.', '#', '#' });

			Assert.False(result);
		}

		[Fact]
		public void CanSimulateAGeneration()
		{
			var puzzle = Day12.ParseInput(_testInput);

			var result = puzzle.Simulate();

			Assert.Contains("#...#....#.....#..#..#..#", result);
		}

		[Fact]
		public void CanSimulate5Generations()
		{
			var puzzle = Day12.ParseInput(_testInput);

			var result = puzzle.SimulateMany(5);

			Assert.Contains("#...##...#.#..#..#...#...#", result);
		}

		[Fact]
		public void CanSimulate20Generations()
		{
			var puzzle = Day12.ParseInput(_testInput);

			var result = puzzle.SimulateMany(20);

			Assert.Contains("#....##....#####...#######....#.#..##", result);
		}

		[Fact]
		public void CanGetValueAfter20Generations()
		{
			var puzzle = Day12.ParseInput(_testInput);
			puzzle.SimulateMany(20);
			var result = puzzle.GetValueAtCurrentGeneration();

			Assert.Equal(325, result);
		}

		[Fact]
		public void Q1()
		{
			var puzzle = Day12.ParseInput(Inputs.Day12);
			puzzle.SimulateMany(20);
			var result = puzzle.GetValueAtCurrentGeneration();

			Assert.Equal(3051, result);
		}

		[Fact]
		public void Q2()
		{
			var puzzle = Day12.ParseInput(Inputs.Day12);
			puzzle.SimulateMany(50000000000);
			var result = puzzle.GetValueAtCurrentGeneration();

			Assert.Equal(1300000000669, result);
		}
	}
}