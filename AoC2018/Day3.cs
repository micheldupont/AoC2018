using System;
using System.Collections.Generic;

namespace AoC2018
{
    public class Day3
    {
        public int Id { get; }
        public int X { get; }
        public int Y { get; }
        public int W { get; }
        public int H { get; }

        internal Day3(int id, int x, int y, int w, int h)
        {
            Id = id;
            X = x;
            Y = y;
            W = w;
            H = h;
        }

        //Format example: "#1 @ 126,902: 29x28"
        public static Day3 Parse(string input)
        {
            var parts = input.Split(new[] {'#', ' ', '@', ',', ':', 'x'}, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 5)
            {
                throw new Exception("Format Error");
            }

            var day3 = new Day3(int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2]), int.Parse(parts[3]),
                int.Parse(parts[4]));
            return day3;
        }

        public bool IsCollidingWith(Coord coord)
        {
            var collidesInX = coord.X >= X && coord.X <= X + W;
            var collidesInY = coord.Y >= Y && coord.Y <= Y + H;

            return collidesInX && collidesInY;
        }

        public List<Coord> GetAllCoveredCoords()
        {
            var list = new List<Coord>();

            for (int x = X; x < X + W; x++)
            {
                for (int y = Y; y < Y + H; y++)
                {
                    list.Add(new Coord(x, y));
                }
            }

            return list;
        }

        public List<Coord> CalculateCollisions(Day3 input)
        {
            var collisionList = new List<Coord>();

            var inputCoords = input.GetAllCoveredCoords();
            foreach (var coord in inputCoords)
            {
                if (IsCollidingWith(coord))
                {
                    collisionList.Add(coord);
                }
            }

            return collisionList;
        }
    }

    public class Day3CollisionMap
    {
        private readonly List<Day3> _input;

        public Day3CollisionMap(List<Day3> input)
        {
            _input = input;
        }

        public List<Coord> CalculateAllCollisions()
        {
            var coordDictionary = BuildCoordDictionary();

            var list = new List<Coord>();
            foreach (var c in coordDictionary)
            {
                if (c.Value > 1)
                {
                    list.Add(c.Key);
                }
            }

            return list;
        }

        private Dictionary<Coord, int> BuildCoordDictionary()
        {
            var coordDictionary = new Dictionary<Coord, int>();

            foreach (var day in _input)
            {
                var coords = day.GetAllCoveredCoords();
                foreach (var coord in coords)
                {
                    if (coordDictionary.TryGetValue(coord, out var coordCount))
                    {
                        coordDictionary[coord] = coordCount + 1;
                    }
                    else
                    {
                        coordDictionary.Add(coord, 1);
                    }
                }
            }

            return coordDictionary;
        }

        public List<Day3> GetNonCollidingDay3S()
        {
            var list = new List<Day3>();

            var coords = BuildCoordDictionary();

            var coordWithNoCollisions = new HashSet<Coord>();
            foreach (var c in coords)
            {
                if (c.Value == 1)
                {
                    coordWithNoCollisions.Add(c.Key);
                }
            }

            foreach (var day3 in _input)
            {
                var day3Coords = day3.GetAllCoveredCoords();

                if (day3Coords.TrueForAll(c => coordWithNoCollisions.Contains(c)))
                {
                    list.Add(day3);
                }
            }

            return list;
        }
    }

    public class Coord
    {
        public int X { get; }
        public int Y { get; }

        public Coord(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object obj)
        {
            var coord = obj as Coord;
            return coord != null &&
                   X == coord.X &&
                   Y == coord.Y;
        }

        public override int GetHashCode()
        {
            var hashCode = 1861411795;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            return hashCode;
        }
    }
}