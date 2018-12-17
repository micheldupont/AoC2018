using Xunit;

namespace AoC2018.tests
{
    public class Day15Tests
    {
        [Fact]
        public void CanParse()
        {
            var input = @"#########
#G..G..G#
#.......#
#.......#
#G..E..G#
#.......#
#.......#
#G..G..G#
#########
#########";

            var day = new Day15(input);

            Assert.Equal(1, day.Elves.Count);
            Assert.Equal(8, day.Goblins.Count);
            Assert.Equal(9, day.Map.GetLength(0));
            Assert.Equal(10, day.Map.GetLength(1));
        }

        [Fact]
        public void CanParseElves()
        {
            var input = @"#########
#G..G..G#
#.......#
#.......#
#G..E..G#
#.......#
#.......#
#G..G..G#
#########
#########";

            var day = new Day15(input);

            Assert.Equal(4, day.Elves[0].X);
            Assert.Equal(4, day.Elves[0].Y);
            Assert.Equal(3, day.Elves[0].AP);
            Assert.Equal(200, day.Elves[0].HP);
        }

        [Fact]
        public void CanParseGoblins()
        {
            var input = @"#########
#G..G..G#
#.......#
#.......#
#G..E..G#
#.......#
#.......#
#G..G..G#
#########
#########";

            var day = new Day15(input);

            Assert.Equal(1, day.Goblins[0].X);
            Assert.Equal(1, day.Goblins[0].Y);
            Assert.Equal(3, day.Goblins[0].AP);
            Assert.Equal(200, day.Goblins[0].HP);
        }

        [Fact]
        public void ElfCanFindClosestGoblinA()
        {
            var input = @"#########
#G..G..G#
#.......#
#.......#
#G..E..G#
#.......#
#.......#
#G..G..G#
#########
#########";

            var day = new Day15(input);

            var result = day.FindClosestEnemyTo(day.Elves[0]);

            Assert.Equal(day.Goblins[1], result);
        }

        [Fact]
        public void ElfCanFindClosestGoblinB()
        {
            var input = @"#########
#G.....G#
#.......#
#.......#
#G..E..G#
#.......#
#.......#
#G..G..G#
#########
#########";

            var day = new Day15(input);

            var result = day.FindClosestEnemyTo(day.Elves[0]);

            Assert.Equal(day.Goblins[2], result);
        }

        [Fact]
        public void ElfCanFindClosestGoblinC()
        {
            var input = @"#########
#G.....G#
#.......#
#.......#
#...E..G#
#.......#
#.......#
#G..G..G#
#########
#########";

            var day = new Day15(input);

            var result = day.FindClosestEnemyTo(day.Elves[0]);

            Assert.Equal(day.Goblins[2], result);
        }

        [Fact]
        public void ElfCanFindClosestGoblinD()
        {
            var input = @"#########
#G.....G#
#.......#
#.......#
#...E...#
#.......#
#.......#
#G..G..G#
#########
#########";

            var day = new Day15(input);

            var result = day.FindClosestEnemyTo(day.Elves[0]);

            Assert.Equal(day.Goblins[3], result);
        }

        [Fact]
        public void ElfCanFindClosestGoblinE()
        {
            var input = @"#########
#G.....G#
#.......#
#.......#
#...E...#
#.......#
#.......#
#G.....G#
#########
#########";

            var day = new Day15(input);

            var result = day.FindClosestEnemyTo(day.Elves[0]);

            Assert.Equal(day.Goblins[0], result);
        }

        [Fact]
        public void GoblinCanFindClosestElfA()
        {
            var input = @"#########
#G.....G#
#.......#
#.......#
#...E...#
#.......#
#.......#
#G..G..G#
#########
#########";

            var day = new Day15(input);

            var result = day.FindClosestEnemyTo(day.Goblins[0]);

            Assert.Equal(day.Elves[0], result);
        }

        [Fact]
        public void ElfCanDoBestMove()
        {
            var input = @"#########
#.G...G.#
#...G...#
#.......#
#.G.E..G#
#.......#
#.......#
#G..G..G#
#########
#########";

            var day = new Day15(input);

            day.MoveToClosestEnemy(day.Elves[0]);

            Assert.Equal(4, day.Elves[0].X);
            Assert.Equal(3, day.Elves[0].Y);
            Assert.Equal('E', day.Map[4, 3]);
            Assert.Equal('.', day.Map[4, 4]);
        }

        [Fact]
        public void CanDoStepsForAFullRound()
        {
            var input = @"#########
#G..G..G#
#.......#
#.......#
#G..E..G#
#.......#
#.......#
#G..G..G#
#########";

            var day = new Day15(input);

            day.DoRound();

            Assert.Equal(2, day.Goblins[0].X);
            Assert.Equal(2, day.Goblins[1].Y);
            Assert.Equal(6, day.Goblins[2].X);
            Assert.Equal(2, day.Goblins[3].X);
            Assert.Equal(3, day.Elves[0].Y);
            Assert.Equal(3, day.Goblins[4].Y);
            Assert.Equal(6, day.Goblins[5].Y);
            Assert.Equal(6, day.Goblins[6].Y);
            Assert.Equal(6, day.Goblins[7].Y);
        }

        [Fact]
        public void CanDoStepsForTwoRound()
        {
            var input = @"#########
#G..G..G#
#.......#
#.......#
#G..E..G#
#.......#
#.......#
#G..G..G#
#########";

            var day = new Day15(input);

            day.DoRound();
            day.DoRound();

            Assert.Equal(3, day.Goblins[0].X);
            Assert.Equal(4, day.Goblins[1].X);
            Assert.Equal(2, day.Goblins[1].Y);
            Assert.Equal(5, day.Goblins[2].X);
            Assert.Equal(3, day.Goblins[3].Y);
            Assert.Equal(4, day.Elves[0].X);
            Assert.Equal(3, day.Elves[0].Y);
            Assert.Equal(6, day.Goblins[4].X);
            Assert.Equal(5, day.Goblins[5].Y);
            Assert.Equal(5, day.Goblins[6].Y);
            Assert.Equal(5, day.Goblins[7].Y);
        }

        [Fact]
        public void CanDoStepsForThreeRound()
        {
            var input = @"#########
#G..G..G#
#.......#
#.......#
#G..E..G#
#.......#
#.......#
#G..G..G#
#########";

            var day = new Day15(input);

            day.DoRound();
            day.DoRound();
            day.DoRound();

            Assert.Equal(2, day.Goblins[0].Y);
            Assert.Equal(4, day.Goblins[1].X);
            Assert.Equal(2, day.Goblins[1].Y);
            Assert.Equal(2, day.Goblins[2].Y);
            Assert.Equal(3, day.Goblins[3].X);
            Assert.Equal(4, day.Elves[0].X);
            Assert.Equal(3, day.Elves[0].Y);
            Assert.Equal(5, day.Goblins[4].X);
            Assert.Equal(4, day.Goblins[5].Y);
            Assert.Equal(4, day.Goblins[6].Y);
            Assert.Equal(5, day.Goblins[7].Y);
        }

        [Fact]
        public void CanDoStepsForFiveRound()
        {
            var input = @"#########
#G..G..G#
#.......#
#.......#
#G..E..G#
#.......#
#.......#
#G..G..G#
#########";

            var day = new Day15(input);

            day.DoRound();
            day.DoRound();
            day.DoRound();
            day.DoRound();
            day.DoRound();

            Assert.Equal(2, day.Goblins[0].Y);
            Assert.Equal(4, day.Goblins[1].X);
            Assert.Equal(2, day.Goblins[1].Y);
            Assert.Equal(2, day.Goblins[2].Y);
            Assert.Equal(3, day.Goblins[3].X);
            Assert.Equal(4, day.Elves[0].X);
            Assert.Equal(3, day.Elves[0].Y);
            Assert.Equal(5, day.Goblins[4].X);
            Assert.Equal(4, day.Goblins[5].Y);
            Assert.Equal(4, day.Goblins[6].Y);
            Assert.Equal(5, day.Goblins[7].Y);
        }

        [Fact]
        public void CanSimToVictoryA()
        {
            var input = @"#######   
#.G...#
#...EG#
#.#.#G#
#..G#E#
#.....#
#######";

            var day = new Day15(input);

            var outcome = day.SimToVictory();

            Assert.Equal(27730, outcome);
        }

        [Fact]
        public void CanSimToVictoryB()
        {
            var input = @"#######
#G..#E#
#E#E.E#
#G.##.#
#...#E#
#...E.#
#######";

            var day = new Day15(input);

            var outcome = day.SimToVictory();

            Assert.Equal(36334, outcome);
        }

        [Fact]
        public void CanSimToVictoryC()
        {
            var input = @"#######
#E..EG#
#.#G.E#
#E.##E#
#G..#.#
#..E#.#
#######";

            var day = new Day15(input);

            var outcome = day.SimToVictory();

            Assert.Equal(39514, outcome);
        }

        [Fact]
        public void CanSimToVictoryD()
        {
            var input = @"#######
#E.G#.#
#.#G..#
#G.#.G#
#G..#.#
#...E.#
#######";

            var day = new Day15(input);

            var outcome = day.SimToVictory();

            Assert.Equal(27755, outcome);
        }

        [Fact]
        public void CanSimToVictoryE()
        {
            var input = @"#######
#.E...#
#.#..G#
#.###.#
#E#G#G#
#...#G#
#######";

            var day = new Day15(input);

            var outcome = day.SimToVictory();

            Assert.Equal(28944, outcome);
        }

        [Fact]
        public void CanSimToVictoryF()
        {
            var input = @"#########
#G......#
#.E.#...#
#..##..G#
#...##..#
#...#...#
#.G...G.#
#.....G.#
#########";

            var day = new Day15(input);

            var outcome = day.SimToVictory();

            Assert.Equal(18740, outcome);
        }

        [Fact]
        public void Q1()
        {
            var day = new Day15(Inputs.Day15);

            var outcome = day.SimToVictory();

            Assert.Equal(226688, outcome);
        }

        [Fact]
        public void CanSimAnElfFlawlessVictoryA()
        {
            var input = @"#######
#.G...#
#...EG#
#.#.#G#
#..G#E#
#.....#
#######";
            var day = new Day15(input);

            var outcome = day.SimFlawlessVictory();

            Assert.Equal(4988, outcome);
        }

        [Fact]
        public void CanSimAnElfFlawlessVictoryB()
        {
            var input = @"#######
#E..EG#
#.#G.E#
#E.##E#
#G..#.#
#..E#.#
#######";
            var day = new Day15(input);

            var outcome = day.SimFlawlessVictory();

            Assert.Equal(31284, outcome);
        }

        [Fact]
        public void CanSimAnElfFlawlessVictoryC()
        {
            var input = @"#######
#E.G#.#
#.#G..#
#G.#.G#
#G..#.#
#...E.#
#######";
            var day = new Day15(input);

            var outcome = day.SimFlawlessVictory();

            Assert.Equal(3478, outcome);
        }

        [Fact]
        public void CanSimAnElfFlawlessVictoryD()
        {
            var input = @"#######
#.E...#
#.#..G#
#.###.#
#E#G#G#
#...#G#
#######";
            var day = new Day15(input);

            var outcome = day.SimFlawlessVictory();

            Assert.Equal(6474, outcome);
        }

        [Fact]
        public void CanSimAnElfFlawlessVictoryE()
        {
            var input = @"#########
#G......#
#.E.#...#
#..##..G#
#...##..#
#...#...#
#.G...G.#
#.....G.#
#########";
            var day = new Day15(input);

            var outcome = day.SimFlawlessVictory();

            Assert.Equal(1140, outcome);
        }

        [Fact]
        public void Q2()
        {
            var day = new Day15(Inputs.Day15);

            var outcome = day.SimFlawlessVictory();

            Assert.Equal(62958, outcome);
        }
    }
}