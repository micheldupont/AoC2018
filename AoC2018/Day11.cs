using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2018
{
	public class Day11
	{
		private readonly int _serialNumber;
		private readonly int _dimension = 300;
		private readonly Cell[] _grid;

		public Day11(int serialNumber)
		{
			_serialNumber = serialNumber;
			_grid = GenerateGrid();
		}

		public Cell BuildCell(int x, int y)
		{
			return new Cell(x, y, _serialNumber);
		}

		public Square FindBestSquare()
		{
			var squares = GenerateSquares();
			var best = squares.OrderByDescending(s => s.TotalPower).First();
			return best;
		}

		public Square FindBestSquareOfAnySize()
		{
			Square bestSquareOverall = null;

			var isNotGettingAnyBetterCount = 0;
			for (int currentSize = 1; currentSize <= _dimension; currentSize++)
			{
				var squares = GenerateSquares(currentSize);
				var bestForCurrentSize = squares.OrderByDescending(s => s.TotalPower).First();

				if (bestSquareOverall == null || bestForCurrentSize.TotalPower > bestSquareOverall.TotalPower)
				{
					bestSquareOverall = bestForCurrentSize;
					isNotGettingAnyBetterCount = 0;
				}
				else
				{
					isNotGettingAnyBetterCount++;
				}

				if (isNotGettingAnyBetterCount == 10)
				{
					return bestSquareOverall;
				}
			}

			return bestSquareOverall;
		}

		public List<Square> GenerateSquares(int size = 3)
		{
			if (size % 2 == 0)
			{
				//even sized squares
				var list = new List<Square>();

				var padding = size;

				for (int y = 0; y <= _dimension - padding; y++)
				{
					for (int x = 0; x <= _dimension - padding; x++)
					{
						var square = BuildEvenSizeSquareAroundCell(x, y, size);
						list.Add(square);
					}
				}

				return list;
			}
			else
			{
				//odd sized squares
				var list = new List<Square>();

				var padding = (size -1) / 2;

				for (int y = padding; y < _dimension - padding; y++)
				{
					for (int x = padding; x < _dimension - padding; x++)
					{
						var square = BuildOddSizeSquareAroundCell(x, y, size);
						list.Add(square);
					}
				}

				return list;
			}
		}

		private Square BuildEvenSizeSquareAroundCell(int x, int y, int size)
		{
			var topLeft = _grid[(y * _dimension) + x];

			var totalPower = 0;
			for (int currentY = y; currentY < y + size; currentY++)
			{
				for (int currentX = x; currentX < x + size; currentX++)
				{
					var currentCell = _grid[(currentY * _dimension) + currentX];
					totalPower += currentCell.PowerLevel;
				}
			}

			return new Square(topLeft, size, totalPower);
		}

		private Square BuildOddSizeSquareAroundCell(int x, int y, int size)
		{
			var padding = (size - 1) / 2;

			var topLeft = _grid[((y - padding) * _dimension) + (x - padding)];

			var totalPower = 0;
			for (int currentY = y - padding; currentY <= y + padding; currentY++)
			{
				for (int currentX = x - padding; currentX <= x + padding; currentX++)
				{
					var currentCell = _grid[(currentY * _dimension) + currentX];
					totalPower += currentCell.PowerLevel;
				}
			}

			return new Square(topLeft, size, totalPower);
		}

		private Cell[] GenerateGrid()
		{
			var cells = new Cell[_dimension * _dimension];
			for (int y = 0; y < _dimension; y++)
			{
				for (int x = 0; x < _dimension; x++)
				{
					var c = new Cell(x+1, y+1, _serialNumber);

					cells[(y * _dimension) + x] = c;
				}
			}

			return cells;
		}

		public class Cell
		{
			public int X { get; }
			public int Y { get; }
			public readonly int PowerLevel;

			public Cell(int x, int y, int serialNumber)
			{
				X = x;
				Y = y;

				var rackId = X + 10;
				PowerLevel = rackId * Y;
				PowerLevel = PowerLevel + serialNumber;
				PowerLevel = PowerLevel * rackId;
				PowerLevel = GetThirdDigitOfNumber(PowerLevel);
				PowerLevel = PowerLevel - 5;
			}

			private int GetThirdDigitOfNumber(int number)
			{
				if (number < 100)
				{
					throw new Exception("unexpected value: " + number);
				}

				var downToThreeDigit = number % 1000;
				var third = downToThreeDigit / 100;

				return third;
			}
		}

		public class Square
		{
			public int Size { get; }
			public int TotalPower { get;}
			public Cell Corner { get; }

			public Square(Cell corner, int size, int totalPower = 0)
			{
				Size = size;
				Corner = corner;
				TotalPower = totalPower;
			}
		}
	}
}