using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace AoC2018
{
	public class Day6
	{
		public class Coordinate : IEquatable<Coordinate>
		{
			public bool Equals(Coordinate other)
			{
				if (ReferenceEquals(null, other)) return false;
				if (ReferenceEquals(this, other)) return true;
				return Y == other.Y && X == other.X;
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
					return (Y * 397) ^ X;
				}
			}

			public int Y { get; set; }
			public int X { get; set; }

			public Coordinate(int x, int y)
			{
				X = x;
				Y = y;
			}
		}

		public class Box : IEquatable<Box>
		{
			public bool Equals(Box other)
			{
				if (ReferenceEquals(null, other)) return false;
				if (ReferenceEquals(this, other)) return true;
				return Equals(TopLeft, other.TopLeft) && Equals(BottomRight, other.BottomRight);
			}

			public override bool Equals(object obj)
			{
				if (ReferenceEquals(null, obj)) return false;
				if (ReferenceEquals(this, obj)) return true;
				if (obj.GetType() != this.GetType()) return false;
				return Equals((Box) obj);
			}

			public override int GetHashCode()
			{
				unchecked
				{
					return ((TopLeft != null ? TopLeft.GetHashCode() : 0) * 397) ^ (BottomRight != null ? BottomRight.GetHashCode() : 0);
				}
			}

			public Coordinate TopLeft { get; set; }
			public Coordinate BottomRight { get; set; }
		}

		public static int GetDistance(Coordinate coordinateA, Coordinate coordinateB)
		{
			var xDist = Math.Abs(coordinateA.X - coordinateB.X);
			var yDist = Math.Abs(coordinateA.Y - coordinateB.Y);

			return xDist + yDist;
		}

		public static Box GetMinBoxCoordinates(List<Coordinate> input)
		{
			var topLeftCoordinate = new Coordinate(input[0].X, input[0].Y);
			var bottomRightCoordinate = new Coordinate(input[0].X, input[0].Y);
			
			foreach (var coordinate in input)
			{
				if (coordinate.X < topLeftCoordinate.X)
				{
					topLeftCoordinate.X = coordinate.X;
				}

				if (coordinate.X > bottomRightCoordinate.X)
				{
					bottomRightCoordinate.X = coordinate.X;
				}

				if (coordinate.Y < topLeftCoordinate.Y)
				{
					topLeftCoordinate.Y = coordinate.Y;
				}

				if (coordinate.Y > bottomRightCoordinate.Y)
				{
					bottomRightCoordinate.Y = coordinate.Y;
				}
			}

			return new Box{BottomRight = bottomRightCoordinate, TopLeft = topLeftCoordinate};
		}

		public static List<Area> EvaluateCoordinates(List<Coordinate> input)
		{
			var list = new List<Area>();
			for (var baseIndex = 0; baseIndex < input.Count; baseIndex++)
			{
				var coordinate = input[baseIndex];
				var currentArea = new Area
				{
					Id = baseIndex + 1,
					Center = new Coordinate(coordinate.X, coordinate.Y)
				};

				for (int i = 0; i < input.Count; i++)
				{
					if(i == baseIndex)
						continue;

					var otherCoordinate = input[i];
					var bound = false;

					if (otherCoordinate.X < coordinate.X) //west bound maybe?
					{
						var coordinateToTest = new Coordinate(otherCoordinate.X, coordinate.Y);
						var otherToTestDistance = GetDistance(otherCoordinate, coordinateToTest);
						var currentToTestDistance = GetDistance(coordinate, coordinateToTest);

						if (otherToTestDistance <= currentToTestDistance)
						{
							currentArea.IsWestBounded = true;
							bound = true;
						}
					}

					if (otherCoordinate.X > coordinate.X) //east bound maybe?
					{
						var coordinateToTest = new Coordinate(otherCoordinate.X, coordinate.Y);
						var otherToTestDistance = GetDistance(otherCoordinate, coordinateToTest);
						var currentToTestDistance = GetDistance(coordinate, coordinateToTest);

						if (otherToTestDistance <= currentToTestDistance)
						{
							currentArea.IsEastBounded = true;
							bound = true;
						}
					}

					if (otherCoordinate.Y < coordinate.Y) //north bound maybe?
					{
						var coordinateToTest = new Coordinate(coordinate.X, otherCoordinate.Y);
						var otherToTestDistance = GetDistance(otherCoordinate, coordinateToTest);
						var currentToTestDistance = GetDistance(coordinate, coordinateToTest);

						if (otherToTestDistance <= currentToTestDistance)
						{
							currentArea.IsNorthBounded = true;
							bound = true;
						}
					}

					if (otherCoordinate.Y > coordinate.Y) //east bound maybe?
					{
						var coordinateToTest = new Coordinate(coordinate.X, otherCoordinate.Y);
						var otherToTestDistance = GetDistance(otherCoordinate, coordinateToTest);
						var currentToTestDistance = GetDistance(coordinate, coordinateToTest);

						if (otherToTestDistance <= currentToTestDistance)
						{
							currentArea.IsSouthBounded = true;
							bound = true;
						}
					}

					if (bound)
					{
						currentArea.BoundedBy.Add(otherCoordinate);
					}
				}

				list.Add(currentArea);
			}

			return list;
		}

		public static int GetDimension(Area area)
		{
			var dimension = 0;
			if (area.IsFinite)
			{
				var box = GetMinBoxCoordinates(area.BoundedBy);

				for (int i = box.TopLeft.X; i < box.BottomRight.X; i++)
				{
					for (int j = box.TopLeft.Y; j < box.BottomRight.Y; j++)
					{
						var centerDistance = GetDistance(area.Center, new Coordinate(i, j));
						var othersMinDistance = int.MaxValue;

						foreach (var other in area.BoundedBy)
						{
							var otherDistance = GetDistance(other, new Coordinate(i, j));
							if (otherDistance < othersMinDistance)
							{
								othersMinDistance = otherDistance;
							}
						}

						if (centerDistance < othersMinDistance)
						{
							dimension += 1;
						}

					}
				}
			}

			return dimension;
		}

		public static int GetMaxDimension(List<Coordinate> input)
		{
		    var areas = EvaluateCoordinates(input);

		    var maxDimension = 0;

		    foreach (var area in areas.Where(a => a.IsFinite))
		    {
		        var dimension = GetDimension(area);

		        if (dimension > maxDimension)
		        {
		            maxDimension = dimension;
		        }

		    }

            return maxDimension;
		}

	    public static int GetSafeDimension(List<Coordinate> input, int safeDistance)
	    {
	        var box = GetMinBoxCoordinates(input);
	        var safeCount = 0;

	        for (int i = box.TopLeft.X; i < box.BottomRight.X; i++)
	        {
	            for (int j = box.TopLeft.Y; j < box.BottomRight.Y; j++)
	            {
	                var totalDistance = 0;
	                foreach (var coordinate in input)
	                {
	                    var dist = GetDistance(coordinate, new Coordinate(i, j));
	                    totalDistance += dist;
	                }

	                if (totalDistance < safeDistance)
	                {
	                    safeCount++;
	                }
	            }
	        }

	        return safeCount;
	    }
	}

	public class Area
	{
		public int Id { get; set; }
		public Day6.Coordinate Center { get; set; }
		public bool IsNorthBounded { get; set; }
		public bool IsSouthBounded { get; set; }
		public bool IsWestBounded { get; set; }
		public bool IsEastBounded { get; set; }

		public bool IsFinite => IsNorthBounded && IsSouthBounded && IsWestBounded && IsEastBounded;

		public List<Day6.Coordinate> BoundedBy { get; set; }

		public Area()
		{
			BoundedBy = new List<Day6.Coordinate>();
		}
	}
}