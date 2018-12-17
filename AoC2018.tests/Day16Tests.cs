using System.Linq;
using Xunit;

namespace AoC2018.tests
{
	public class Day16Tests
	{
		[Fact]
		public void CanParseTestInputs()
		{
			var input = @"Before: [3, 2, 1, 1]
9 2 1 2
After:  [3, 2, 2, 1]";

			var day = new Day16();

			var tests = day.ParseTests(input);

			Assert.Equal(1, tests.Count);
		}

		[Fact]
		public void CanRunTests()
		{
			var input = @"Before: [3, 2, 1, 1]
9 2 1 2
After:  [3, 2, 2, 1]";

			var day = new Day16();

			var tests = day.RunTests(input);

			Assert.Equal(3, tests[0].MatchingOpCodes.Count);
		}

		[Fact]
		public void Q1()
		{
			var day = new Day16();

			var tests = day.RunTests(Inputs.Day16);

			var testsWithThreeOrMoreOpCodes = tests.Where(t => t.MatchingOpCodes.Count >= 3);

			Assert.Equal(531, testsWithThreeOrMoreOpCodes.Count());
		}

		[Fact]
		public void CanResolveOpCodes()
		{
			var day = new Day16();
			var opcodes = day.ResolveOpCodes(Inputs.Day16);

			Assert.Equal(16, opcodes.Count(c => c.OpCode.HasValue));
		}

		[Fact]
		public void Q2()
		{
			var day = new Day16();
			var r0 = day.ResolveOpCodesAndRun(Inputs.Day16);

			Assert.Equal(649, r0);
		}
	}
}