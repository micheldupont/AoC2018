using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace AoC2018
{
    public class Day15
    {
        private readonly string _input;
        public List<Elf> Elves { get; set; }
        public List<Goblin> Goblins { get; set; }

        public char[,] Map { get; set; }

        private int _elfAP;

        public Day15(string input)
        {
            _input = input;

            _elfAP = 3;
            ParseInput();
        }

        private void ParseInput()
        {
            Elves = new List<Elf>();
            Goblins = new List<Goblin>();

            var lines = _input.Split(new[] {'\r', '\n'}, StringSplitOptions.RemoveEmptyEntries);

            var firstLine = lines[0];

            Map = new char[firstLine.Length, lines.Length];

            for (int y = 0; y < lines.Length; y++)
            {
                var currentLine = lines[y];
                var parts = currentLine.ToCharArray();

                for (int x = 0; x < parts.Length; x++)
                {
                    var currentChar = parts[x];

                    Map[x, y] = currentChar;
                    switch (currentChar)
                    {
                        case 'E':
                            Elves.Add(new Elf(x, y, _elfAP));
                            break;
                        case 'G':
                            Goblins.Add(new Goblin(x, y));
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public Goblin FindClosestEnemyTo(Elf source)
        {
            var sourceX = source.X;
            var sourceY = source.Y;

            var target = SearchFor('G', sourceX, sourceY);

            return Goblins.Find(g => g.X == target.X && g.Y == target.Y);
        }

        public Elf FindClosestEnemyTo(Goblin source)
        {
            var sourceX = source.X;
            var sourceY = source.Y;

            var target = SearchFor('E', sourceX, sourceY);

            return Elves.Find(e => e.X == target.X && e.Y == target.Y);
        }

        public void MoveToClosestEnemy(Elf source)
        {
            var sourceX = source.X;
            var sourceY = source.Y;

            var target = SearchFor('G', sourceX, sourceY);

            if (target == null)
                return;

            var previous = target;
            var current = target.From;

            while (current.From != null)
            {
                previous = current;
                current = current.From;
            }

            var newX = previous.X;
            var newY = previous.Y;

            Map[sourceX, sourceY] = '.';
            Map[newX, newY] = 'E';

            source.X = newX;
            source.Y = newY;
        }

        public void MoveToClosestEnemy(Goblin source)
        {
            var sourceX = source.X;
            var sourceY = source.Y;

            var target = SearchFor('E', sourceX, sourceY);

            if (target == null)
                return;

            var previous = target;
            var current = target.From;

            while (current.From != null)
            {
                previous = current;
                current = current.From;
            }

            var newX = previous.X;
            var newY = previous.Y;

            Map[sourceX, sourceY] = '.';
            Map[newX, newY] = 'G';

            source.X = newX;
            source.Y = newY;
        }

        public bool DoRound(bool abortIfAnElfDies = false)
        {
            var allFighters = 
                Goblins.Cast<Fighter>()
                .Concat(Elves.Cast<Fighter>())
                    .Where(f => !f.IsDead)
                    .OrderBy(f => f.Y)
                    .ThenBy(f => f.X).ToList();


            foreach (var fighter in allFighters)
            {
                if (Goblins.All(g => g.IsDead) || Elves.All(e => e.IsDead))
                    return true;

                if (abortIfAnElfDies && Elves.Any(e => e.IsDead))
                    return true;

                if (fighter.IsDead)
                {
                    continue;
                }

                if (fighter is Elf)
                {
                    var asElf = (Elf) fighter;

                    if (!TryToFight(asElf))
                    {
                        MoveToClosestEnemy(asElf);
                        TryToFight(asElf);
                    }
                }
                else
                {
                    var asGoblin = (Goblin)fighter;

                    if (!TryToFight(asGoblin))
                    {
                        MoveToClosestEnemy(asGoblin);
                        TryToFight(asGoblin);
                    }
                }
            }

            return false;
        }

        public int SimToVictory(bool abortIfAnElfDies = false)
        {
            var round = 0;
            while (true)
            {
                if (DoRound(abortIfAnElfDies))
                {
                    if (abortIfAnElfDies && Elves.Any(e => e.IsDead))
                        return -1;

                    if (Goblins.All(g => g.IsDead))
                    {
                        var hpLeft = Elves.Where(e => !e.IsDead).Sum(e => e.HP);
                        return round * hpLeft;
                    }

                    if (Elves.All(e => e.IsDead))
                    {
                        var hpLeft = Goblins.Where(e => !e.IsDead).Sum(e => e.HP);
                        return round * hpLeft;
                    }
                }

                round++;
            }
        }

        public int SimFlawlessVictory()
        {
            _elfAP = 3;
            var result = -1;

            while (result == -1)
            {
                _elfAP++;
                ParseInput();
                result = SimToVictory(true);
            }

            return result;
        }

        private bool TryToFight(Elf elf)
        {
            var root = new Coordinate(elf.X, elf.Y);
            var neighbors = GetNeighbors(root);

            var goblins = new List<Goblin>();

            foreach (var neighbor in neighbors)
            {
                var goblin = Goblins.Find(g => !g.IsDead && g.X == neighbor.X && g.Y == neighbor.Y);
                if (goblin != null)
                {
                    goblins.Add(goblin);
                }
            }

            var toAttack = goblins.OrderBy(g => g.HP).ThenBy(g => g.Y).ThenBy(g => g.X).FirstOrDefault();

            if (toAttack != null)
            {
                toAttack.HP -= elf.AP;

                if (toAttack.IsDead)
                {
                    Map[toAttack.X, toAttack.Y] = '.';
                }

                return true;
            }

            return false;
        }

        private bool TryToFight(Goblin goblin)
        {
            var root = new Coordinate(goblin.X, goblin.Y);
            var neighbors = GetNeighbors(root);

            var elves = new List<Elf>();

            foreach (var neighbor in neighbors)
            {
                var elf = Elves.Find(g => !g.IsDead && g.X == neighbor.X && g.Y == neighbor.Y);
                if (elf != null)
                {
                    elves.Add(elf);
                }
            }

            var toAttack = elves.OrderBy(g => g.HP).ThenBy(g => g.Y).ThenBy(g => g.X).FirstOrDefault();

            if (toAttack != null)
            {
                toAttack.HP -= goblin.AP;

                if (toAttack.IsDead)
                {
                    Map[toAttack.X, toAttack.Y] = '.';
                }

                return true;
            }

            return false;
        }

        private Coordinate SearchFor(char target, int x, int y)
        {
            var root = new Coordinate(x, y);
            var frontier = new List<Coordinate> {root};
            var seen = new List<Coordinate>{root};

            while (frontier.Count > 0)
            {
                var c = frontier.Count;

                var possibleTarget = new List<Coordinate>();

                for (int i = 0; i < c; i++)
                {
                    var current = frontier.First();
                    frontier.Remove(current);

                    var neighbors = GetNeighbors(current);

                    foreach (var neighbor in neighbors)
                    {
                        if (!seen.Contains(neighbor))
                        {
                            if (target == 'G' && neighbor.IsGoblin())
                            {
                                possibleTarget.Add(neighbor);
                            }

                            if (target == 'E' && neighbor.IsElf())
                            {
                                possibleTarget.Add(neighbor);
                            }

                            if (neighbor.CanMoveTo())
                            {
                                frontier.Add(neighbor);
                                seen.Add(neighbor);
                            }
                        }
                    }
                }

                if(possibleTarget.Count > 0)
                    return possibleTarget.OrderBy(t => t.Y).ThenBy(t => t.X).First();
            }

            return null;
        }

        private List<Coordinate> GetNeighbors(Coordinate current)
        {
            var top = new Coordinate(current.X, current.Y - 1, current, Map[current.X, current.Y - 1]);
            var left = new Coordinate(current.X - 1, current.Y, current, Map[current.X - 1, current.Y]);
            var right = new Coordinate(current.X + 1, current.Y, current, Map[current.X + 1, current.Y]);
            var bottom = new Coordinate(current.X, current.Y + 1, current, Map[current.X, current.Y + 1]);

            return new List<Coordinate> {top, left, right, bottom};
        }

        public class Coordinate
        {
            public int X { get; set; }
            public int Y { get; set; }

            public Coordinate From { get; set; }

            public char C { get; set; }

            public Coordinate(int x, int y, Coordinate from = null, char c = '_')
            {
                X = x;
                Y = y;
                From = from;
                C = c;
            }

            public bool CanMoveTo()
            {
                return C == '.';
            }

            public bool IsGoblin()
            {
                return C == 'G';
            }

            public bool IsElf()
            {
                return C == 'E';
            }

            public override bool Equals(object obj)
            {
                var coordinate = obj as Coordinate;
                return coordinate != null &&
                       X == coordinate.X &&
                       Y == coordinate.Y &&
                       C == coordinate.C;
            }

            public override int GetHashCode()
            {
                var hashCode = 41800274;
                hashCode = hashCode * -1521134295 + X.GetHashCode();
                hashCode = hashCode * -1521134295 + Y.GetHashCode();
                hashCode = hashCode * -1521134295 + C.GetHashCode();
                return hashCode;
            }
        }

        public class Goblin : Fighter
        {
            public Goblin(int x, int y)
                : base(x, y, 3, 200)
            {

            }
        }

        public class Elf : Fighter
        {
            public Elf(int x, int y, int ap)
                :base(x, y, ap, 200)
            {
                
            }

            
        }

        public class Fighter
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int AP { get; set; }
            public int HP { get; set; }

            public bool IsDead => HP <= 0;

            public Fighter(int x, int y, int ap, int hp)
            {
                X = x;
                Y = y;
                AP = ap;
                HP = hp;
            }
        }
    }
}