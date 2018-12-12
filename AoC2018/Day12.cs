using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2018
{
	public class Day12
	{
		/* . . . # #   = >   #  */

		/* 0 1 2 3 4 5 6 7 8 9 */

		public static SpreadRule ParseRule(string input)
		{
			var parts = input.ToCharArray();

			var pattern = new[]
			{
				parts[0] == '#',
				parts[1] == '#',
				parts[2] == '#',
				parts[3] == '#',
				parts[4] == '#'
			};

			var yieldsPlant = parts[9] == '#';

			return new SpreadRule(pattern, yieldsPlant);
		}

		public static Puzzle ParseInput(string testInput)
		{
			var lines = testInput.Split(new[] {'\r', '\n'}, StringSplitOptions.RemoveEmptyEntries);

			var initialStateString = lines[0].Replace("initial state: ", "");
			var initialStateArray = initialStateString.ToCharArray();

			var rules = new List<SpreadRule>();
			for (int i = 1; i < lines.Length; i++)
			{
				rules.Add(ParseRule(lines[i]));
			}

			return new Puzzle(initialStateArray, rules);
		}

		public class Puzzle
		{
			public char[] InitialState { get; }
			public List<SpreadRule> Rules { get; }

			private List<char> _currentState;
			private long _minPotIndex;

			private List<string> _oldStates = new List<string>();

			public Puzzle(char[] initialState, List<SpreadRule> rules)
			{
				InitialState = initialState;
				_currentState = new List<char>((char[])initialState.Clone());
				Rules = rules;
				_minPotIndex = 0;
			}

			public string Simulate()
			{
				var firstIndexWithPlant = FindFirstIndexWithPlant();
				var minimumRequiredIndex = firstIndexWithPlant - 4;
				if (minimumRequiredIndex < 0)
				{
					for (var i = minimumRequiredIndex; i < 0; i++)
					{
						_currentState.Insert(0, '.');
						_minPotIndex--;
					}
				}


				var lastIndexWithPlant = FindLastIndexWithPlant();
				var maxRequiredIndex = lastIndexWithPlant + 4;

				if (maxRequiredIndex > _currentState.Count -1)
				{
					var diff = maxRequiredIndex - (_currentState.Count - 1);
					for (var i = 0; i < diff; i++)
					{
						_currentState.Add('.');
					}
				}

				var nextState = new List<char>(new []{'.','.'});

				var max = _currentState.Count - 3;
				for (var i = 2; i < max; i++)
				{
					var currentSample = _currentState.GetRange(i-2, 5).ToArray();
					var matchingRule = Rules.FirstOrDefault(r => r.IsMatch(currentSample));

					if (matchingRule != null)
					{
						nextState.Add(matchingRule.YieldsPlant ? '#' : '.');
					}
					else
					{
						nextState.Add('.');
					}
				}

				_currentState = nextState;
				return new string(nextState.ToArray());
			}

			private int FindLastIndexWithPlant()
			{
				for (int i = _currentState.Count -1; i >= 0; i--)
				{
					if (_currentState[i] == '#')
					{
						return i;
					}
				}

				throw new Exception("no plant in set!");
			}

			private int FindFirstIndexWithPlant()
			{
				for (int i = 0; i < _currentState.Count; i++)
				{
					if (_currentState[i] == '#')
					{
						return i;
					}
				}

				throw new Exception("no plant in set!");
			}

			public string SimulateMany(long generationCount)
			{
				var previousState = "";
				var previousFirstIndex = 0;
				for (long i = 0; i < generationCount; i++)
				{
					var state = Simulate();
					var start = state.IndexOf('#');
					var end = state.LastIndexOf('#');
					state = state.Substring(start, end - start + 1);

					if (state == previousState) //dev observation: from this point on, the state stays the same, but "slides" to the right by 1 each generation
					{
						var currentFirstIndex = FindFirstIndexWithPlant();
						var step = currentFirstIndex - previousFirstIndex;

						var loopLeft = generationCount - i -1;

						_minPotIndex += (loopLeft * step);

						return state;
					}
					else
					{
						previousState = state;
						previousFirstIndex = FindFirstIndexWithPlant();
					}

				}

				return new string(_currentState.ToArray());
			}

			public long GetValueAtCurrentGeneration()
			{
				var currentValue = _minPotIndex;
				var sum = 0L;
				for (int i = 0; i < _currentState.Count; i++)
				{
					var currentPot = _currentState[i];
					if (currentPot == '#')
					{
						sum += currentValue;
					}

					currentValue++;
				}

				return sum;
			}
		}

		public class SpreadRule
		{
			public bool YieldsPlant { get; }
			public bool[] Pattern { get; }

			public SpreadRule(bool[] pattern, bool yieldsPlant)
			{
				YieldsPlant = yieldsPlant;
				Pattern = pattern;
			}

			public bool IsMatch(char[] input)
			{
				if (input.Length != 5)
				{
					throw new Exception("unexpected array size: " + input.Length);
				}

				for (int i = 0; i < 5; i++)
				{
					var patternTest = Pattern[i];
					var inputValue = input[i] == '#';

					if (patternTest != inputValue)
					{
						return false;
					}
				}

				return true;
			}
		}
	}
}
 