using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace AoC2018
{
	public class Day20
	{
		private readonly char[] _steps;
		private readonly List<Coordinate> _coordinates;


		public char[,] Map;

		public Day20(string input)
		{
			_steps = input.ToCharArray();
			_coordinates = new List<Coordinate>();
		}

		public void BuildMap()
		{
			GenerateCoordinates();
			NormalizeCoordinates();
			GenerateMap();
		}

		public int SearchForLongestPath()
		{
			var root = _coordinates[0];
			var frontier = new List<Coordinate> { root };
			var seen = new List<Coordinate> { root };

			var doors = 0;

			while (frontier.Count > 0)
			{
				doors++;
				var c = frontier.Count;

				for (int i = 0; i < c; i++)
				{
					var current = frontier.First();
					frontier.Remove(current);

					var neighbors = GetNeighbors(current);
					var secondNeighbors = GetSecondLevelNeighbors(current);

					for (var index = 0; index < neighbors.Count; index++)
					{
						var neighbor = neighbors[index];

						if (!seen.Contains(neighbor))
						{
							if (neighbor.C == '|' || neighbor.C == '-')
							{
								frontier.Add(secondNeighbors[index]);
								seen.Add(neighbor);
								seen.Add(secondNeighbors[index]);
							}
						}
					}
				}
			}

			return doors -1;
		}

		public int SearchFarRooms()
		{
			var root = _coordinates[0];
			var frontier = new List<Coordinate> { root };
			var seen = new List<Coordinate> { root };

			var doors = 0;
			var rooms = 0;

			while (frontier.Count > 0)
			{
				doors++;
				var c = frontier.Count;

				for (int i = 0; i < c; i++)
				{
					var current = frontier.First();
					frontier.Remove(current);

					var neighbors = GetNeighbors(current);
					var secondNeighbors = GetSecondLevelNeighbors(current);

					for (var index = 0; index < neighbors.Count; index++)
					{
						var neighbor = neighbors[index];

						if (!seen.Contains(neighbor))
						{
							if (neighbor.C == '|' || neighbor.C == '-')
							{
								frontier.Add(secondNeighbors[index]);
								seen.Add(neighbor);
								seen.Add(secondNeighbors[index]);
								if (doors >= 1000)
								{
									rooms++;
								}
							}
						}
					}
				}
			}

			return rooms;
		}

		private List<Coordinate> GetNeighbors(Coordinate current)
		{
			var top = new Coordinate(current.X, current.Y - 1, Map[current.X, current.Y - 1]);
			var left = new Coordinate(current.X - 1, current.Y, Map[current.X - 1, current.Y]);
			var right = new Coordinate(current.X + 1, current.Y, Map[current.X + 1, current.Y]);
			var bottom = new Coordinate(current.X, current.Y + 1, Map[current.X, current.Y + 1]);

			return new List<Coordinate> { top, left, right, bottom };
		}

		private List<Coordinate> GetSecondLevelNeighbors(Coordinate current)
		{
			var top = new Coordinate(current.X, current.Y - 2, Map[current.X, current.Y - 2]);
			var left = new Coordinate(current.X - 2, current.Y, Map[current.X - 2, current.Y]);
			var right = new Coordinate(current.X + 2, current.Y, Map[current.X + 2, current.Y]);
			var bottom = new Coordinate(current.X, current.Y + 2, Map[current.X, current.Y + 2]);

			return new List<Coordinate> { top, left, right, bottom };
		}

		private void GenerateMap()
		{
			var maxX = _coordinates.Max(c => c.X);
			var maxY = _coordinates.Max(c => c.Y);

			Map = new char[maxX + 3, maxY + 3];

			for (int y = 0; y < maxY + 3; y++)
			{
				for (int x = 0; x < maxX + 3; x++)
				{
					Map[x, y] = '#';
				}
			}

			foreach (var coordinate in _coordinates)
			{
				Map[coordinate.X, coordinate.Y] = coordinate.C;
			}
		}

		private void NormalizeCoordinates()
		{
			var minX = _coordinates.Min(c => c.X);
			var offsetX = 0 - minX + 2;
			var minY = _coordinates.Min(c => c.Y);
			var offsetY = 0 - minY + 2;

			foreach (var coordinate in _coordinates)
			{
				coordinate.X = coordinate.X + offsetX;
				coordinate.Y = coordinate.Y + offsetY;
			}
		}

		private void GenerateCoordinates()
		{
			var root = new Coordinate(0, 0, 'X');
			_coordinates.Add(root);

			Step(1, root);
		}

		private void Step(int index, Coordinate previousCoordinate, bool detour = false)
		{
			var currentStep = _steps[index];
			switch (currentStep)
			{
				case 'N':
				{
					var door = new Coordinate(previousCoordinate.X, previousCoordinate.Y - 1, '-');
					var room = new Coordinate(previousCoordinate.X, previousCoordinate.Y - 2, '.');

					_coordinates.Add(door);
					_coordinates.Add(room);

					Step(index + 1, room);
				}
				break;
				case 'E':
				{
					var door = new Coordinate(previousCoordinate.X + 1, previousCoordinate.Y, '|');
					var room = new Coordinate(previousCoordinate.X + 2, previousCoordinate.Y, '.');

					_coordinates.Add(door);
					_coordinates.Add(room);

					Step(index + 1, room);
				}
				break;
				case 'S':
				{
					var door = new Coordinate(previousCoordinate.X, previousCoordinate.Y + 1, '-');
					var room = new Coordinate(previousCoordinate.X, previousCoordinate.Y + 2, '.');

					_coordinates.Add(door);
					_coordinates.Add(room);

					Step(index + 1, room);
				}
				break;
				case 'W':
				{
					var door = new Coordinate(previousCoordinate.X - 1, previousCoordinate.Y, '|');
					var room = new Coordinate(previousCoordinate.X - 2, previousCoordinate.Y, '.');

					_coordinates.Add(door);
					_coordinates.Add(room);

					Step(index + 1, room);
				}
				break;
				case '(':
				{
					Step(index + 1, previousCoordinate);

					var splitIndexes = FindSplitsFrom(index);
					var isDetour = _steps[splitIndexes.Last() + 1] == ')';
					foreach (var splitIndex in splitIndexes)
					{
						Step(splitIndex + 1, previousCoordinate, isDetour);
					}
				}
				break;
				case '|':
				{
					if (detour)
						return;

					var closingIndex = FindClosingBraceFrom(index);
					Step(closingIndex + 1, previousCoordinate);
				}
				break;
				case ')':
				{
					if(detour)
						return;

					Step(index + 1, previousCoordinate);
				}
				break;
				case '$':
					return;
				default:
					throw new Exception("???: " + currentStep);
			}
		}

		private List<int> FindSplitsFrom(int i)
		{
			var splits = new List<int>();

			var opened = 0;
			var found = false;
			var currentIndex = i;
			while (opened >= 0)
			{
				currentIndex++;
				var currentChar = _steps[currentIndex];
				switch (currentChar)
				{
					case '(':
						opened++;
						break;
					case ')':
						opened--;
						break;
					case '|':
						if (opened == 0)
						{
							splits.Add(currentIndex);
						}
						break;
					default:
						break;
				}
			}

			return splits;
		}

		private int FindClosingBraceFrom(int i)
		{
			var opened = 1;
			var currentIndex = i;
			while (opened != 0)
			{
				currentIndex++;
				var currentChar = _steps[currentIndex];
				switch (currentChar)
				{
					case '(':
						opened++;
						break;
					case ')':
						opened--;
						break;
					default:
						break;
				}	
			}

			return currentIndex;
		}

		public class Coordinate : IEquatable<Coordinate>
		{
			public bool Equals(Coordinate other)
			{
				if (ReferenceEquals(null, other)) return false;
				if (ReferenceEquals(this, other)) return true;
				return X == other.X && Y == other.Y && C == other.C;
			}

			public override bool Equals(object obj)
			{
				if (ReferenceEquals(null, obj)) return false;
				if (ReferenceEquals(this, obj)) return true;
				if (obj.GetType() != this.GetType()) return false;
				return Equals((Coordinate) obj);
			}

			public override int GetHashCode()
			{
				unchecked
				{
					var hashCode = X;
					hashCode = (hashCode * 397) ^ Y;
					hashCode = (hashCode * 397) ^ C.GetHashCode();
					return hashCode;
				}
			}

			public int X;
			public int Y;

			public char C;

			public Coordinate(int x, int y, char c)
			{
				X = x;
				Y = y;
				C = c;
			}
		}
	}
}