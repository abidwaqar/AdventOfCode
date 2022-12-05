using System.Text;

namespace AdventOfCode._22._5_SupplyStacks
{
    internal static class PartTwo
    {
        private static IList<List<char>> cratesWithStacks = new List<List<char>>
        {
            new List<char>(new char[] { 'B', 'S', 'V', 'Z', 'G', 'P', 'W' }),
            new List<char>(new char[] { 'J', 'V', 'B', 'C', 'Z', 'F' }),
            new List<char>(new char[] { 'V', 'L', 'M', 'H', 'N', 'Z', 'D', 'C' }),
            new List<char>(new char[] { 'L', 'D', 'M', 'Z', 'P', 'F', 'J', 'B' }),
            new List<char>(new char[] { 'V', 'F', 'C', 'G', 'J', 'B', 'Q', 'H' }),
            new List<char>(new char[] { 'G', 'F', 'Q', 'T', 'S', 'L', 'B' }),
            new List<char>(new char[] { 'L', 'G', 'C', 'Z', 'V' }),
            new List<char>(new char[] { 'N', 'L', 'G' }),
            new List<char>(new char[] { 'J', 'F', 'H', 'C' })
        };

        // T = O(nm) | S = O(1)
        // Where n is the number of lines and m is the total items in all stacks
        public static string Solve()
        {
            bool realInputStarted = false;
            foreach (var input in File.ReadAllLines("../../../input.txt"))
            {
                if (!realInputStarted)
                {
                    if (input == string.Empty)
                    {
                        realInputStarted = true;
                    }

                    continue;
                }

                var details = input.Split(new string[] { "move ", " from ", " to " }, StringSplitOptions.RemoveEmptyEntries);
                var move = Convert.ToInt32(details[0]);
                var from = Convert.ToInt32(details[1]) - 1;
                var to = Convert.ToInt32(details[2]) - 1;

                cratesWithStacks[to].AddRange(cratesWithStacks[from].GetRange(cratesWithStacks[from].Count - move, move));
                cratesWithStacks[from].RemoveRange(cratesWithStacks[from].Count - move, move);
            }

            StringBuilder result = new StringBuilder();
            foreach (var stack in cratesWithStacks)
            {
                result.Append(stack.Last());
            }

            return result.ToString();
        }
    }
}
