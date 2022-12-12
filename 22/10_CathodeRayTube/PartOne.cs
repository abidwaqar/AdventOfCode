namespace AdventOfCode._22._10_CathodeRayTube;

internal static class PartOne
{
    // T = O(m * n) | S = O(1)
    // Where m is operations count and n is the cycles it takes to do the longest operation.
    public static int Solve()
    {
        IDictionary<string, int> operationsToRequiredCycles = new Dictionary<string, int>
        {
            {"noop", 1 },
            {"addx", 2 }
        };
        int cycle = 1, totalSignalStrength = 0, X = 1;
        foreach (string input in File.ReadAllLines("../../../input.txt"))
        {
            string[] inputArr = input.Split(' ');
            for (int i = operationsToRequiredCycles[inputArr[0]] - 1; i >= 0; --i)
            {
                if (cycle % 20 == 0 && cycle % 40 != 0)
                {
                    totalSignalStrength += cycle * X;
                }

                ++cycle;
            }

            switch(inputArr[0]) 
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

        return totalSignalStrength;
    }
}
