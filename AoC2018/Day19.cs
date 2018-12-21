using System;
using System.Collections.Generic;

namespace AoC2018
{
	public class Day19
	{
		public InstructionPointer InstructionPointer;
		public List<Instruction> Program;
		public List<int> Registers;


		public Day19(string testInput)
		{
			Registers = new List<int>{0,0,0,0,0,0};

			var lines = testInput.Split(new[] {'\r', '\n'}, StringSplitOptions.RemoveEmptyEntries);

			var ipRegister = int.Parse(lines[0].Split(new []{' '}, StringSplitOptions.RemoveEmptyEntries)[1]);

			InstructionPointer = new InstructionPointer(ipRegister);
			Program = new List<Instruction>();

			for (int i = 1; i < lines.Length; i++)
			{
				var parts = lines[i].Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
				var inst = new Instruction();
				inst.Inputs = new[] {int.Parse(parts[1]), int.Parse(parts[2]), int.Parse(parts[3])};

				switch (parts[0])
				{
					case "addr":
						inst.OpCode = new Day16.AddR();
						break;
					case "addi":
						inst.OpCode = new Day16.AddI();
						break;
					case "mulr":
						inst.OpCode = new Day16.MulR();
						break;
					case "muli":
						inst.OpCode = new Day16.MulI();
						break;
					case "banr":
						inst.OpCode = new Day16.BanR();
						break;
					case "bani":
						inst.OpCode = new Day16.BanI();
						break;
					case "borr":
						inst.OpCode = new Day16.BorR();
						break;
					case "bori":
						inst.OpCode = new Day16.BorI();
						break;
					case "setr":
						inst.OpCode = new Day16.SetR();
						break;
					case "seti":
						inst.OpCode = new Day16.SetI();
						break;
					case "gtir":
						inst.OpCode = new Day16.GtIR();
						break;
					case "gtri":
						inst.OpCode = new Day16.GtRI();
						break;
					case "gtrr":
						inst.OpCode = new Day16.GtRR();
						break;
					case "eqir":
						inst.OpCode = new Day16.EqIR();
						break;
					case "eqri":
						inst.OpCode = new Day16.EqRI();
						break;
					case "eqrr":
						inst.OpCode = new Day16.EqRR();
						break;
					default:
						throw new Exception("unknown opcode:" + parts[0]);
				}

				Program.Add(inst);
			}
		}

		public void RunProgram()
		{
			var isProgramEnd = false;

			do
			{
				isProgramEnd = InstructionPointer.ExecuteNextInstruction(Program, Registers);
			} while (!isProgramEnd);
		}

		public bool StepProgram()
		{
			return InstructionPointer.ExecuteNextInstruction(Program, Registers);
		}
	}

	public class InstructionPointer
	{
		public int IpRegisterIndex { get; }
		public int CurrentIp { get; set; }

		public HashSet<int> DebugValues { get; set; }

		public InstructionPointer(int ipRegisterIndex)
		{
			IpRegisterIndex = ipRegisterIndex;
			CurrentIp = 0;

			DebugValues = new HashSet<int>();
		}

		public bool ExecuteNextInstruction(List<Instruction> program, List<int> registers)
		{
			UpdateIpRegister(registers);

			//for day21 Q1
			/*	if (CurrentIp == 28)
				{
					throw new Exception("Key condition! - value: " + registers[4]);
				}*/

			//day 21 Q2
			if (CurrentIp == 28)
			{
				if(!DebugValues.Contains(registers[4]))
					DebugValues.Add(registers[4]);
				else
					throw new Exception("value repeating! : " + DebugValues);
			}

			program[CurrentIp].Run(registers);
			UpdateCurrentIp(registers);

			return CurrentIp >= program.Count;
		}

		private void UpdateCurrentIp(List<int> registers)
		{
			CurrentIp = registers[IpRegisterIndex];
			CurrentIp++;
		}

		private void UpdateIpRegister(List<int> registers)
		{
			registers[IpRegisterIndex] = CurrentIp;
		}
	}

	public class Instruction
	{
		public Day16.IOpCode OpCode { get; set; }
		public int[] Inputs { get; set; }

		public Instruction()
		{
	
		}

		public void Run(List<int> registers)
		{
			OpCode.Run(ref registers, Inputs[0], Inputs[1], Inputs[2]);
		}
	}
}