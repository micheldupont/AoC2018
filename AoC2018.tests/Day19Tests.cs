﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;

namespace AoC2018.tests
{
	public class Day19Tests : IDisposable
	{
		private string _testInput = @"#ip 0
seti 5 0 1
seti 6 0 2
addi 0 1 0
addr 1 2 3
setr 1 0 0
seti 8 0 4
seti 9 0 5";

		protected internal StreamWriter Sw;

		public Day19Tests()
		{
			Sw = new StreamWriter("./out.txt");

		}

		public void Dispose()
		{
			Sw.Close();
			Sw.Dispose();
		}

		private void Render(List<int> registers)
		{
			Sw.WriteLine("[ " + string.Join(", ", registers) + " ]");	
			//Sw.WriteLine();
		}

		[Fact]
		public void CanParse()
		{
			var day = new Day19(_testInput);

			Assert.Equal(0, day.InstructionPointer.IpRegisterIndex);
			Assert.Equal(7, day.Program.Count);
			Assert.IsType<Day16.SetI>(day.Program[1].OpCode);
		}

		[Fact]
		public void CanRunSample()
		{
			var day = new Day19(_testInput);
			day.RunProgram();

			Assert.Equal(6, day.Registers[0]);
		}

		[Fact]
		public void Q1()
		{
			var day = new Day19(Inputs.Day19);
			day.RunProgram();

			Assert.Equal(1568, day.Registers[0]);
		}

		//[Fact]
		public void LoopTest()
		{
			var day = new Day19(Inputs.Day19) { Registers = { [0] = 1 }};
			Render(day.Registers);

			for (int i = 0; i < 900000; i++)
			{
				var ended = day.StepProgram();
				if (ended)
				{
					break;
				}
			}

			for (int i = 0; i < 500000; i++)
			{
				var ended = day.StepProgram();
				Render(day.Registers);
				if (ended)
				{
					break;
				}		
			}
			
			Assert.True(true);
		}

		/* loop
		    IP |R: 0      1     2     3     4       5
			    
			3  | [ 0, 10551292, 2,  112505, 0,      1 ] 
			4  | [ 0, 10551292, 3,  112505, 112505, 1 ] <- (3) mulr 5 3 4  reg[4] = reg[5] * reg[3]  // 1 * 112505
			5  | [ 0, 10551292, 4,  112505, 0,      1 ] <- (4) eqrr 4 1 4  reg[4] = reg[4] == reg[1] // 112505 == 10551292 (0)
			6  | [ 0, 10551292, 5,  112505, 0,      1 ] <- (5) addr 4 2 2  reg[2] = reg[4] + reg[2]  // 0 + 5
			8  | [ 0, 10551292, 7,  112505, 0,      1 ] <- (6) addi 2 1 2  reg[2] = reg[2] + 1
			----------------------------------- ------------------------------------------------------------------------------2  | [ 0, 10551292, 2, 112505, 0,      1 ] <- (7) addr 5 0 0  reg[0] = reg[5] + reg[0]
			9  | [ 0, 10551292, 8,  112506, 0,      1 ] <- (8) addi 3 1 3  reg[3] = reg[3] + 1
			10 | [ 0, 10551292, 9,  112506, 0,      1 ] <- (9) gtrr 3 1 4  reg[4] = reg[3] > reg[1] // 112506 > 10551292 (0)
			11 | [ 0, 10551292, 10, 112506, 0,      1 ] <- (10) addr 2 4 2  reg[2] = reg[2] + reg[4] //10 + 0
			3  | [ 0, 10551292, 2,  112506, 0,      1 ] <- (11) seti 2 1 2  reg[2] = 2
			 		 
			 
			 */

		/* loop 2
		    IP |R: 0      1     2       3     4         5
			    
			3  | [ 0, 10551292, 2,  10551292, 0,        1 ] 
			4  | [ 0, 10551292, 3,  10551292, 10551292, 1 ] <- (3) mulr 5 3 4  reg[4] = reg[5] * reg[3]  // 1 * 10551292
			5  | [ 0, 10551292, 4,  10551292, 1,        1 ] <- (4) eqrr 4 1 4  reg[4] = reg[4] == reg[1] // 10551292 == 10551292 (1)
			7  | [ 0, 10551292, 6,  10551292, 0,        1 ] <- (5) addr 4 2 2  reg[2] = reg[4] + reg[2]  // 1 + 5
			------------------------------------------------------------------------------------------------------------------8  | [ 0, 10551292, 7,  10551292, 0,        1 ] <- (6) addi 2 1 2  reg[2] = reg[2] + 1
			8  | [ 1, 10551292, 7,  10551292, 0,        1 ] <- (7) addr 5 0 0  reg[0] = reg[5] + reg[0] // 1+ 0
			9  | [ 1, 10551292, 8,  10551293, 0,        1 ] <- (8) addi 3 1 3  reg[3] = reg[3] + 1
			10 | [ 1, 10551292, 9,  10551293, 1,        1 ] <- (9) gtrr 3 1 4  reg[4] = reg[3] > reg[1] // 10551293 > 10551292 (1)
			12 | [ 1, 10551292, 11, 10551293, 0,        1 ] <- (10) addr 2 4 2 reg[2] = reg[2] + reg[4] //10 + 1
			---------------------------------------------------------------------------------------------------------------------------- 3  | [ 0, 10551292, 2,  10551293, 0,        1 ] <- (11) seti 2 1 2  reg[2] = 2
			13 | [ 1, 10551292, 12, 10551293, 0,        2 ] <- (12) addi 5 1 5 reg[5] = reg[5] + 1
			14 | [ 1, 10551292, 13, 10551293, 0,        2 ] <- (13) gtrr 5 1 4 reg[4] = reg[5] > reg[1]  // 2 > 10551292 (0)
			15 | [ 1, 10551292, 14, 10551293, 0,        2 ] <- (14) addr 4 2 2 reg[2] = reg[4] + reg[2]
			2  | [ 1, 10551292, 1,  10551293, 0,        2 ] <- (15) seti 1 1 2 reg[2] = 1

			3  | [ 1, 10551292, 2,  1,        0,        2 ] <- (2)  seti 1 0 3 reg[3] = 1




			alt...

			13 | [ 1, 10551292, 12, 10551293, 0,        10551293 ] <- (12) addi 5 1 5 reg[5] = reg[5] + 1
			14 | [ 1, 10551292, 13, 10551293, 1,        2 ] <- (13) gtrr 5 1 4 reg[4] = reg[5] > reg[1]  // 10551292 > 10551292 (1)
			16 | [ 1, 10551292, 15, 10551293, 1,        2 ] <- (14) addr 4 2 2 reg[2] = reg[4] + reg[2]

			257 | [ 1, 10551292, 256, 10551293, 1,       2 ] <- (16) mulr 2 2 2 reg[2] = reg[2] * reg[2] 
			
			 */


		/* loop 3
		    IP |R: 0      1     2       3     4         5
			    
			3  | [ 1, 10551292, 2,  10551292, 0,        2 ] 
			4  | [ 1, 10551292, 3,  10551292, 21102584, 2 ] <- (3) mulr 5 3 4  reg[4] = reg[5] * reg[3]  // 2 * 10551292
			5  | [ 1, 10551292, 4,  10551292, 0,        2 ] <- (4) eqrr 4 1 4  reg[4] = reg[4] == reg[1] // 21102584 == 10551292 (0)
			6  | [ 1, 10551292, 5,  10551292, 0,        2 ] <- (5) addr 4 2 2  reg[2] = reg[4] + reg[2]  // 0 + 5
			8  | [ 1, 10551292, 7,  10551292, 0,        2 ] <- (6) addi 2 1 2  reg[2] = reg[2] + 1
			-----------------------------------------------------------------------------------------------------------------------------8  | [ 1, 10551292, 7,  10551292, 0,        2 ] <- (7) addr 5 0 0  reg[0] = reg[5] + reg[0] // 1+ 0
			9  | [ 1, 10551292, 8,  10551293, 0,        2 ] <- (8) addi 3 1 3  reg[3] = reg[3] + 1
			10 | [ 1, 10551292, 9,  10551293, 1,        2 ] <- (9) gtrr 3 1 4  reg[4] = reg[3] > reg[1] // 10551293 > 10551292 (1)
			12 | [ 1, 10551292, 11, 10551293, 1,        2 ] <- (10) addr 2 4 2 reg[2] = reg[2] + reg[4] //10 + 1
			---------------------------------------------------------------------------------------------------------------------------- 3  | [ 0, 10551292, 2,  10551293, 0,        1 ] <- (11) seti 2 1 2  reg[2] = 2
			13 | [ 1, 10551292, 12, 10551293, 1,        3 ] <- (12) addi 5 1 5 reg[5] = reg[5] + 1
			14 | [ 1, 10551292, 13, 10551293, 0,        3 ] <- (13) gtrr 5 1 4 reg[4] = reg[5] > reg[1]  // 2 > 10551292 (0)
			15 | [ 1, 10551292, 14, 10551293, 0,        3 ] <- (14) addr 4 2 2 reg[2] = reg[4] + reg[2]
			2  | [ 1, 10551292, 1,  10551293, 0,        3 ] <- (15) seti 1 1 2 reg[2] = 1

			3  | [ 1, 10551292, 2,  1,        0,        3 ] <- (2)  seti 1 0 3 reg[3] = 1




			alt...

			13 | [ 1, 10551292, 12, 10551293, 0,        10551293 ] <- (12) addi 5 1 5 reg[5] = reg[5] + 1
			14 | [ 1, 10551292, 13, 10551293, 1,        2 ] <- (13) gtrr 5 1 4 reg[4] = reg[5] > reg[1]  // 10551292 > 10551292 (1)
			16 | [ 1, 10551292, 15, 10551293, 1,        2 ] <- (14) addr 4 2 2 reg[2] = reg[4] + reg[2]

			257 | [ 1, 10551292, 256, 10551293, 1,       2 ] <- (16) mulr 2 2 2 reg[2] = reg[2] * reg[2] 
			
			 */

		

		[Fact]
		public void Q2Form()
		{
			var target = 0;

			for (int i = 1; i <= 10551292; i++)
			{
				if (10551292 % i == 0)
				{
					target = target + i;
				}
			}

			Assert.Equal(19030032, target);
		}
	}
}