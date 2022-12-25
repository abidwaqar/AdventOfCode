namespace AdventOfCode._22._16_ProboscideaVolcanium;

internal static class PartOne
{
    // T = O(n) | S = O(n)
    public static int Solve()
    {
        string[] delimeters = new string[] { "Valve ", " has flow rate=", "; tunnels lead to valves ", "; tunnel leads to valve ", ", " };
        IDictionary<string, Valve> valveGraph = new Dictionary<string, Valve>();
        foreach (string input in File.ReadAllLines("../../../input.txt"))
        {
            string[] processedInput = input.Split(delimeters, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

            List<string> to = new();
            for (int i = 2; i < processedInput.Length; i++)
            {
                to.Add(processedInput[i]);
            }

            Valve valve = new Valve(processedInput[0], Convert.ToInt32(processedInput[1]), false, to);

            valveGraph[valve.Name] = valve;
        }

        return MaxPressure("AA", valveGraph, 30);
    }

    private class Valve
    {
        public string Name;
        public int PressureRate;
        public bool Opened;
        public List<string> AdjacentValves;

        public Valve(string name, int pressureRate, bool opened, List<string> adjacentValves)
        {
            this.Name = name;
            this.PressureRate = pressureRate;
            this.Opened = opened;
            this.AdjacentValves = adjacentValves;
        }
    }

    private static int MaxPressure(string currValve, IDictionary<string, Valve> valveGraph, int timeRemaining)
    {
        if (timeRemaining <= 0)
        {
            return 0;
        }

        int currValvePressureResult = 0;
        if (!valveGraph[currValve].Opened && valveGraph[currValve].PressureRate != 0 && timeRemaining >= 2)
        {
            --timeRemaining;
            currValvePressureResult = valveGraph[currValve].PressureRate * timeRemaining;
            valveGraph[currValve].Opened = true;
        }

        int maxAdjacentPressure = 0;
        foreach (string adjacentValve in valveGraph[currValve].AdjacentValves)
        {
            maxAdjacentPressure = Math.Max(maxAdjacentPressure, MaxPressure(adjacentValve, valveGraph, timeRemaining - 1));
        }

        return currValvePressureResult + maxAdjacentPressure;
    }
}
