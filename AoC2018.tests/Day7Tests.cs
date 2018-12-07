using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AoC2018.tests
{
	public class Day7Tests
	{
		private readonly string _testInput = @"Step C must be finished before step A can begin.
Step C must be finished before step F can begin.
Step A must be finished before step B can begin.
Step A must be finished before step D can begin.
Step B must be finished before step E can begin.
Step D must be finished before step E can begin.
Step F must be finished before step E can begin.";

		[Fact]
		public void CanParseSteps()
		{
			var inputs = _testInput.Split(new[] {'\r', '\n'}, StringSplitOptions.RemoveEmptyEntries);

			var results = Day7.Parse(inputs);

			Assert.Equal(6, results.Count());
		}

		[Fact]
		public void CanParseStepsAndReturnThemInAlphaOrder()
		{
			var inputs = _testInput.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

			var results = Day7.Parse(inputs);

			Assert.Equal("A", results[0].Name);
			Assert.Equal("F", results[5].Name);
		}

		[Fact]
		public void CanParseStepsWithUnlockList()
		{
			var inputs = _testInput.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

			var results = Day7.Parse(inputs);

			Assert.Equal(2, results[0].Unlocks.Count());
			Assert.Equal(1, results[1].Unlocks.Count());
			Assert.Equal(2, results[2].Unlocks.Count());
			Assert.Equal(1, results[3].Unlocks.Count());
			Assert.Equal(0, results[4].Unlocks.Count());
			Assert.Equal(1, results[5].Unlocks.Count());
		}

		[Fact]
		public void CanFlagStepsWithoutDependencies()
		{
			var inputs = _testInput.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
			var results = Day7.Parse(inputs);

			Day7.UpdateStatus(results);

			Assert.True(results[0].IsLocked);
			Assert.True(results[1].IsLocked);
			Assert.False(results[2].IsLocked);
			Assert.True(results[3].IsLocked);
			Assert.True(results[4].IsLocked);
			Assert.True(results[5].IsLocked);
		}

		[Fact]
		public void CanReturnUnlockedSingleStep()
		{
			var inputs = _testInput.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
			var steps = Day7.Parse(inputs);

			var results = Day7.FindUnlockedSteps(steps);

			Assert.Equal(1, results.Count());
			Assert.Equal("C", results[0].Name);
		}

		[Fact]
		public void CanReturnUnlockedSteps()
		{
			var inputs = _testInput.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
			var steps = Day7.Parse(inputs);
			steps.RemoveAt(2);

			var results = Day7.FindUnlockedSteps(steps);

			Assert.Equal(2, results.Count());
			Assert.Equal("A", results[0].Name);
			Assert.Equal("F", results[1].Name);
		}

		[Fact]
		public void CanFindTheRightUnlockSequence()
		{
			var inputs = _testInput.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
			var steps = Day7.Parse(inputs);

			var result = Day7.GetUnlockSequence(steps);

			Assert.Equal("CABDFE", result);
		}

		[Fact]
		public void Q1()
		{
			var inputs = Inputs.Day7.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
			var steps = Day7.Parse(inputs);

			var result = Day7.GetUnlockSequence(steps);

			Assert.Equal("GJKLDFNPTMQXIYHUVREOZSAWCB", result);
		}

		[Fact]
		public void CanCalculateInitialDurationForStep()
		{
			var inputs = _testInput.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
			var steps = Day7.Parse(inputs);

			Day7.SetDuration(0, steps[0]);
			Day7.SetDuration(60, steps[5]);

			Assert.Equal(1, steps[0].Duration);
			Assert.Equal(66, steps[5].Duration);
		}

		[Fact]
		public void CanSimulateOneStep()
		{
			var inputs = _testInput.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
			var steps = Day7.Parse(inputs);

			var result = Day7.Simulate(1, 0, new List<Step> {steps[2]});

			Assert.Equal(3, result);
		}

		[Fact]
		public void CanSimulateOneStepB()
		{
			var inputs = _testInput.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
			var steps = Day7.Parse(inputs);

			var result = Day7.Simulate(1, 0, new List<Step> { steps[0] });

			Assert.Equal(1, result);
		}

		[Fact]
		public void CanSimulateTwoLinearSteps()
		{
			var inputs = _testInput.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
			var steps = Day7.Parse(inputs);

			var result = Day7.Simulate(1, 10, new List<Step> { steps[0], steps[2] });

			Assert.Equal(24, result);
		}

		[Fact]
		public void CanSimulateTwoLinearStepsB()
		{
			var inputs = _testInput.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
			var steps = Day7.Parse(inputs);

			var result = Day7.Simulate(1, 0, new List<Step> { steps[0], steps[1] });

			Assert.Equal(3, result);
		}

		[Fact]
		public void CanSimulateTwoParallelStepsWithOneWorker()
		{
			var inputs = _testInput.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
			var steps = Day7.Parse(inputs);

			var result = Day7.Simulate(1, 10, new List<Step> { steps[0], steps[2], steps[5] });

			Assert.Equal(40, result);
		}

		[Fact]
		public void CanSimulateTwoParallelStepsWithTwoWorkers()
		{
			var inputs = _testInput.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
			var steps = Day7.Parse(inputs);

			var result = Day7.Simulate(2, 0, new List<Step> { steps[0], steps[2], steps[5] });

			Assert.Equal(9, result);
		}

		[Fact]
		public void CanSimulateTwoParallelStepsWithTwoWorkersB()
		{
			var inputs = _testInput.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
			var steps = Day7.Parse(inputs);

			var result = Day7.Simulate(2, 0, new List<Step> { steps[0], steps[2], steps[5] });

			Assert.Equal(9, result);
		}

		[Fact]
		public void CanSimulateExample()
		{
			var inputs = _testInput.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
			var steps = Day7.Parse(inputs);

			var result = Day7.Simulate(2, 0, steps);

			Assert.Equal(15, result);
		}

		[Fact]
		public void Q2()
		{
			var inputs = Inputs.Day7.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
			var steps = Day7.Parse(inputs);

			var result = Day7.Simulate(5, 60, steps);

			Assert.Equal(967, result);
		}
	}
}