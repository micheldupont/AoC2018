using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2018
{
	public class Day7
	{
		/* Step C must be finished before step A can begin. */
		/*   0  1   2   3     4      5      6  7  8    9    */
		public static List<Step> Parse(string[] inputs)
		{
			var list = new List<Step>();

			foreach (var line in inputs)
			{
				var parts = line.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
				var name = parts[1];
				var unlockName = parts[7];

				var step = list.Find(s => s.Name == name);
				if (step == null)
				{
					step = new Step{Name = name};
					list.Add(step);
				}

				var unlock = list.Find(s => s.Name == unlockName);
				if (unlock == null)
				{
					unlock = new Step { Name = unlockName };
					list.Add(unlock);
				}

				step.Unlocks.Add(unlock);
			}

			return list.OrderBy(s => s.Name).ToList();
		}

		public static void UpdateStatus(List<Step> steps)
		{
			var allUnlocks = steps.SelectMany(s => s.Unlocks).ToList();

			foreach (var step in steps)
			{
				step.IsLocked = true;
				if (allUnlocks.All(u => u.Name != step.Name))
				{
					step.IsLocked = false;
				}
			}
		}

		public static List<Step> FindUnlockedSteps(List<Step> steps)
		{
			UpdateStatus(steps);

			return steps.Where(s => !s.IsLocked).OrderByDescending(s => s.WorkingOnIt).ThenBy(s => s.Name).ToList();
		}

		public static string GetUnlockSequence(List<Step> steps)
		{
			var result = "";
			while (steps.Count > 0)
			{
				var unlocked = FindUnlockedSteps(steps);

				var firstUnlocked = unlocked.First();
				result += firstUnlocked.Name;

				steps.Remove(firstUnlocked);
			}

			return result;
		}

		public static void SetDuration(int baseDuration, Step step)
		{
			step.Duration = baseDuration;

			var index = char.ToUpper(step.Name[0]) -64;

			step.Duration += index;
		}

		public static int Simulate(int workerCount, int baseDuration, List<Step> steps)
		{
			if (workerCount <= 0)
			{
				return -1;
			}

			var duration = 0;

			foreach (var step in steps)
			{
				SetDuration(baseDuration, step);
			}

			while (steps.Count > 0)
			{
				duration++;
				var unlocked = FindUnlockedSteps(steps);

				var topUnlockedSteps = unlocked.Take(workerCount);

				foreach (var step in topUnlockedSteps)
				{
					step.WorkingOnIt = true;
					step.Duration--;
					if (step.Duration <= 0)
					{
						steps.Remove(step);
					}
				}
			}

			return duration;
		}
	}

	public class Step
	{
		public string Name { get; set; }
		public List<Step> Unlocks { get; }
		public bool IsLocked { get; set; }
		public int Duration { get; set; }
		public bool WorkingOnIt { get; set; }

		public Step()
		{
			Unlocks = new List<Step>();
			IsLocked = true;
			WorkingOnIt = false;
		}
	}
}
 