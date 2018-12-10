using Xunit;
using Xunit.Abstractions;

namespace AoC2018.tests
{
    public class Day10Tests
    {
        private readonly ITestOutputHelper _output;

        public Day10Tests(ITestOutputHelper output)
        {
            _output = output;
        }

        #region Test Input

        private readonly string _testInput = @"position=< 9,  1> velocity=< 0,  2>
position=< 7,  0> velocity=<-1,  0>
position=< 3, -2> velocity=<-1,  1>
position=< 6, 10> velocity=<-2, -1>
position=< 2, -4> velocity=< 2,  2>
position=<-6, 10> velocity=< 2, -2>
position=< 1,  8> velocity=< 1, -1>
position=< 1,  7> velocity=< 1,  0>
position=<-3, 11> velocity=< 1, -2>
position=< 7,  6> velocity=<-1, -1>
position=<-2,  3> velocity=< 1,  0>
position=<-4,  3> velocity=< 2,  0>
position=<10, -3> velocity=<-1,  1>
position=< 5, 11> velocity=< 1, -2>
position=< 4,  7> velocity=< 0, -1>
position=< 8, -2> velocity=< 0,  1>
position=<15,  0> velocity=<-2,  0>
position=< 1,  6> velocity=< 1,  0>
position=< 8,  9> velocity=< 0, -1>
position=< 3,  3> velocity=<-1,  1>
position=< 0,  5> velocity=< 0, -1>
position=<-2,  2> velocity=< 2,  0>
position=< 5, -2> velocity=< 1,  2>
position=< 1,  4> velocity=< 2,  1>
position=<-2,  7> velocity=< 2, -2>
position=< 3,  6> velocity=<-1, -1>
position=< 5,  0> velocity=< 1,  0>
position=<-6,  0> velocity=< 2,  0>
position=< 5,  9> velocity=< 1, -2>
position=<14,  7> velocity=<-2,  0>
position=<-3,  6> velocity=< 2, -1>";

        #endregion

        [Fact]
        public void CanParse()
        {
            var results = new Day10(_testInput).Stars;
            
            var expected = new Star
            {
                X=6, Y = 10, VelocityY = -1, VelocityX = -2
            };
            Assert.Equal(expected, results[3]);
        }

        [Fact]
        public void CanRunAOneSecondStep()
        {
            var day10 = new Day10(_testInput);
            day10.MoveAll();

            var expected = new Star
            {
                X = 4,
                Y = 9,
                VelocityY = -1,
                VelocityX = -2
            };
            Assert.Equal(expected, day10.Stars[3]);
        }

        [Fact]
        public void CanRunThreeSteps()
        {
            var day10 = new Day10(_testInput);
            day10.MoveAll(3);

            var expected = new Star
            {
                X = 0,
                Y = 7,
                VelocityY = -1,
                VelocityX = -2
            };
            Assert.Equal(expected, day10.Stars[3]);
        }

        [Fact]
        public void CanFindSecondsToConvergenceForExample()
        {
            var day10 = new Day10(_testInput);
            var result = day10.FindNumberOfSecondBeforeMaximumConvergence();

            Assert.Equal(3, result);
        }

        [Fact]
        public void CanRenderExample()
        {
            var day10 = new Day10(_testInput);

            day10.RenderToConsoleOutput(3, _output);

            Assert.True(true);
        }

        [Fact]
        public void CanFindSecondsToConvergenceForQ1()
        {
            var day10 = new Day10(Inputs.Day10);
            var result = day10.FindNumberOfSecondBeforeMaximumConvergence();

            Assert.Equal(10613, result);
        }

        [Fact]
        public void RendersForStepsAroundConvergenceTime()
        {
            var maxConvergenceSecond = 10613;

            for (int i = maxConvergenceSecond -2 ; i <= maxConvergenceSecond + 2; i++)
            {
                _output.WriteLine("---- " + i + " -------------------------------------------------------------------");
                var day10 = new Day10(Inputs.Day10);

                day10.RenderToConsoleOutput(i, _output);

                _output.WriteLine("----------------------------------------------------------------------------------");
            }

            //NOTE: from visual inspection from the output we can find that 10612 gives out the answer (for this input)
            Assert.True(true);
        }

        [Fact]
        public void CanRenderQ1()
        {
            var day10 = new Day10(Inputs.Day10);

            day10.RenderToConsoleOutput(10612, _output);

            Assert.True(true);
        }
    }
}