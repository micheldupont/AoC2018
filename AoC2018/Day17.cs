﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2018
{
    public class Day17
    {
        public char[,] Map;
        public int Minx { get; set; }

        public Day17(string input)
        {
            var lines = input.Split(new[] {'\r', '\n'}, StringSplitOptions.RemoveEmptyEntries);
            var wallCoordinates = new List<Coordinate>();

            foreach (var line in lines)
            {
                var parts = line.Split(new[] {',', ' '}, StringSplitOptions.RemoveEmptyEntries);

                var firstDimParts =  parts[0].Split(new[] {'='}, StringSplitOptions.RemoveEmptyEntries);
                var secondDimParts = parts[1].Split(new[] { '=', '.' }, StringSplitOptions.RemoveEmptyEntries);

                //vert line
                if (firstDimParts[0] == "x")
                {
                    var x = int.Parse(firstDimParts[1]);
                    
                    var begin = int.Parse(secondDimParts[1]);
                    var end = int.Parse(secondDimParts[2]);

                    var ys = Enumerable.Range(begin, end - begin + 1);

                    foreach (var y in ys)
                    {
                        wallCoordinates.Add(new Coordinate(x, y));
                    }
                }
                else //y ... hor line
                {
                    var y = int.Parse(firstDimParts[1]);

                    var begin = int.Parse(secondDimParts[1]);
                    var end = int.Parse(secondDimParts[2]);

                    var xs = Enumerable.Range(begin, end - begin + 1);

                    foreach (var x in xs)
                    {
                        wallCoordinates.Add(new Coordinate(x, y));
                    }
                }
            }

            var maxX = wallCoordinates.Max(c => c.X);
            var minX = wallCoordinates.Min(c => c.X);
            var maxY = wallCoordinates.Max(c => c.Y);

            Map = new char[maxX - minX + 1, maxY + 1];
            for (int y = 0; y <= maxY; y++)
            {
                for (int x = 0; x <= maxX - minX; x++)
                {
                    /*if (wallCoordinates.Find(c => c.X == (x + minX) && c.Y == y) != null)
                    {
                        Map[x, y] = '#';
                    }
                    else
                    {*/
                        Map[x, y] = '.';
                   // }
                }
            }

            Map[500 - minX, 0] = '+';

	        foreach (var wallCoordinate in wallCoordinates)
	        {
		        Map[wallCoordinate.X - minX, wallCoordinate.Y] = '#';
	        }

            Minx = minX;
        }

        private int XOffset(int num)
        {
            return num - Minx;
        }


        public Drop DropWater()
        {
            if (Map[XOffset(500), 1] != '.')
                return null;

            var newDrop = new Drop(XOffset(500), 0);
            newDrop.RunDown(ref Map);

            return newDrop;
        }

        public void DropManyWater(int count)
        {
            for (int i = 0; i < count; i++)
            {
                DropWater();
            }

	        Map[XOffset(500), 0] = '+';
		}

	    public int Flood()
	    {
		    do{
			    DropWater();
		    } while (Map[XOffset(500), 1] == '.');

		    Map[XOffset(500), 0] = '+';

		    var flooded = 0;
		    for (int y = 0; y < Map.GetLength(1); y++)
		    {
			    for (int x = 0; x < Map.GetLength(0); x++)
			    {

				    if (Map[x, y] == 'w' || Map[x, y] == '|')
				    {
					    flooded++;
				    }

			    }
		    }


		    return flooded;
	    }

		public class Coordinate
        {
            public int X { get; set; }
            public int Y { get; set; }

            public Coordinate(int x, int y)
            {
                X = x;
                Y = y;
            }
        }

        public class Drop
        {
            public int X { get; set; }
            public int Y { get; set; }

            public Drop(int x, int y)
            {
                X = x;
                Y = y;
            }

            public void RunDown(ref char[,] map)
            {
                map[X, Y] = '.';
                map[X, Y + 1] = 'w';
                Y++;

	            if (Y + 1 >= map.GetLength(1))
	            {
		            map[X, Y] = '|';
		            return;
				}
				
                var down = map[X, Y + 1];
                var left = map[X - 1, Y];
                var right = map[X + 1, Y];

                if (down == '.')
                {
                    RunDown(ref map);
                }
				else if (down == '|')
                {
	                map[X, Y] = '|';
                }
                else if (left == '.')
                {
                    RunLeft(ref map);
                }
                else if(right == '.')
                {
                    RunRight(ref map);
                }
                else if( left == '|' || right == '|')
                {
	                map[X, Y] = '|';
                }
            }

            public void RunLeft(ref char[,] map)
            {
                map[X, Y] = '.';
                map[X - 1, Y] = 'w';
                X--;

                if (X - 1 < 0)
                {
	                map[X, Y] = '|';
					return;
                }

                var down = map[X, Y + 1];
                var left = map[X - 1, Y];

                if (down == '.')
                {
                    RunDown(ref map);
                }
				else if (down == '|')
                {
	                map[X, Y] = '|';
                }
                else if (left == '.')
                {
                    RunLeft(ref map);
                }
                else if (left == '|')
                {
					map[X, Y] = '|';
				}
			}

            public void RunRight(ref char[,] map)
            {
                map[X, Y] = '.';
                map[X + 1, Y] = 'w';
                X++;

                if (X + 1 >= map.GetLength(0))
                {
	                map[X, Y] = '|';
					return;
                }

                var down = map[X, Y + 1];
                var right = map[X + 1, Y];

                if (down == '.')
                {
                    RunDown(ref map);
                }
                else if (down == '|')
                {
	                map[X, Y] = '|';
                }
				else if (right == '.')
                {
                    RunRight(ref map);
                }
                else if (right == '|')
                {
	                map[X, Y] = '|';
                }
			}
        }
    }
}