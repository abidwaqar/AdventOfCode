using System.Linq;

namespace AdventOfCode._22._6_TuningTrouble
{
    internal static class PartTwo
    {
        // T = O(n) | S = O(1)
        public static int Solve()
        {
            var map = new Dictionary<char, int>();
            var startIdx = -1;
            var distinctCharacters = 14;
            var input = File.ReadAllLines("../../../input.txt").Single();

            for (int i = 0; i < input.Length; ++i)
            {
                if (!map.ContainsKey(input[i]) || map[input[i]] < startIdx)
                {
                    map[input[i]] = i;
                    if (i - startIdx == distinctCharacters)
                    {
                        return i + 1;
                    }
                }
                else
                {
                    startIdx = map[input[i]];
                    map[input[i]] = i;
                }
            }

            return -1;
        }
    }
}
