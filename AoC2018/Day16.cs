using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2018
{
	public class Day16
	{
		public List<IOpCode> OpCodes { get; }
		public List<int[]> Program { get; }

		public Day16()
		{
			OpCodes = new List<IOpCode>();
			Program = new List<int[]>();

			OpCodes.Add(new AddR());
			OpCodes.Add(new AddI());

			OpCodes.Add(new MulR());
			OpCodes.Add(new MulI());

			OpCodes.Add(new BanR());
			OpCodes.Add(new BanI());

			OpCodes.Add(new BorR());
			OpCodes.Add(new BorI());

			OpCodes.Add(new SetR());
			OpCodes.Add(new SetI());

			OpCodes.Add(new GtIR());
			OpCodes.Add(new GtRI());
			OpCodes.Add(new GtRR());

			OpCodes.Add(new EqIR());
			OpCodes.Add(new EqRI());
			OpCodes.Add(new EqRR());
		}

		public List<OpCodeTest> ParseTests(string input)
		{
			var lines = input.Split(new[] {'\r', '\n'}, StringSplitOptions.RemoveEmptyEntries);

			var list = new List<OpCodeTest>();
			int? lineCount = null;
			for (int i = 0; i < lines.Length; i += 3)
			{
				if (!lines[i].StartsWith("Before"))
				{
					lineCount = i;
					break;
				}

				var beforeLine = lines[i].Split(new []{'[',',',' ',']'}, StringSplitOptions.RemoveEmptyEntries);
				var opLine = lines[i+1].Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
				var afterLine = lines[i + 2].Split(new[] { '[', ',', ' ', ']' }, StringSplitOptions.RemoveEmptyEntries);

				list.Add(new OpCodeTest(
					int.Parse(opLine[0]), 
					int.Parse(opLine[1]), 
					int.Parse(opLine[2]), 
					int.Parse(opLine[3]),
					new List<int>{ int.Parse(beforeLine[1]), int.Parse(beforeLine[2]), int.Parse(beforeLine[3]), int.Parse(beforeLine[4]) },
					new List<int> { int.Parse(afterLine[1]), int.Parse(afterLine[2]), int.Parse(afterLine[3]), int.Parse(afterLine[4]) }
				));
			}

			if (lineCount.HasValue && lineCount.Value < lines.Length)
			{
				for (int i = lineCount.Value; i < lines.Length; i++)
				{
					var parts = lines[i].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

					Program.Add(new []
					{
						int.Parse(parts[0]),
						int.Parse(parts[1]),
						int.Parse(parts[2]),
						int.Parse(parts[3])
					});
				}
			}

			return list;
		}

		public List<OpCodeTest> RunTests(string input)
		{
			var tests = ParseTests(input);

			foreach (var opCodeTest in tests)
			{
				foreach (var opCode in OpCodes)
				{
					var beforeState = opCodeTest.RegisterBefore.Select(r => r).ToList();
					opCode.Run(ref beforeState, opCodeTest.A, opCodeTest.B, opCodeTest.C);

					if (beforeState.SequenceEqual(opCodeTest.RegisterAfter))
					{
						opCodeTest.MatchingOpCodes.Add(opCode);
					}
				}
			}

			return tests;
		}

		public List<IOpCode> ResolveOpCodes(string input)
		{
			var tests = RunTests(input);

			var opCodeWithoutId = OpCodes.Count(o => !o.OpCode.HasValue);

			while (opCodeWithoutId != 0)
			{
				var testWithOneMatchOnly = tests.First(t => t.MatchingOpCodes.Count == 1);
				var match = testWithOneMatchOnly.MatchingOpCodes.First();
				match.OpCode = testWithOneMatchOnly.OpCode;

				foreach (var opCodeTest in tests)
				{
					opCodeTest.MatchingOpCodes.Remove(match);
				}

				opCodeWithoutId = OpCodes.Count(o => !o.OpCode.HasValue);
			}

			return OpCodes;
		}

		public int ResolveOpCodesAndRun(string input)
		{
			var opCodes = ResolveOpCodes(input);
			var registers = new List<int> {0, 0, 0, 0};

			foreach (var line in Program)
			{
				var code = line[0];
				var opCode = opCodes.Find(c => c.OpCode == code);

				opCode.Run(ref registers, line[1], line[2], line[3]);
			}

			return registers[0];
		}

		public interface IOpCode
		{
			string Name { get; }
			int? OpCode { get; set; }

			void Run(ref List<int> registers, int a, int b, int output);
		}

		public class AddR : IOpCode
		{
			public string Name => "AddR";
			public int? OpCode { get; set; }
			public void Run(ref List<int> registers, int a, int b, int output)
			{
				registers[output] = registers[a] + registers[b];
			}
		}

		public class AddI : IOpCode
		{
			public string Name => "AddI";
			public int? OpCode { get; set; }
			public void Run(ref List<int> registers, int a, int b, int output)
			{
				registers[output] = registers[a] + b;
			}
		}

		public class MulR : IOpCode
		{
			public string Name => "MulR";
			public int? OpCode { get; set; }
			public void Run(ref List<int> registers, int a, int b, int output)
			{
				registers[output] = registers[a] * registers[b];
			}
		}

		public class MulI : IOpCode
		{
			public string Name => "MulI";
			public int? OpCode { get; set; }
			public void Run(ref List<int> registers, int a, int b, int output)
			{
				registers[output] = registers[a] * b;
			}
		}

		public class BanR : IOpCode
		{
			public string Name => "BanR";
			public int? OpCode { get; set; }
			public void Run(ref List<int> registers, int a, int b, int output)
			{
				registers[output] = registers[a] & registers[b];
			}
		}

		public class BanI : IOpCode
		{
			public string Name => "BanI";
			public int? OpCode { get; set; }
			public void Run(ref List<int> registers, int a, int b, int output)
			{
				registers[output] = registers[a] & b;
			}
		}

		public class BorR : IOpCode
		{
			public string Name => "BanR";
			public int? OpCode { get; set; }
			public void Run(ref List<int> registers, int a, int b, int output)
			{
				registers[output] = registers[a] | registers[b];
			}
		}

		public class BorI : IOpCode
		{
			public string Name => "BanI";
			public int? OpCode { get; set; }
			public void Run(ref List<int> registers, int a, int b, int output)
			{
				registers[output] = registers[a] | b;
			}
		}

		public class SetR : IOpCode
		{
			public string Name => "SetR";
			public int? OpCode { get; set; }
			public void Run(ref List<int> registers, int a, int b, int output)
			{
				registers[output] = registers[a];
			}
		}

		public class SetI : IOpCode
		{
			public string Name => "SetI";
			public int? OpCode { get; set; }
			public void Run(ref List<int> registers, int a, int b, int output)
			{
				registers[output] = a;
			}
		}

		public class GtIR : IOpCode
		{
			public string Name => "GtIR";
			public int? OpCode { get; set; }
			public void Run(ref List<int> registers, int a, int b, int output)
			{
				registers[output] = (a > registers[b]) ? 1 : 0;
			}
		}

		public class GtRI : IOpCode
		{
			public string Name => "GtRI";
			public int? OpCode { get; set; }
			public void Run(ref List<int> registers, int a, int b, int output)
			{
				registers[output] = (registers[a] > b) ? 1 : 0;
			}
		}

		public class GtRR : IOpCode
		{
			public string Name => "GtRR";
			public int? OpCode { get; set; }
			public void Run(ref List<int> registers, int a, int b, int output)
			{
				registers[output] = (registers[a] > registers[b]) ? 1 : 0;
			}
		}

		public class EqIR : IOpCode
		{
			public string Name => "EqIR";
			public int? OpCode { get; set; }
			public void Run(ref List<int> registers, int a, int b, int output)
			{
				registers[output] = (a == registers[b]) ? 1 : 0;
			}
		}

		public class EqRI : IOpCode
		{
			public string Name => "EqRI";
			public int? OpCode { get; set; }
			public void Run(ref List<int> registers, int a, int b, int output)
			{
				registers[output] = (registers[a] == b) ? 1 : 0;
			}
		}

		public class EqRR : IOpCode
		{
			public string Name => "EqRR";
			public int? OpCode { get; set; }
			public void Run(ref List<int> registers, int a, int b, int output)
			{
				registers[output] = (registers[a] == registers[b]) ? 1 : 0;
			}
		}

		public class OpCodeTest
		{
			public List<int> RegisterBefore { get; set; }
			public List<int> RegisterAfter { get; set; }

			public int OpCode { get; set; }
			public int A { get; set; }
			public int B { get; set; }
			public int C { get; set; }
			public List<IOpCode> MatchingOpCodes { get;}

			public OpCodeTest(int opCode, int a, int b, int c, List<int> registerBefore, List<int> registerAfter)
			{
				OpCode = opCode;
				A = a;
				B = b;
				C = c;
				RegisterBefore = registerBefore;
				RegisterAfter = registerAfter;

				MatchingOpCodes = new List<Day16.IOpCode>();
			}
		}
	}
}