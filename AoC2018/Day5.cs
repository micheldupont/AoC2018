using System;
using System.Globalization;

namespace AoC2018
{
	public class Day5
	{
		private readonly string _input;
		private readonly CultureInfo _cultureInfo;

		public Day5(string input)
		{
			this._input = input;
			_cultureInfo = CultureInfo.CurrentCulture;
		}

		public string Reduce(string input = null)
		{
			if (input == null)
			{
				input = _input;
			}
			var beforeLength = input.Length;
			var reduced = true;
			do
			{
				input = DoReducePass(input);
				var newLength = input.Length;
				reduced = newLength != 0 && newLength < beforeLength;
				beforeLength = newLength;
			} while (reduced);

			

			return input;
		}

		private string DoReducePass(string input)
		{
			var inputArray = input.ToCharArray();

			for (int i = 0; i < inputArray.Length; i++)
			{
				if ((i + 1) < inputArray.Length && inputArray[i] != '-')
				{
					var current = inputArray[i];
					var next = inputArray[i + 1];

					var currentOpposite = Invert(current);

					if (currentOpposite == next)
					{
						inputArray[i] = '-';
						inputArray[i + 1] = '-';
					}
				}
			}
			var finalString = new string(inputArray).Replace("-", "");

			return finalString;
		}

		private char Invert(char current)
		{			
			if (Char.IsUpper(current))
			{
				return Char.ToLower(current, _cultureInfo);
			}

			return Char.ToUpper(current, _cultureInfo);
		}

		public string Optimize(string targetPolymer)
		{
			var targetLowerCase = targetPolymer.ToLower();
			var targetUpCase = targetPolymer.ToUpper();

			var cleanedInput = _input.Replace(targetLowerCase, "").Replace(targetUpCase, "");

			var result = Reduce(cleanedInput);

			return result;
		}

		public string OptimizeForAllPolymers()
		{
			var optimisationsToTry = "bcdefghijklmnopqrstuvwxyz".ToCharArray();
			Console.WriteLine("simulating a...");
			var bestResult = Optimize("a");

			foreach (var optimisation in optimisationsToTry)
			{
				Console.WriteLine("simulating " + optimisation + "...");
				var r = Optimize(new string(new []{optimisation}));

				if (r.Length < bestResult.Length)
				{
					bestResult = r;
				}
			}

			return bestResult;
		}
	}
}