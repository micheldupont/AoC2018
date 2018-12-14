using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AoC2018
{
	public class Day14
	{
		private const int ScoreLength = 10;
		public List<int> RecipeScores { get; }
		public List<int> ElvesCurrentRecipeIndexes { get; }


		public Day14(int elfCount, int[] initialScores)
		{
			RecipeScores = new List<int>(initialScores);
			ElvesCurrentRecipeIndexes = new List<int>();

			for (var i = 0; i < elfCount; i++)
			{
				ElvesCurrentRecipeIndexes.Add(i);
			}
		}

		public void CreateNewRecipes()
		{
			var combinedScore = 0;

			foreach (var elfRecipeIndex in ElvesCurrentRecipeIndexes)
			{
				combinedScore += RecipeScores[elfRecipeIndex];
			}

			if (combinedScore >= 10)
			{
				var first = combinedScore / 10;
				var second = combinedScore % 10;

				RecipeScores.AddRange(new[] {first, second});
			}
			else
			{
				RecipeScores.Add(combinedScore);
			}

			for (var index = 0; index < ElvesCurrentRecipeIndexes.Count; index++)
			{
				var elfRecipeIndex = ElvesCurrentRecipeIndexes[index];

				var currentRecipeScore = RecipeScores[elfRecipeIndex];
				var steps = currentRecipeScore + 1;

				var newIndex = elfRecipeIndex + steps;
				var normalizedIndex = newIndex % RecipeScores.Count;

				ElvesCurrentRecipeIndexes[index] = normalizedIndex;
			}
		}

		public string GetScoreAfterRecipeCount(int recipeCount)
		{
			var targetRecipeCount = recipeCount + ScoreLength;

			while (RecipeScores.Count < targetRecipeCount)
			{
				CreateNewRecipes();
			}

			var scoreItems = RecipeScores.GetRange(recipeCount, ScoreLength);
			var score = new StringBuilder();
			foreach (var scoreItem in scoreItems)
			{
				score.Append(scoreItem);
			}

			return score.ToString();
		}

		public int FindRecipeCountForScore(string input)
		{
			var inputAsIntArray = input.ToCharArray().Select(c => int.Parse(new string(new[] {c}))).ToArray();
			
			while (!InputExistsInScores(inputAsIntArray))
			{
				CreateNewRecipes();
			}

			var scoreBuilder = new StringBuilder(RecipeScores.Count);
			foreach (var scoreItem in RecipeScores)
			{
				scoreBuilder.Append(scoreItem);
			}

			var scoresAsString = scoreBuilder.ToString();
			var matchIndex = scoresAsString.IndexOf(input, StringComparison.Ordinal);
			var scoreBeforeMatch = scoresAsString.Substring(0, matchIndex);

			return scoreBeforeMatch.Length;
		}

		private bool InputExistsInScores(int[] inputAsIntArray)
		{
			var scores = RecipeScores;
			var scoresLength = scores.Count;
			var inputLength = inputAsIntArray.Length;

			// always check at the end of the score list
			// (padding by 2 because that's the max number of value we can add in one turn)
			var scoreStartingIndex = scoresLength - inputLength - 2; 
			if (scoreStartingIndex < 0)
				scoreStartingIndex = 0;

			for (var recipeIndex = scoreStartingIndex; recipeIndex < scoresLength; recipeIndex++)
			{
				//if there is not enough item in score left to cover the input length, no use to keep going
				if (recipeIndex + inputLength > scoresLength - 1) continue;

				//if no match here there is no use to keep going
				if (scores[recipeIndex] != inputAsIntArray[0]) continue;

				var j = recipeIndex;
				for (var i = 0; i < inputLength; i++)
				{
					if (scores[j] == inputAsIntArray[i])
					{
						j++;
					}
					else
					{
						break;
					}
				}

				//if we went through all inputs without break-ing, we have a match
				if ((j - recipeIndex) == inputLength)
				{
					return true;
				}
			}

			return false;
		}
	}
}