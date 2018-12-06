using System.Collections.Generic;
using System.Linq;

namespace AoC2018
{
    public class Day1
    {
        public int Run(int[] input)
        {
            var sum = input.Sum();
            return sum;
        }

        public int Run2(int[] input)
        {
            var index = 0;
            var currentFrequency = 0;
            var knownFrequencies = new HashSet<int>{0};

            while (true)
            {
                var currentIndex = index % input.Length;
                var currentItem = input[currentIndex];
                currentFrequency = currentFrequency + currentItem;

                if (knownFrequencies.Contains(currentFrequency))
                {
                    return currentFrequency;
                }

                knownFrequencies.Add(currentFrequency);
                index++;
            }
        }
    }
}