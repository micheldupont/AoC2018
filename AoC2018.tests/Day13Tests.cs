using Xunit;

namespace AoC2018.tests
{
	public class Day13Tests
	{
		private string _testInput = @"
/->-\        
|   |  /----\
| /-+--+-\  |
| | |  | v  |
\-+-/  \-+--/
  \------/   
";

		[Fact]
		public void CanParseMap()
		{
			var day13 = new Day13(_testInput);

			Assert.Equal('/', day13.Map[0,0]);
			Assert.Equal('-', day13.Map[1, 0]);
			Assert.Equal('|', day13.Map[0, 2]);
			Assert.Equal('+', day13.Map[4, 2]);
			Assert.Equal(13, day13.Map.GetLength(0));
			Assert.Equal(6, day13.Map.GetLength(1));
			Assert.Equal(' ', day13.Map[12, 5]);
		}

		[Fact]
		public void CanDetectCarts()
		{
			var day13 = new Day13(_testInput);

			Assert.Equal('-', day13.Map[2, 0]);
			Assert.Equal('|', day13.Map[9, 3]);
			Assert.Equal(2, day13.Carts.Count);		
		}

		[Fact]
		public void CanParseCarts()
		{
			var day13 = new Day13(_testInput);

			Assert.Equal(2, day13.Carts.Count);
			Assert.Equal(2, day13.Carts[0].X);
			Assert.Equal(0, day13.Carts[0].Y);
			Assert.Equal('-', day13.Carts[0].Path);
			Assert.Equal(Day13.Headings.Right, day13.Carts[0].Heading);

			Assert.Equal(9, day13.Carts[1].X);
			Assert.Equal(3, day13.Carts[1].Y);
			Assert.Equal('|', day13.Carts[1].Path);
			Assert.Equal(Day13.Headings.Down, day13.Carts[1].Heading);
		}

		[Fact]
		public void CartCanMove()
		{
			var day13 = new Day13(_testInput);
			var cart = day13.Carts[0];

			cart.Move();

			Assert.Equal(3, cart.X);
			Assert.Equal(0, cart.Y);
			Assert.Equal('-', cart.Path);
			Assert.Equal(Day13.Headings.Right, cart.Heading);

			cart.Move();

			Assert.Equal(4, cart.X);
			Assert.Equal(0, cart.Y);
			Assert.Equal('\\', cart.Path);
			Assert.Equal(Day13.Headings.Down, cart.Heading);

			cart.Move();

			Assert.Equal(4, cart.X);
			Assert.Equal(1, cart.Y);
			Assert.Equal('|', cart.Path);
			Assert.Equal(Day13.Headings.Down, cart.Heading);
		}

		[Fact]
		public void CartCanMoveThroughIntersections()
		{
			var day13 = new Day13(_testInput);
			var cart = day13.Carts[0];

			cart.Move();
			cart.Move();
			cart.Move();
			cart.Move(); //x here, go left
			cart.Move();

			Assert.Equal(5, cart.X);
			Assert.Equal(2, cart.Y);
			Assert.Equal('-', cart.Path);
			Assert.Equal(Day13.Headings.Right, cart.Heading);

			cart.Move();
			cart.Move(); //x here, go straight
			cart.Move();

			Assert.Equal(8, cart.X);
			Assert.Equal(2, cart.Y);
			Assert.Equal('-', cart.Path);
			Assert.Equal(Day13.Headings.Right, cart.Heading);

			cart.Move();
			cart.Move();
			cart.Move(); //x here, go right
			cart.Move();

			Assert.Equal(8, cart.X);
			Assert.Equal(4, cart.Y);
			Assert.Equal('-', cart.Path);
			Assert.Equal(Day13.Headings.Left, cart.Heading);
		}

		[Fact]
		public void CanDetectCrashes()
		{
			var day13 = new Day13(_testInput);

			var crash = day13.SimUntilCrash();

			Assert.Equal(7, crash.X);
			Assert.Equal(3, crash.Y);
		}

		[Fact]
		public void Q1()
		{
			var day13 = new Day13(Inputs.Day13);

			var crash = day13.SimUntilCrash();

			Assert.Equal(103, crash.X);
			Assert.Equal(85, crash.Y);
		}

		private string _testInputB = @"
/>-<\  
|   |  
| /<+-\
| | | v
\>+</ |
  |   ^
  \<->/
";

		[Fact]
		public void CanSimUntilOneCartRemains()
		{
			var day13 = new Day13(_testInputB);

			var lastCart = day13.SimToLastCart();

			Assert.Equal(6, lastCart.X);
			Assert.Equal(4, lastCart.Y);
		}

		[Fact]
		public void Q2()
		{
			var day13 = new Day13(Inputs.Day13);

			var lastCart = day13.SimToLastCart();

			Assert.Equal(88, lastCart.X);
			Assert.Equal(64, lastCart.Y);
		}
	}
}