using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit.Abstractions;

namespace AoC2018
{
    public class Day10
    {
        public List<Star> Stars { get; }

        public Day10(string input)
        {
            Stars = Parse(input);
        }

        private static List<Star> Parse(string input)
        {
            var lines = input.Split(new[] {'\r', '\n'}, StringSplitOptions.RemoveEmptyEntries);

            var stars = new List<Star>();
            foreach (var line in lines)
            {
                var parts = line.Split(new[] {'>'}, StringSplitOptions.RemoveEmptyEntries);
                var positionParts = parts[0].Split(new[] { '=', '<', ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
                var velocityParts = parts[1].Split(new[] { '=', '<', ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);

                stars.Add(new Star
                {
                    X = int.Parse(positionParts[1]),
                    Y = int.Parse(positionParts[2]),
                    VelocityX = int.Parse(velocityParts[1]),
                    VelocityY = int.Parse(velocityParts[2]),
                });
            }

            return stars;
        }

        public void MoveAll(int seconds = 1)
        {
            for (int i = 0; i < seconds; i++)
            {
                foreach (var star in Stars)
                {
                    star.Move();
                }
            }
        }

        public double GetTotalDistanceToOrigin()
        {
            var distance = 0d;
            foreach (var star in Stars)
            {
                var xsq = (star.X * (double)star.X);
                var ysq = (star.Y * (double)star.Y);
                var s = xsq + ysq;
                var sqrt = Math.Sqrt(s);
                distance += sqrt;
            }

            return distance;
        }

        public int FindNumberOfSecondBeforeMaximumConvergence()
        {
            var currentDistanceToOrigin = double.MaxValue;
            var second = 0;

            while(true)
            {
                MoveAll();
                var newDistance = GetTotalDistanceToOrigin();

                if (newDistance < currentDistanceToOrigin)
                {
                    second++;
                    currentDistanceToOrigin = newDistance;
                }
                else
                {
                    return second;
                }
            }
        }

        public void RenderToConsoleOutput(int second, ITestOutputHelper output)
        {
            MoveAll(second);

            var minX = Stars.Min(s => s.X);
            var minY = Stars.Min(s => s.Y);

            foreach (var star in Stars)
            {
                star.X -= minX - 2;
                star.Y -= minY - 2;
            }

            var maxX = Stars.Max(s => s.X);
            var maxY = Stars.Max(s => s.Y);

            for (int y = 0; y < maxY +2; y++)
            {
                var line = new StringBuilder();
                for (int x = 0; x < maxX + 10; x++)
                {
                    if (Stars.Exists(s => s.X == x && s.Y == y))
                    {
                        line.Append('#');
                    }
                    else
                    {
                        line.Append(' ');
                    }
                }

                output.WriteLine(line.ToString());
            }

        }
    }

    public class Star
    {
        protected bool Equals(Star other)
        {
            return X == other.X && Y == other.Y && VelocityX == other.VelocityX && VelocityY == other.VelocityY;
        }

        public void Move()
        {
            X += VelocityX;
            Y += VelocityY;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Star) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = X;
                hashCode = (hashCode * 397) ^ Y;
                hashCode = (hashCode * 397) ^ VelocityX;
                hashCode = (hashCode * 397) ^ VelocityY;
                return hashCode;
            }
        }

        public int X { get; set; }
        public int Y { get; set; }

        public int VelocityX { get; set; }
        public int VelocityY { get; set; }
    }
}