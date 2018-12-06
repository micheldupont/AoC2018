using System;
using System.Collections.Generic;
using Xunit;

namespace AoC2018.tests
{
    public class Day4Tests
    {
        [Fact]
        public void CanParseAShiftGuardLine()
        {
            var input = "[1518-11-01 00:00] Guard #10 begins shift";
            var result = Day4.ParseEvent(input);

            Assert.Equal(10, result.GuardId);
            Assert.Equal(Day4.ShiftEventTypes.ShiftStart, result.EventType);
            Assert.Equal(new DateTime(1518,11,1,0,0,0), result.EventTime);
        }

        [Fact]
        public void CanParseAShiftSleepLine()
        {
            var input = "[1518-11-01 00:05] falls asleep";
            var result = Day4.ParseEvent(input);

            Assert.Equal(-1, result.GuardId);
            Assert.Equal(Day4.ShiftEventTypes.FallAsleep, result.EventType);
            Assert.Equal(new DateTime(1518, 11, 1, 0, 5, 0), result.EventTime);
        }

        [Fact]
        public void CanParseAShiftWakeUpLine()
        {
            var input = "[1518-11-02 00:50] wakes up";
            var result = Day4.ParseEvent(input);

            Assert.Equal(-1, result.GuardId);
            Assert.Equal(Day4.ShiftEventTypes.WakeUp, result.EventType);
            Assert.Equal(new DateTime(1518, 11, 2, 0, 50, 0), result.EventTime);
        }

        [Fact]
        public void CanParseAShiftWithGuardIdAndEventsInOrder()
        {
            var input = new []
            {
                "[1518-11-01 00:05] falls asleep",
                "[1518-11-01 00:00] Guard #10 begins shift",
                "[1518-11-01 00:50] wakes up"
            };
            var expectedEvents = new List<Day4.ShiftEvent>
            {
                Day4.ParseEvent(input[1]),
                Day4.ParseEvent(input[0]),
                Day4.ParseEvent(input[2])
            };

            var results = Day4.ParseShifts(input);

            Assert.Equal(10, results[0].GuardId);
            Assert.Equal(expectedEvents, results[0].Events);
        }

        [Fact]
        public void CanParseTwoShiftstWithGuardIdAndEventsInOrder()
        {
            var input = new[]
            {     
                "[1518-11-02 00:00] Guard #14 begins shift",           
                "[1518-11-01 00:05] falls asleep",
                "[1518-11-02 00:50] wakes up",
                "[1518-11-01 00:00] Guard #10 begins shift",
                "[1518-11-01 00:50] wakes up",
                "[1518-11-02 00:05] falls asleep",
            };
            var expectedEventsA = new List<Day4.ShiftEvent>
            {
                Day4.ParseEvent(input[3]),
                Day4.ParseEvent(input[1]),
                Day4.ParseEvent(input[4])
            };

            var expectedEventsB = new List<Day4.ShiftEvent>
            {
                Day4.ParseEvent(input[0]),
                Day4.ParseEvent(input[5]),
                Day4.ParseEvent(input[2])
            };

            var results = Day4.ParseShifts(input);

            Assert.Equal(10, results[0].GuardId);
            Assert.Equal(expectedEventsA, results[0].Events);
            Assert.Equal(14, results[1].GuardId);
            Assert.Equal(expectedEventsB, results[1].Events);
        }

        [Fact]
        public void CanParseTwoShiftstWithGuardIdAndEventsInOrderEventIfGuardComesInEarly()
        {
            var input = new[]
            {
                "[1518-11-01 23:50] Guard #14 begins shift",
                "[1518-11-01 00:05] falls asleep",
                "[1518-11-02 00:50] wakes up",
                "[1518-10-31 23:00] Guard #10 begins shift",
                "[1518-11-01 00:50] wakes up",
                "[1518-11-02 00:05] falls asleep",
            };
            var expectedEventsA = new List<Day4.ShiftEvent>
            {
                Day4.ParseEvent(input[3]),
                Day4.ParseEvent(input[1]),
                Day4.ParseEvent(input[4])
            };

            var expectedEventsB = new List<Day4.ShiftEvent>
            {
                Day4.ParseEvent(input[0]),
                Day4.ParseEvent(input[5]),
                Day4.ParseEvent(input[2])
            };

            var results = Day4.ParseShifts(input);

            Assert.Equal(10, results[0].GuardId);
            Assert.Equal(expectedEventsA, results[0].Events);
            Assert.Equal(14, results[1].GuardId);
            Assert.Equal(expectedEventsB, results[1].Events);
        }

        [Fact]
        public void CanCalculateSleepMinutesForGuardShift()
        {
            var input = new[]
            {
                "[1518-11-01 00:05] falls asleep",
                "[1518-11-01 00:00] Guard #10 begins shift",
                "[1518-11-01 00:50] wakes up"
            };
            
            var results = Day4.ParseShifts(input);

            Assert.Equal(45, results[0].GetSleepTime());
        }

        [Fact]
        public void CanCalculateSleepMinutesForGuardShiftWhenThereAreMultipleSleepCycles()
        {
            var input = new[]
            {
                "[1518-11-01 00:05] falls asleep",
                "[1518-11-01 00:00] Guard #10 begins shift",
                "[1518-11-01 00:10] wakes up",
                "[1518-11-01 00:15] falls asleep",
                "[1518-11-01 00:20] wakes up",
                "[1518-11-01 00:30] falls asleep",
                "[1518-11-01 00:40] wakes up",
            };
          
            var results = Day4.ParseShifts(input);

            Assert.Equal(20, results[0].GetSleepTime());
        }

        [Fact]
        public void CanCalculateSleepPeriodsForGuardShift()
        {
            var input = new[]
            {
                "[1518-11-01 00:06] falls asleep",
                "[1518-11-01 00:00] Guard #10 begins shift",
                "[1518-11-01 00:13] wakes up"
            };

            var results = Day4.ParseShifts(input);

            Assert.Equal(new []{6,7,8,9,10,11,12}, results[0].GetSleepPeriods());
        }

        [Fact]
        public void CanFindBestSleeper()
        {
            var input = new[]
            {
                "[1518-11-02 00:00] Guard #14 begins shift",
                "[1518-11-01 00:05] falls asleep",
                "[1518-11-02 00:50] wakes up",
                "[1518-11-01 00:00] Guard #10 begins shift",
                "[1518-11-01 00:50] wakes up",
                "[1518-11-02 00:05] falls asleep",
                "[1518-11-05 00:00] Guard #10 begins shift",
                "[1518-11-05 00:50] wakes up",
                "[1518-11-05 00:05] falls asleep",
            };

            var results = Day4.FindBestSleeper(input);

            Assert.Equal(10, results.Id);
            Assert.Equal(90, results.MinuteSlept);
        }

        [Fact]
        public void CanFindMyBestSleeper()
        {
            var input = Inputs.Day4.Split(new[] {'\r', '\n'}, StringSplitOptions.RemoveEmptyEntries);

            var results = Day4.FindBestSleeper(input);

            Assert.Equal(1523, results.Id);
            Assert.Equal(483, results.MinuteSlept);
        }

        [Fact]
        public void CanFindMostSleptPeriod()
        {
            var input = new[]
            {
                "[1518-11-02 00:00] Guard #10 begins shift",
                "[1518-11-01 00:05] falls asleep",
                "[1518-11-02 00:10] wakes up",
                "[1518-11-01 00:00] Guard #10 begins shift",
                "[1518-11-01 00:06] wakes up",
                "[1518-11-02 00:05] falls asleep",
                "[1518-11-05 00:00] Guard #10 begins shift",
                "[1518-11-05 00:15] wakes up",
                "[1518-11-05 00:05] falls asleep",
            };

            var result = Day4.FindBestPeriod(10, input);

            Assert.Equal(5, result.Index);
        }

        [Fact]
        public void CanFindMyBestPeriod()
        {
            var input = Inputs.Day4.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            var result = Day4.FindBestPeriod(1523, input);

            var Q1 = 1523 * result.Index;
            Assert.Equal(43, result.Index);
            Assert.Equal(65489, Q1);
        }

        [Fact]
        public void Q2()
        {
            var input = Inputs.Day4.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            var result = Day4.Q2(input);

            Assert.Equal(3852, result);
        }
    }
}