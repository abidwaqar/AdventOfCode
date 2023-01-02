namespace AdventOfCode._22._18_BoilingBoulders;

internal static class PartOne
{
    // T = O(n) | S = O(n)
    // Where n is the count of points in droplet
    public static int Solve()
    {
        HashSet<Tuple<int, int, int>> droplet = new();

        int surfaceArea = 0;
        foreach (string input in File.ReadAllLines("../../../Input/prod.txt"))
        {
            string[] inputArr = input.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            droplet.Add(new(Convert.ToInt32(inputArr[0]), Convert.ToInt32(inputArr[1]), Convert.ToInt32(inputArr[2])));
        }

        foreach (Tuple<int, int, int> point in droplet)
        {
            surfaceArea += droplet.Contains(new(point.Item1 + 1, point.Item2, point.Item3)) ? 0 : 1;
            surfaceArea += droplet.Contains(new(point.Item1 - 1, point.Item2, point.Item3)) ? 0 : 1;

            surfaceArea += droplet.Contains(new(point.Item1, point.Item2 + 1, point.Item3)) ? 0 : 1;
            surfaceArea += droplet.Contains(new(point.Item1, point.Item2 - 1, point.Item3)) ? 0 : 1;

            surfaceArea += droplet.Contains(new(point.Item1, point.Item2, point.Item3 + 1)) ? 0 : 1;
            surfaceArea += droplet.Contains(new(point.Item1, point.Item2, point.Item3 - 1)) ? 0 : 1;
        }

        return surfaceArea;
    }
}
