namespace AdventOfCode._22._18_BoilingBoulders;

internal static class PartTwo
{
    // T = O((maxX - minX) * (maxY - minY) * (maxZ - minZ)) | S = O((maxX - minX) * (maxY - minY) * (maxZ - minZ))
    public static int Solve()
    {
        HashSet<Tuple<int, int, int>> droplet = new();

        int surfaceArea = 0, minX = int.MaxValue, maxX = int.MinValue, minY = int.MaxValue, maxY = int.MinValue, minZ = int.MaxValue, maxZ = int.MinValue;
        foreach (string input in File.ReadAllLines("../../../Input/prod.txt"))
        {
            string[] inputArr = input.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            int x = Convert.ToInt32(inputArr[0]), y = Convert.ToInt32(inputArr[1]), z = Convert.ToInt32(inputArr[2]);
            
            droplet.Add(new(x, y, z));

            minX = Math.Min(minX, x);
            maxX = Math.Max(maxX, x);

            minY = Math.Min(minY, y);
            maxY = Math.Max(maxY, y);

            minZ = Math.Min(minZ, z);
            maxZ = Math.Max(maxZ, z);
        }

        HashSet<Tuple<int, int, int>> water = new();
        ModelWater(minX - 1, minY, minZ, water, droplet, minX - 1, maxX + 1, minY - 1, maxY + 1, minZ - 1, maxZ + 1);

        foreach (Tuple<int, int, int> point in droplet)
        {
            surfaceArea += water.Contains(new(point.Item1 + 1, point.Item2, point.Item3)) ? 1 : 0;
            surfaceArea += water.Contains(new(point.Item1 - 1, point.Item2, point.Item3)) ? 1 : 0;

            surfaceArea += water.Contains(new(point.Item1, point.Item2 + 1, point.Item3)) ? 1 : 0;
            surfaceArea += water.Contains(new(point.Item1, point.Item2 - 1, point.Item3)) ? 1 : 0;

            surfaceArea += water.Contains(new(point.Item1, point.Item2, point.Item3 + 1)) ? 1 : 0;
            surfaceArea += water.Contains(new(point.Item1, point.Item2, point.Item3 - 1)) ? 1 : 0;
        }

        return surfaceArea;
    }

    private static void ModelWater(int x, int y, int z, HashSet<Tuple<int, int, int>> water, HashSet<Tuple<int, int, int>> droplet, int minX, int maxX, int minY, int maxY, int minZ, int maxZ)
    {
        Tuple<int, int, int> point = new(x, y, z);
        if (x < minX || x > maxX || y < minY || y > maxX || z < minZ || z > maxZ || water.Contains(point) || droplet.Contains(point))
        {
            return;
        }

        water.Add(point);

        ModelWater(x + 1, y, z, water, droplet, minX, maxX, minY, maxY, minZ, maxZ);
        ModelWater(x - 1, y, z, water, droplet, minX, maxX, minY, maxY, minZ, maxZ);

        ModelWater(x, y + 1, z, water, droplet, minX, maxX, minY, maxY, minZ, maxZ);
        ModelWater(x, y - 1, z, water, droplet, minX, maxX, minY, maxY, minZ, maxZ);

        ModelWater(x, y, z + 1, water, droplet, minX, maxX, minY, maxY, minZ, maxZ);
        ModelWater(x, y, z - 1, water, droplet, minX, maxX, minY, maxY, minZ, maxZ);
    }
}
