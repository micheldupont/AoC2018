using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;

namespace AoC2018
{
	public class Day13
	{
		public char[,] Map { get; private set; }
		public List<Cart> Carts { get; }

		private readonly char[] _cartChars = { '<', '^', '>', 'v' };

		public Day13(string testInput)
		{
			Carts = new List<Cart>();
			Parse(testInput);
		}

		private void Parse(string testInput)
		{
			var lines = testInput.Split(new[] {'\r', '\n'}, StringSplitOptions.RemoveEmptyEntries);
			var xSize = lines[0].Length;
			var ySize = lines.Length;

			Map = new char[xSize,ySize];

			for (var y = 0; y < ySize; y++)
			{
				var currentLine = lines[y].ToCharArray();
				for (var x = 0; x < xSize; x++)
				{
					var currentChar = currentLine[x];

					if (_cartChars.Contains(currentChar))
					{
						if (currentChar == '<' || currentChar == '>')
						{
							Map[x, y] = '-';
							Carts.Add(new Cart(x, y, '-', currentChar == '<' ? Headings.Left : Headings.Right, Map));
						}
						else
						{
							Map[x, y] = '|';
							Carts.Add(new Cart(x, y, '|', currentChar == '^' ? Headings.Up : Headings.Down, Map));
						}
					}
					else
					{
						Map[x, y] = currentChar;
					}
				}
			}
		}

		public CrashInfo SimUntilCrash()
		{
			var sortedCarts = Carts.OrderBy(c => c.Y).ThenBy(c => c.X).ToList();

			while (true)
			{
				foreach (var cart in sortedCarts)
				{
					cart.Move();
					if (sortedCarts.Find(c => !c.Equals(cart) && c.X == cart.X && c.Y == cart.Y) != null)
					{
						return new CrashInfo(cart.X, cart.Y);
					}
				}
			}
		}

		public Cart SimToLastCart()
		{
			var sortedCarts = Carts.OrderBy(c => c.Y).ThenBy(c => c.X).ToList();

			while (true)
			{
				var crashedCart = new List<Cart>();

				foreach (var cart in sortedCarts)
				{
					cart.Move();
					var cart1 = cart;
					var cartCrashed = sortedCarts.Find(c => !c.Equals(cart1) && c.X == cart1.X && c.Y == cart1.Y);
					if (cartCrashed != null)
					{
						//crashed!
						crashedCart.Add(cart);
						crashedCart.Add(cartCrashed);
					}
				}

				foreach (var cart in crashedCart.Distinct())
				{
					sortedCarts.Remove(cart);
				}

				if (sortedCarts.Count == 1)
				{
					return sortedCarts.First();
				}

				if (sortedCarts.Count == 0)
				{
					throw new Exception("All dead!");
				}
			}
		}

		public enum Headings
		{
			Up,
			Down,
			Left,
			Right
		}

		public enum Decisions
		{
			GoLeft,
			GoStraight,
			GoRight
		}

		public class Cart
		{
			private readonly char[,] _map;
			public int X { get; set; }
			public int Y { get; set; }
			public char Path { get; set; }
			public Headings Heading { get; set; }

			private readonly Decisions[] _decisions = { Decisions.GoLeft, Decisions.GoStraight, Decisions.GoRight };
			private int _nextDecisionIndex;
		
			public Cart(int x, int y, char path, Headings heading, char[,] map)
			{
				_map = map;
				X = x;
				Y = y;
				Path = path;
				Heading = heading;
				_nextDecisionIndex = 0;
			}

			public void Move()
			{
				UpdatePosition();
				UpdateHeading();
			}

			private void UpdatePosition()
			{
				switch (Path)
				{
					case '-':
					case '|':
					case '/':
					case '\\':
					case '+':
						switch (Heading)
						{
							case Headings.Left:
								X--;
								break;
							case Headings.Right:
								X++;
								break;
							case Headings.Up:
								Y--;
								break;
							case Headings.Down:
								Y++;
								break;
						}

						break;
					default:
						throw new Exception("Invalid path! : " + Path);
				}

				Path = _map[X, Y];
			}

			private void UpdateHeading()
			{
				switch (Path)
				{
					case '-':
						break;
					case '|':
						break;
					case '/':
						switch (Heading)
						{
							case Headings.Left:
								Heading = Headings.Down;
								break;
							case Headings.Up:
								Heading = Headings.Right;
								break;
							case Headings.Right:
								Heading = Headings.Up;
								break;
							case Headings.Down:
								Heading = Headings.Left;
								break;
						}

						break;
					case '\\':
						switch (Heading)
						{
							case Headings.Left:
								Heading = Headings.Up;
								break;
							case Headings.Up:
								Heading = Headings.Left;
								break;
							case Headings.Right:
								Heading = Headings.Down;
								break;
							case Headings.Down:
								Heading = Headings.Right;
								break;
						}

						break;
					case '+':
						HandleIntersection();
						break;
					default:
						throw new Exception("Invalid path! : " + Path);
				}
			}

			private void HandleIntersection()
			{
				switch (_decisions[_nextDecisionIndex])
				{
					case Decisions.GoLeft:
						switch (Heading)
						{
							case Headings.Left:
								Heading = Headings.Down;
								break;
							case Headings.Up:
								Heading = Headings.Left;
								break;
							case Headings.Right:
								Heading = Headings.Up;
								break;
							case Headings.Down:
								Heading = Headings.Right;
								break;
						}

						break;
					case Decisions.GoStraight:
						break;
					case Decisions.GoRight:
						switch (Heading)
						{
							case Headings.Left:
								Heading = Headings.Up;
								break;
							case Headings.Up:
								Heading = Headings.Right;
								break;
							case Headings.Right:
								Heading = Headings.Down;
								break;
							case Headings.Down:
								Heading = Headings.Left;
								break;
						}

						break;
				}

				_nextDecisionIndex++;
				_nextDecisionIndex = _nextDecisionIndex % _decisions.Length;
			}
		}

		public class CrashInfo
		{
			public int X { get; }
			public int Y { get; }

			public CrashInfo(int x, int y)
			{
				this.X = x;
				this.Y = y;
			}
		}
	}
}