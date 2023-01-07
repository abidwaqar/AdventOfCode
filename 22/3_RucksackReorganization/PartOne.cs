namespace AdventOfCode._22._3_RucksackReorganization
{
    internal static class PartOne
    {
        private static readonly int LowercaseStartingPriority = 1;
        private static readonly int UppercaseStartingPriority = 27;

        // T = O(n * m) | S = O(m)
        public static int Solve()
        {
            int prioritiesSum = 0;
            var seenItems = new HashSet<char>();
            foreach (var input in File.ReadAllLines("../../../Input/prod.txt"))
            {
                for (int i = 0; i < input.Length / 2; ++i)
                {
                    seenItems.Add(input[i]);
                }

                for (int i = input.Length / 2; i < input.Length; ++i)
                {
                    if (seenItems.Contains(input[i]))
                    {
                        prioritiesSum += getPriority(input[i]);
                        break;
                    }
                }

                seenItems.Clear();
            }

            return prioritiesSum;
        }

        private static int getPriority(char chr)
        {
            return chr + (char.IsLower(chr) ? -97 + LowercaseStartingPriority : -65 + UppercaseStartingPriority);
        }
    }
}
