namespace AdventOfCode._22._19_NotEnoughMinerals;

internal static class PartOne
{
    // T = O(n * 5^24) | S = O(5^24)
    // Where n is the count of blueprint. These are the worst case complexities, on average a lot less time and space will be used because of dynamic programming.
    public static int Solve()
    {
        int qualityLevel = 0;
        string[] separators = new string[] { "Blueprint ", ": Each ore robot costs ", " ore. Each clay robot costs ", " ore. Each obsidian robot costs ", " ore and ", " clay. Each geode robot costs ", " ore and ", " obsidian." };
        foreach (string input in File.ReadAllLines("../../../Input/prod.txt"))
        {
            string[] inputArr = input.Split(separators, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            
            int blueprintId = Convert.ToInt32(inputArr[0]);
            int oreRobotCost = Convert.ToInt32(inputArr[1]);
            int clayRobotCost = Convert.ToInt32(inputArr[2]);
            Tuple<int, int> obsidianRobotCost = new(Convert.ToInt32(inputArr[3]), Convert.ToInt32(inputArr[4]));
            Tuple<int, int> geodeRobotCost = new(Convert.ToInt32(inputArr[5]), Convert.ToInt32(inputArr[6]));

            qualityLevel += blueprintId * MaxGeodesThatCanBeOpened(oreRobotCost, clayRobotCost, obsidianRobotCost, geodeRobotCost, 24, 1, 0, 0, 0, 0, 0, 0, new());
        }

        return qualityLevel;
    }

    private static int MaxGeodesThatCanBeOpened(
        int oreRobotCost, 
        int clayRobotCost, 
        Tuple<int, int> obsidianRobotCost, 
        Tuple<int, int> geodeRobotCost, 
        int remainingTime, 
        int oreCrackingRobotCount, 
        int clayCrackingRobotCount, 
        int obsidianCrackingRobotCount, 
        int geodeCrackingRobotCount, 
        int oreCount, 
        int clayCount, 
        int obsidianCount,
        Dictionary<string, int> dp)
    {
        if (remainingTime <= 0)
        {
            return 0;
        }

        string key = $"{remainingTime}|{oreCrackingRobotCount}|{clayCrackingRobotCount}|{obsidianCrackingRobotCount}|{geodeCrackingRobotCount}|{oreCount}|{clayCount}|{obsidianCount}";
        if (dp.ContainsKey(key))
        {
            return dp[key];
        }

        int newOreCount = oreCount + oreCrackingRobotCount, newClayCount = clayCount + clayCrackingRobotCount, newObsidianCount = obsidianCount + obsidianCrackingRobotCount;

        int maxGeodes = MaxGeodesThatCanBeOpened(oreRobotCost, clayRobotCost, obsidianRobotCost, geodeRobotCost, remainingTime - 1, oreCrackingRobotCount, clayCrackingRobotCount, obsidianCrackingRobotCount, geodeCrackingRobotCount, newOreCount, newClayCount, newObsidianCount, dp);

        if (oreRobotCost <= oreCount)
        {
            maxGeodes = Math.Max(maxGeodes, MaxGeodesThatCanBeOpened(oreRobotCost, clayRobotCost, obsidianRobotCost, geodeRobotCost, remainingTime - 1, oreCrackingRobotCount + 1, clayCrackingRobotCount, obsidianCrackingRobotCount, geodeCrackingRobotCount, newOreCount - oreRobotCost, newClayCount, newObsidianCount, dp));
        }

        if (clayRobotCost <= oreCount)
        {
            maxGeodes = Math.Max(maxGeodes, MaxGeodesThatCanBeOpened(oreRobotCost, clayRobotCost, obsidianRobotCost, geodeRobotCost, remainingTime - 1, oreCrackingRobotCount, clayCrackingRobotCount + 1, obsidianCrackingRobotCount, geodeCrackingRobotCount, newOreCount - clayRobotCost, newClayCount, newObsidianCount, dp));
        }

        if (obsidianRobotCost.Item1 <= oreCount && obsidianRobotCost.Item2 <= clayCount)
        {
            maxGeodes = Math.Max(maxGeodes, MaxGeodesThatCanBeOpened(oreRobotCost, clayRobotCost, obsidianRobotCost, geodeRobotCost, remainingTime - 1, oreCrackingRobotCount, clayCrackingRobotCount, obsidianCrackingRobotCount + 1, geodeCrackingRobotCount, newOreCount - obsidianRobotCost.Item1, newClayCount - obsidianRobotCost.Item2, newObsidianCount, dp));
        }

        if (geodeRobotCost.Item1 <= oreCount && geodeRobotCost.Item2 <= obsidianCount)
        {
            maxGeodes = Math.Max(maxGeodes, MaxGeodesThatCanBeOpened(oreRobotCost, clayRobotCost, obsidianRobotCost, geodeRobotCost, remainingTime - 1, oreCrackingRobotCount, clayCrackingRobotCount, obsidianCrackingRobotCount, geodeCrackingRobotCount + 1, newOreCount - geodeRobotCost.Item1, newClayCount, newObsidianCount - geodeRobotCost.Item2, dp));
        }

        return dp[key] = maxGeodes + geodeCrackingRobotCount;
    }
}
