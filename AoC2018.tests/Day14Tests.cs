using System;
using Xunit;

namespace AoC2018.tests
{
	public class Day14Tests
	{
		[Fact]
		public void CanDoInit()
		{
			var day = new Day14(2, new[] {3, 7});

			Assert.Equal(0, day.ElvesCurrentRecipeIndexes[0]);
			Assert.Equal(1, day.ElvesCurrentRecipeIndexes[1]);
			Assert.Equal(3, day.RecipeScores[0]);
			Assert.Equal(7, day.RecipeScores[1]);
		}

		[Fact]
		public void CanDoFirstGeneration()
		{
			var day = new Day14(2, new[] {3, 7});

			day.CreateNewRecipes();

			Assert.Equal(4, day.RecipeScores.Count);
			Assert.Equal(1, day.RecipeScores[2]);
			Assert.Equal(0, day.RecipeScores[3]);
		}

		[Fact]
		public void ElvesCanPickNewRecipes()
		{
			var day = new Day14(2, new[] {3, 7});

			day.CreateNewRecipes();

			Assert.Equal(0, day.ElvesCurrentRecipeIndexes[0]);
			Assert.Equal(1, day.ElvesCurrentRecipeIndexes[1]);

			day.CreateNewRecipes();

			Assert.Equal(4, day.ElvesCurrentRecipeIndexes[0]);
			Assert.Equal(3, day.ElvesCurrentRecipeIndexes[1]);
		}

		[Fact]
		public void CanDoTenGenerations()
		{
			var day = new Day14(2, new[] {3, 7});

			day.CreateNewRecipes();
			day.CreateNewRecipes();
			day.CreateNewRecipes();
			day.CreateNewRecipes();
			day.CreateNewRecipes();
			day.CreateNewRecipes();
			day.CreateNewRecipes();
			day.CreateNewRecipes();
			day.CreateNewRecipes();
			day.CreateNewRecipes();

			Assert.Equal(1, day.ElvesCurrentRecipeIndexes[0]);
			Assert.Equal(13, day.ElvesCurrentRecipeIndexes[1]);
		}

		[Fact]
		public void ExampleA()
		{
			var day = new Day14(2, new[] { 3, 7 });

			var result = day.GetScoreAfterRecipeCount(9);

			Assert.Equal("5158916779", result);
		}

		[Fact]
		public void ExampleB()
		{
			var day = new Day14(2, new[] { 3, 7 });

			var result = day.GetScoreAfterRecipeCount(5);

			Assert.Equal("0124515891", result);
		}

		[Fact]
		public void ExampleC()
		{
			var day = new Day14(2, new[] { 3, 7 });

			var result = day.GetScoreAfterRecipeCount(18);

			Assert.Equal("9251071085", result);
		}

		[Fact]
		public void ExampleD()
		{
			var day = new Day14(2, new[] { 3, 7 });

			var result = day.GetScoreAfterRecipeCount(2018);

			Assert.Equal("5941429882", result);
		}

		[Fact]
		public void Q1()
		{
			var day = new Day14(2, new[] { 3, 7 });

			var result = day.GetScoreAfterRecipeCount(637061);

			Assert.Equal("3138510102", result);
		}

		[Fact]
		public void ExampleA2()
		{
			var day = new Day14(2, new[] { 3, 7 });

			var result = day.FindRecipeCountForScore("51589");

			Assert.Equal(9, result);
		}

		[Fact]
		public void ExampleB2()
		{
			var day = new Day14(2, new[] { 3, 7 });

			var result = day.FindRecipeCountForScore("01245");

			Assert.Equal(5, result);
		}

		[Fact]
		public void ExampleC2()
		{
			var day = new Day14(2, new[] { 3, 7 });

			var result = day.FindRecipeCountForScore("92510");

			Assert.Equal(18, result);
		}
		[Fact]
		public void ExampleD2()
		{
			var day = new Day14(2, new[] { 3, 7 });

			var result = day.FindRecipeCountForScore("59414");

			Assert.Equal(2018, result);
		}

		[Fact]
		public void Q2()
		{
			var day = new Day14(2, new[] { 3, 7 });

			var result = day.FindRecipeCountForScore("637061");

			Assert.Equal(20179081, result);
		}
	}
}