using Xunit;

namespace AoC2018.tests
{
    public class Day8Tests
    {
        private readonly string _exampleInput = @"2 3 0 3 10 11 12 1 1 0 1 99 2 1 1 2";

        [Fact]
        public void CanParseInts()
        {
            var day8 = new Day8(_exampleInput);

            Assert.Equal(16, day8.InputLength);
        }

        [Fact]
        public void CanBuildFirstNodeHeader()
        {
            var result = new Day8(_exampleInput).ParseNodes();

            Assert.Equal(2, result.ChildCount);
            Assert.Equal(3, result.MetadataLength);
        }

        [Fact]
        public void CanBuildFirstNodeWithChildsAndMetadata()
        {
            var result = new Day8(_exampleInput).ParseNodes();

            Assert.Equal(2, result.ChildNodes.Count);
            Assert.Equal(3, result.Metadata.Count);
        }

        [Fact]
        public void CanCalculateChecksum()
        {
            var root = new Day8(_exampleInput).ParseNodes();

            Assert.Equal(138, root.GetChecksum());
        }

        [Fact]
        public void Q1()
        {
            var root = new Day8(Inputs.Day8).ParseNodes();

            Assert.Equal(46096, root.GetChecksum());
        }

        [Fact]
        public void CanCalculateValue()
        {
            var root = new Day8(_exampleInput).ParseNodes();

            Assert.Equal(66, root.GetValue());
        }

        [Fact]
        public void Q2()
        {
            var root = new Day8(Inputs.Day8).ParseNodes();

            Assert.Equal(24820, root.GetValue());
        }
    }
}