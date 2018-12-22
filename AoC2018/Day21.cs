using System.Collections.Generic;
using System.Linq;

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

	    public class Prog
	    {
	        public int Reg0, /*Reg1,*/ Reg2, Reg3, Reg4, Reg5;

	        public int Run(int reg0 = 0)
	        {
	            Reg0 = reg0;
	            Reg2 = Reg3 = Reg4 = Reg5 = 0;
                var timeout = 0;

	            var lastReg4 = 0;

	   /*Line0:   Reg4 = 123;
	   Line1:   Reg4 = Reg4 & 456;
	            Reg4 = Reg4 == 72 ? 1 : 0;
	            if (Reg4 == 1) goto Line5;
	            goto Line1;*/
	   Line5:   Reg4 = 0;
	   Line6:   Reg3 = Reg4 | 65536;
	            Reg4 = 12670166;
	   Line8:   Reg2 = Reg3 & 255; timeout++; if(timeout > 1000000) return lastReg4;
                Reg4 = Reg4 + Reg2;
	            Reg4 = Reg4 & 16777215;
	            Reg4 = Reg4 * 65899;
	            Reg4 = Reg4 & 16777215;
	            Reg2 = 256 > Reg3 ? 1 : 0;
	            if (Reg2 == 1) goto Line16;
	            goto Line17;
      Line16:   goto Line28;
      Line17:   Reg2 = 0;
	  Line18:   Reg5 = Reg2 + 1;
	            Reg5 = Reg5 * 256;
	            Reg5 = Reg5 > Reg3 ? 1 : 0;
	            if (Reg5 == 1) goto Line23;
	            goto Line24;
      Line23:   goto Line26;
      Line24:   Reg2 = Reg2 + 1;
	            goto Line18;
	  Line26:   Reg3 = Reg2;
	            goto Line8;
      Line28:   Reg2 = Reg4 == Reg0 ? 1 : 0; lastReg4 = Reg4; timeout = 0;
                if (Reg2 == 1) goto End;
	            goto Line6;
      End:      return lastReg4;
	        }

	        public int Run2(int reg0 = 0)
	        {
	            Reg0 = reg0;
	            Reg2 = Reg3 = Reg4 = Reg5 = 0;
	           
	            List<int> values = new List<int>();

	    Line6:  Reg3 = Reg4 | 0x10000;
	            Reg4 = 12670166;
	    Line8:  Reg2 = Reg3 & 255;
	            Reg4 += Reg2;
	            Reg4 &= 16777215;
	            Reg4 *= 65899;
	            Reg4 &= 16777215;

	            Reg2 = 256 > Reg3 ? 1 : 0;
	            if (Reg2 != 1) goto Line17;
	            goto Line28;
	    Line17: Reg2 = 0;
	    Line18: Reg5 = Reg2 + 1;
	            Reg5 *= 256;
	            Reg5 = Reg5 > Reg3 ? 1 : 0;
	            if (Reg5 != 1) goto Line24;
	            goto Line26;
	    Line24: Reg2 = Reg2 + 1;
	            goto Line18;
	    Line26: Reg3 = Reg2;
	            goto Line8;
	    Line28: Reg2 = Reg4 == Reg0 ? 1 : 0;

	            if (values.Contains(Reg4))
	            {
	                return values.Last();
	            }
	            values.Add(Reg4);
	            
	            if (Reg2 == 1) return Reg4;
	            goto Line6;
	        }

	       
	    }
	}
}