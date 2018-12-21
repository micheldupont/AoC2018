using Xunit;

namespace AoC2018.tests
{
	/*
	#ip 1


	( 0) seti 123 0 4			reg[4] = 123									//
	( 1) bani 4 456 4			reg[4] = reg[4] & 456							//
	( 2) eqri 4 72 4			reg[4] = reg[4] == 72 ? 1 : 0					// 123 & 456 = 72, donc reg[4] = 1
	
*	( 3) addr 4 1 1				reg[1] = reg[4] + reg[1] //si reg[4] == 72 saute à (5)
*	( 4) seti 0 0 1				reg[1] = 0				 //			sinon retour à (1)

   ----- check done... on revient jamais en haut... ---------------


	( 5) seti 0 4 4				reg[4] = 0

	( 6) bori 4 65536 3			reg[3] = reg[4] | 65536             //3: 65536
	( 7) seti 12670166 8 4		reg[4] = 12670166					//4: 12670166
	( 8) bani 3 255 2			reg[2] = reg[3] & 255				//2:  0
	( 9) addr 4 2 4				reg[4] = reg[4] + reg[2]			//4: 12670166
	(10) bani 4 16777215 4		reg[4] = reg[4] & 16777215			//4: 12670166
	(11) muli 4 65899 4			reg[4] = reg[4] * 65899				//4: 934951269234
	(12) bani 4 16777215 4		reg[4] = reg[4] & 16777215			//4: 16337778
	(13) gtir 256 3 2			reg[2] = 256 > reg[3] ? 1 : 0		//2: 0
	
*	(14) addr 2 1 1				reg[1] = reg[2] + reg[1]     //si reg[2] == 1 saute à (16 -> 28)
*	(15) addi 1 1 1				reg[1] = reg[1] + 1			 //			sinon continue à (17)
*	(16) seti 27 6 1			reg[1] = 27

	(17) seti 0 0 2				reg[2] = 0									//2: 0
	(18) addi 2 1 5				reg[5] = reg[2] + 1							//5: 1
	(19) muli 5 256 5			reg[5] = reg[5] * 256						//5: 256
	(20) gtrr 5 3 5				reg[5] = reg[5] > reg[3] ? 1 : 0			//5: 256 > 65536 -> 0
	
*	(21) addr 5 1 1				reg[1] = reg[5] + reg[1]					//si reg5 > reg 3 : saute à (23 -> 26)
*	(22) addi 1 1 1				reg[1] = reg[1] + 1							//sinon continue à (24)
*	(23) seti 25 6 1			reg[1] = 25

	(24) addi 2 1 2				reg[2] = reg[2] + 1				//2: = 2:++
*	(25) seti 17 8 1			reg[1] = 17						// va à (18)

	(26) setr 2 5 3				reg[3] = reg[2]

*	(27) seti 7 2 1				reg[1] = 7		          // saute à (8)
	
	(28) eqrr 4 0 2				reg[2] = reg[4] == reg[0] ? 1 : 0
*	(29) addr 2 1 1				reg[1] = reg[2] + reg[1] // si reg[4] == reg[0] -> FIN
*	(30) seti 5 8 1				reg[1] = 5               // saute à (6)
	*/

	public class Day21Tests
	{
		[Fact]
		public void Q1()
		{
			var day = new Day21(Inputs.Day21);

			day.Registers[0] = 15690445;

			day.RunProgram();

			Assert.True(true);
		}

		[Fact]
		public void Q2()
		{
			var day = new Day21(Inputs.Day21);

			day.Registers[0] = 0;

			day.RunProgram();

			Assert.True(true);
		}
	}
}
 