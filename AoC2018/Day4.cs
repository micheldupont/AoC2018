using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;

namespace AoC2018
{
    public class Day4
    {
        public enum ShiftEventTypes
        {
            ShiftStart,
            FallAsleep,
            WakeUp
        }
        public class ShiftEvent
        {
            protected bool Equals(ShiftEvent other)
            {
                return EventType == other.EventType && EventTime.Equals(other.EventTime) && GuardId == other.GuardId;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != this.GetType()) return false;
                return Equals((ShiftEvent) obj);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    var hashCode = (int) EventType;
                    hashCode = (hashCode * 397) ^ EventTime.GetHashCode();
                    hashCode = (hashCode * 397) ^ GuardId;
                    return hashCode;
                }
            }

            public ShiftEventTypes EventType { get; }
            public DateTime EventTime { get; }
            public int GuardId { get; }

            public ShiftEvent(ShiftEventTypes eventType, DateTime eventTime, int guardId)
            {
                this.EventType = eventType;
                this.EventTime = eventTime;
                this.GuardId = guardId;
            }
        }

        //"[1518-11-01 00:00] Guard #10 begins shift"
        //         0     1      2    3    4      5
        //
        //"[1518-11-01 00:05] falls asleep"
        //        0       1     2      3
        //
        //"[1518-11-02 00:50] wakes up"
        //        0       1     2    3
        public static ShiftEvent ParseEvent(string input)
        {
            var parts = input.Split(new[] {'[', ']', ' '}, StringSplitOptions.RemoveEmptyEntries);

            var eventTime = DateTime.ParseExact(parts[0] + " " + parts[1] , "yyyy-MM-dd HH:mm", CultureInfo.CurrentCulture);

            switch (parts[2])
            {
                case "Guard":
                {
                    var guardId = int.Parse(parts[3].Replace("#", ""));
                    var evt = new ShiftEvent(ShiftEventTypes.ShiftStart, eventTime, guardId);
                    return evt;
                }
                case "falls":
                {
                    var evt = new ShiftEvent(ShiftEventTypes.FallAsleep, eventTime, -1);
                    return evt;
                }
                case "wakes":
                {
                    var evt = new ShiftEvent(ShiftEventTypes.WakeUp, eventTime, -1);
                    return evt;
                }
            }

            throw new Exception("Unable to parse: " + input);
        }

        public static List<GuardShift> ParseShifts(string[] input)
        {
            var events = input.Select(ParseEvent).OrderBy(i => i.EventTime).ToList();

            if (events.Count == 0 || events[0].EventType != ShiftEventTypes.ShiftStart)
            {
                throw new Exception("Shifts does not start with guard!");
            }

            if (events.Count == 1)
            {
                throw new Exception("Missing data!");
            }

            var list = new List<GuardShift>();
            var currentGuardId = events[0].GuardId;
            var currentEvents = new List<ShiftEvent>{events[0]};

            for (var index = 1; index < events.Count; index++)
            {
                var shiftEvent = events[index];

                if (shiftEvent.EventType == ShiftEventTypes.ShiftStart)
                {
                    list.Add(new GuardShift(currentGuardId, currentEvents));
                    currentGuardId = shiftEvent.GuardId;
                    currentEvents = new List<ShiftEvent>{shiftEvent};
                }
                else
                {
                    currentEvents.Add(shiftEvent);
                }
            }

            if (currentEvents.Count > 0)
            {
                list.Add(new GuardShift(currentGuardId, currentEvents));
            }

            return list;
        }

        public static GuardStats FindBestSleeper(string[] input)
        {
            var guardShifts = ParseShifts(input);

            var guardStatsList = new List<GuardStats>();

            foreach (var guardShift in guardShifts)
            {
                var sleepTime = guardShift.GetSleepTime();

                var stat = guardStatsList.Find(s => s.Id == guardShift.GuardId);
                if (stat == null)
                {
                    stat = new GuardStats {Id = guardShift.GuardId, MinuteSlept = 0};
                    guardStatsList.Add(stat);
                }

                stat.MinuteSlept += sleepTime;
            }

            var best = guardStatsList.OrderByDescending(s => s.MinuteSlept).First();

            return best;
        }

        public static PeriodStats FindBestPeriod(int guardId, string[] input)
        {
            var guardShifts = ParseShifts(input).Where(s => s.GuardId == guardId).ToList();
            var list = new List<int>();
            foreach (var guardShift in guardShifts)
            {
                list.AddRange(guardShift.GetSleepPeriods());
            }

            if (list.Count == 0)
            {
                return null;
            }

            var mostCommonPeriod = list.GroupBy(i => i).OrderByDescending(grp => grp.Count()).Select(grp => new PeriodStats{Index= grp.Key, SleepCount = grp.Count()}).First();

            return mostCommonPeriod;
        }

        public static int Q2(string[] input)
        {
            var guardShifts = ParseShifts(input);

            var guardShiftsGroups = guardShifts.GroupBy(s => s.GuardId, s => s);

            var list = new List<GuardStats>();
            foreach (var guardId in guardShiftsGroups.Select(grp => grp.Key))
            {
                var best = FindBestPeriod(guardId, input);
                if (best != null)
                {
                    list.Add(new GuardStats{Id = guardId, BestPeriod = best.Index, BestPeriodSleepCount  = best.SleepCount});
                }
            }

            var result = list.OrderByDescending(s => s.BestPeriodSleepCount).Select(s => s.Id * s.BestPeriod).First();

            return result;

        }
    }

    public class GuardStats
    {
        public int Id { get; set; }
        public int MinuteSlept { get; set; }
        public int BestPeriod { get; set; }
        public int BestPeriodSleepCount { get; set; }
    }

    public class PeriodStats
    {
        public int Index { get; set; }
        public int SleepCount { get; set; }
    }

    public class GuardShift
    {
        public int GuardId { get; }
        public List<Day4.ShiftEvent> Events { get; }

        public GuardShift(int guardId, List<Day4.ShiftEvent> events)
        {
            this.GuardId = guardId;
            this.Events = events;
        }

        public int GetSleepTime()
        {
            var totalSleepTime = 0;

            Day4.ShiftEvent startSleeping = null;

            foreach (var shiftEvent in this.Events)
            {
                if (shiftEvent.EventType == Day4.ShiftEventTypes.FallAsleep)
                {
                    startSleeping = shiftEvent;
                }

                if (shiftEvent.EventType == Day4.ShiftEventTypes.WakeUp)
                {
                    if (startSleeping == null)
                    {
                        throw new Exception("Wrong sequence!");
                    }

                    var timeSleeping = shiftEvent.EventTime.Subtract(startSleeping.EventTime).Minutes;
                    totalSleepTime += timeSleeping;
                }
            }


            return totalSleepTime;
        }

        public int[] GetSleepPeriods()
        {
            var periods = new List<int>();

            Day4.ShiftEvent startSleeping = null;

            foreach (var shiftEvent in this.Events)
            {
                if (shiftEvent.EventType == Day4.ShiftEventTypes.FallAsleep)
                {
                    startSleeping = shiftEvent;
                }

                if (shiftEvent.EventType == Day4.ShiftEventTypes.WakeUp)
                {
                    if (startSleeping == null)
                    {
                        throw new Exception("Wrong sequence!");
                    }

                    for (int i = startSleeping.EventTime.Minute; i < shiftEvent.EventTime.Minute; i++)
                    { 
                        periods.Add(i);
                    }
                }
            }

            return periods.ToArray();
        }
    }
}