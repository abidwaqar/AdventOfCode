namespace AdventOfCode._22._9_RopeBridge;

internal static class PartTwo
{
    // T = O(n * m) | S = O(n * m)
    // Where n is the number of direction inputs and m is the longest distance input
    public static int Solve()
    {
        ISet<string> tailVisitedCells = new HashSet<string>();
        int knotsCount = 10;
        Coordinate[] knotsCoords = new Coordinate[knotsCount];
        for (int i = 0; i < knotsCount; ++i)
        {
            knotsCoords[i] = new Coordinate();
        }

        foreach (var input in File.ReadAllLines("../../../input.txt"))
        {
            string[] inputArr = input.Split(' ');
            char direction = Convert.ToChar(inputArr[0]);
            int distance = Convert.ToInt32(inputArr[1]);

            for (int i = 0; i < distance; ++i)
            {
                _ = direction switch
                {
                    'U' => ++knotsCoords[0].y,
                    'D' => --knotsCoords[0].y,
                    'L' => --knotsCoords[0].x,
                    'R' => ++knotsCoords[0].x,
                    _ => throw new Exception("Invalid direction input"),
                };

                for (int j = 1; j < knotsCount; ++j)
                {
                    knotsCoords[j].Follow(knotsCoords[j - 1]);
                }

                tailVisitedCells.Add($"{knotsCoords[knotsCount - 1].x}|{knotsCoords[knotsCount - 1].y}");
            }
        }

        return tailVisitedCells.Count;
    }

    private class Coordinate
    {
        public int x;
        public int y;

        public Coordinate()
        {
            this.x = 0;
            this.y = 0;
        }

        public void Follow(Coordinate coords)
        {
            if (this.x == coords.x)
            {
                if (Math.Abs(this.y - coords.y) <= 1)
                {
                    return;
                }

                this.y = this.y < coords.y ? this.y + 1 : this.y - 1;
            }
            else if (this.y == coords.y)
            {
                if (Math.Abs(this.x - coords.x) <= 1)
                {
                    return;
                }

                this.x = this.x < coords.x ? this.x + 1 : this.x - 1;
            }
            else
            {
                if (Math.Sqrt(Math.Pow(this.x - coords.x, 2) + Math.Pow(this.y - coords.y, 2)) < 2)
                {
                    return;
                }

                this.x = this.x < coords.x ? this.x + 1 : this.x - 1;
                this.y = this.y < coords.y ? this.y + 1 : this.y - 1;
            }
        }
    }
}