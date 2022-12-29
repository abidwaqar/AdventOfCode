using System.Runtime.Intrinsics.Arm;

namespace AdventOfCode._22._16_ProboscideaVolcanium;

internal static class PartTwo
{
    // T = O(2^s * n * t * p) | S = O(2^s * n * t * p)
    // Where s is the number of valves that do not have pressure = 0, n is the total number of valves,
    // t is the total time which in this case is 26 and p is the number of people which in our case is 2
    public static int Solve()
    {
        string[] delimeters = new string[] { "Valve ", " has flow rate=", "; tunnels lead to valves ", "; tunnel leads to valve ", ", " };
        IDictionary<string, Valve> valveGraph = new Dictionary<string, Valve>();
        int index = 0;
        foreach (string input in File.ReadAllLines("../../../Input/prod.txt"))
        {
            string[] processedInput = input.Split(delimeters, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

            List<string> to = new();
            for (int i = 2; i < processedInput.Length; i++)
            {
                to.Add(processedInput[i]);
            }

            string valveName = processedInput[0];
            int valvePressure = Convert.ToInt32(processedInput[1]);
            valveGraph[valveName] = new(valveName, valvePressure, false, to, valvePressure == 0 ? 0 : index++);
        }

        return MaxPressure("AA", valveGraph, 26, 0, 2, new Dictionary<string, int>());
    }

    private class Valve
    {
        public string Name;
        public int PressureRate;
        public bool Opened;
        public List<string> AdjacentValves;
        public int Position;

        public Valve(string name, int pressureRate, bool opened, List<string> adjacentValves, int position)
        {
            this.Name = name;
            this.PressureRate = pressureRate;
            this.Opened = opened;
            this.AdjacentValves = adjacentValves;
            this.Position = position;
        }
    }

    private static int MaxPressure(string currValve, IDictionary<string, Valve> valveGraph, int timeRemaining, ulong state, int players, Dictionary<string, int> dp)
    {
        if (timeRemaining <= 0)
        {
            return players == 1 ? 0 : MaxPressure("AA", valveGraph, 26, state, players - 1, dp);
        }

        string key = "" + state + "|" + currValve + "|" + timeRemaining + "|" + players;
        if (dp.ContainsKey(key))
        {
            return dp[key];
        }

        int maxPressure = 0;
        if (!valveGraph[currValve].Opened && valveGraph[currValve].PressureRate != 0)
        {
            valveGraph[currValve].Opened = true;
            ulong newState = state | (1UL << valveGraph[currValve].Position);
            maxPressure = (valveGraph[currValve].PressureRate * (timeRemaining - 1)) + MaxPressure(currValve, valveGraph, timeRemaining - 1, newState, players, dp);
            valveGraph[currValve].Opened = false;
        }

        foreach (string adjacentValve in valveGraph[currValve].AdjacentValves)
        {
            maxPressure = Math.Max(maxPressure, MaxPressure(adjacentValve, valveGraph, timeRemaining - 1, state, players, dp));
        }

        return dp[key] = maxPressure;
    }
}
