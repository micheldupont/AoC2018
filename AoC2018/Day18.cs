using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2018
{
	public class Day18
	{
		public char[,] Map;
		private int Lenght = 0;

		private List<char[,]> _seen = new List<char[,]>();

		public Day18(string input)
		{
			var lines = input.Split(new[] {'\r', '\n'}, StringSplitOptions.RemoveEmptyEntries);

			var xlenght = lines[0].Length;
			Lenght = xlenght;

			Map = new char[xlenght,lines.Length];

			for (var y = 0; y < lines.Length; y++)
			{
				var line = lines[y];
				var parts = line.ToCharArray();

				for (int x = 0; x < xlenght; x++)
				{
					Map[x, y] = parts[x];
				}
			}

			_seen.Add(Map);
		}

		public bool Mutate()
		{
			var mutatedMap = new char[Lenght, Lenght];

			for (int y = 0; y < Lenght; y++)
			{
				for (int x = 0; x < Lenght; x++)
				{
					var neighbors = GetNeighbors(x, y);

					mutatedMap[x, y] = GetMutationFor(Map[x,y], neighbors);
				}
			}

			var seenBefore = SeenBefore(mutatedMap);

			_seen.Add(mutatedMap);
			
			Map = mutatedMap;

			return seenBefore;
		}

		private bool SeenBefore(char[,] mutatedMap)
		{
			foreach (var map in _seen)
			{
				if (AreMapTheSame(map, mutatedMap))
					return true;
			}

			return false;
		}

		private bool AreMapTheSame(char[,] map, char[,] mutatedMap)
		{
			for (int y = 0; y < Lenght; y++)
			{
				for (int x = 0; x < Lenght; x++)
				{
					if (map[x, y] != mutatedMap[x, y])
						return false;
				}
			}

			return true;
		}

		private char GetMutationFor(char original, List<char> neighbors)
		{
			switch (original)
			{
				case '.':
				{
					var doMutation = neighbors.Count(c => c == '|') >= 3;
					return doMutation ? '|' : '.';
				}
					
				case '|':
				{
					var doMutation = neighbors.Count(c => c == '#') >= 3;
					return doMutation ? '#' : '|';
				}

				case '#':
				{
					var gotLumber = neighbors.Count(c => c == '#') >= 1;
					var gotTree = neighbors.Count(c => c == '|') >= 1;

					return (gotTree && gotLumber) ? '#' : '.';
				}
			}

			throw new Exception("uhh...");
		}

		private List<char> GetNeighbors(int x, int y)
		{
			var list = new List<char>();

			var minX = x - 1;
			if (minX < 0)
			{
				minX = 0;
			}

			var maxX = x + 1;
			if (maxX >= Lenght)
			{
				maxX = Lenght - 1;
			}

			var minY = y - 1;
			if (minY < 0)
			{
				minY = 0;
			}

			var maxY = y + 1;
			if (maxY >= Lenght)
			{
				maxY = Lenght - 1;
			}

			for (int ny = minY; ny <= maxY; ny++)
			{

				for (int nx = minX; nx <= maxX; nx++)
				{
					if(nx == x && ny == y)
						continue;

					list.Add(Map[nx,ny]);
				}
			}

			return list;
		}

		public int GetMutationTenScore()
		{
			for (int i = 0; i < 10; i++)
			{
				Mutate();
			}

			var woods = 0;
			var lumbers = 0;

			for (int y = 0; y < Lenght; y++)
			{
				for (int x = 0; x < Lenght; x++)
				{
					if (Map[x, y] == '#')
					{
						lumbers++;
					}

					if (Map[x, y] == '|')
					{
						woods++;
					}
				}
			}

			return woods * lumbers;
		}

		public int GetMutationStableScore()
		{
			var seenBefore = false;

			var minutes = 0;
			do
			{
				minutes++;
				seenBefore = Mutate();	
			} while (!seenBefore);

			var firstTime =_seen.FindIndex(m => AreMapTheSame(m, _seen[minutes]));

			var cycleLength = minutes - firstTime;

			var remaining = 1000000000 - minutes;
			var timeLeftAfterLastCycle = remaining % cycleLength;

			var lastMinute = firstTime + timeLeftAfterLastCycle;

			Map = _seen[lastMinute];

			var woods = 0;
			var lumbers = 0;

			for (int y = 0; y < Lenght; y++)
			{
				for (int x = 0; x < Lenght; x++)
				{
					if (Map[x, y] == '#')
					{
						lumbers++;
					}

					if (Map[x, y] == '|')
					{
						woods++;
					}
				}
			}

			return woods * lumbers;
		}
	}
}