using System.Collections.Generic;
using System.Linq;

namespace AoC2018
{
    public class Day2
    {
        private readonly List<string> _input;

        public Day2(List<string> input)
        {
            _input = input;
        }

        public int RunQ1()
        {
            var charDuoCount = 0;
            var charTrioCount = 0;

            foreach (var str in _input)
            {
                if (TestForXOfSameChar(str, 2))
                {
                    charDuoCount++;
                }

                if (TestForXOfSameChar(str, 3))
                {
                    charTrioCount++;
                }
            }

            return charDuoCount * charTrioCount;
        }

        public bool TestForXOfSameChar(string testInput, int targetCount)
        {
            var uniqueCharList = new List<char>();

            foreach (var c in testInput.ToCharArray())
            {
                if (!uniqueCharList.Contains(c))
                {
                    uniqueCharList.Add(c);
                }
            }

            foreach (var c in uniqueCharList)
            {
                var charCount = testInput.Count(i => i == c);
                if (charCount == targetCount)
                {
                    return true;
                }
            }

            return false;
        }

        public string RunQ2()
        {
            for (int i = 0; i < _input.Count; i++)
            {
                for (int j = i + 1; j < _input.Count; j++)
                {
                    var missMatchCount = CountCharMissMatch(_input[i], _input[j]);

                    if (missMatchCount == 1)
                    {
                        return GetMatchString(_input[i], _input[j]);
                    }
                }
            }

            return "";
        }

        public int CountCharMissMatch(string strA, string strB)
        {
            var aAsCharArray = strA.ToCharArray();
            var bAsCharArray = strB.ToCharArray();

            var missMatchCount = 0;

            for (int i = 0; i < aAsCharArray.Length; i++)
            {
                if (aAsCharArray[i] != bAsCharArray[i])
                {
                    missMatchCount++;
                }
            }

            return missMatchCount;
        }

        public string GetMatchString(string strA, string strB)
        {
            var aAsCharArray = strA.ToCharArray();
            var bAsCharArray = strB.ToCharArray();

            var matchingChar = new List<char>();

            for (int i = 0; i < aAsCharArray.Length; i++)
            {
                if (aAsCharArray[i] == bAsCharArray[i])
                {
                    matchingChar.Add(aAsCharArray[i]);
                }
            }

            return new string(matchingChar.ToArray());
        }
    }
}