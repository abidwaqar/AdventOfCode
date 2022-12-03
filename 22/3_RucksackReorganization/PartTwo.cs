namespace AdventOfCode._22._3_RucksackReorganization
{
    internal static class PartTwo
    {
        private static readonly int LowercaseStartingPriority = 1;
        private static readonly int UppercaseStartingPriority = 27;

        // T = O(n * m) | S = O(m)
        public static int Solve()
        {
            int prioritiesSum = 0, groupSize = 3, i = 0;
            ISet<char> elf1 = new HashSet<char>(), elf2 = new HashSet<char>(), elf3 = new HashSet<char>();
            foreach (var input in File.ReadAllLines("../../../input.txt"))
            {
                var elf = (i % groupSize) switch
                {
                    0 => elf1,
                    1 => elf2,
                    2 => elf3,
                    _ => throw new Exception("Invalid input")
                };

                for (int j = 0; j < input.Length; ++j)
                {
                    elf.Add(input[j]);
                }

                if (i % groupSize == 2)
                {
                    elf1.IntersectWith(elf2);
                    elf1.IntersectWith(elf3);

                    prioritiesSum += getPriority(elf1.Single());

                    elf1.Clear();
                    elf2.Clear();
                    elf3.Clear();
                }

                ++i;
            }

            return prioritiesSum;
        }

        private static int getPriority(char chr)
        {
            return chr + (char.IsLower(chr) ? -97 + LowercaseStartingPriority : -65 + UppercaseStartingPriority);
        }
    }
}
