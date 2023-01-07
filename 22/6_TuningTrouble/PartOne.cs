namespace AdventOfCode._22._6_TuningTrouble
{
    internal static class PartOne
    {
        // T = O(n) | S = O(1)
        public static int Solve()
        {
            ISet<char> set = new HashSet<char>();
            int distinctCharacters = 4;
            int startIdx = 0;
            string input = File.ReadAllLines("../../../Input/prod.txt").Single();

            for (int i = 0; i < input.Length; ++i)
            {
                if (!set.Contains(input[i]))
                {
                    set.Add(input[i]);

                    if (set.Count == distinctCharacters)
                    {
                        return i + 1;
                    }
                } else
                {
                    while (input[startIdx] != input[i])
                    {
                        set.Remove(input[startIdx]);
                        ++startIdx;
                    }

                    ++startIdx;
                }
            }

            return -1;
        }
    }
}
