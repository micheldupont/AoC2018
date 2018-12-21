using System.Collections.Generic;

namespace AoC2018
{
	public class Day21
	{
		protected internal Day19 Day19;

		public List<int> Registers => Day19.Registers;

		public Day21(string input)
		{
			Day19 = new Day19(input);
		}

		public void RunProgram()
		{
			Day19.RunProgram();
		}
	}
}