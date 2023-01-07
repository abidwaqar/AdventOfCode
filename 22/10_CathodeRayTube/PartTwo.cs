namespace AdventOfCode._22._10_CathodeRayTube;

internal static class PartTwo
{
    // T = O(m * n) | S = O(1)
    // Where m is operations count and n is the cycles it takes to do the longest operation.
    public static void Solve()
    {
        IDictionary<string, int> operationsToRequiredCycles = new Dictionary<string, int>
        {
            {"noop", 1 },
            {"addx", 2 }
        };
        int cycle = 0, X = 1;
        foreach (string input in File.ReadAllLines("../../../Input/prod.txt"))
        {
            string[] inputArr = input.Split(' ');
            for (int i = operationsToRequiredCycles[inputArr[0]] - 1; i >= 0; --i)
            {
                int crtPosition = cycle % 40;
                Console.Write((Math.Abs(crtPosition - X) <= 1) ? "#" : ".");
                ++cycle;
                if (cycle % 40 == 0) Console.WriteLine();
            }

            switch (inputArr[0])
            {
                case "noop":
                    break;
                case "addx":
                    X += Convert.ToInt32(inputArr[1]);
                    break;
                default:
                    throw new Exception("Invalid input");
            }
        }
    }
}
