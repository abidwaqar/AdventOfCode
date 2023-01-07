namespace AdventOfCode._22._14_RegolithReservoir;

internal static class PartOne
{
    // T = O(h * w * h) | S = O(h * w)
    // Where h and w are height and width of the minimum grid size required to run the simulation.
    public static int Solve()
    {
        Coordinates gridStartCoords = new Coordinates(int.MaxValue, 0);
        Coordinates gridEndCoords = new Coordinates(int.MinValue, int.MinValue);
        foreach (string input in File.ReadAllLines("../../../Input/prod.txt"))
        {
            string[] allCoordinates = input.Split("->", StringSplitOptions.TrimEntries);

            for (int i = 0; i < allCoordinates.Length; ++i)
            {
                Coordinates currCoordinates = new Coordinates(allCoordinates[i]);

                gridStartCoords.x = Math.Min(gridStartCoords.x, currCoordinates.x);

                gridEndCoords.x = Math.Max(gridEndCoords.x, currCoordinates.x);
                gridEndCoords.y = Math.Max(gridEndCoords.y, currCoordinates.y);
            }
        }

        char[,] grid = new char[gridEndCoords.y - gridStartCoords.y + 1, gridEndCoords.x - gridStartCoords.x + 1];
        foreach (string input in File.ReadAllLines("../../../Input/prod.txt"))
        {
            string[] allCoordinates = input.Split("->", StringSplitOptions.TrimEntries);
            Coordinates prevCoordinates = new Coordinates(allCoordinates[0]) - gridStartCoords;

            for (int i = 1; i < allCoordinates.Length; ++i)
            {
                Coordinates currCoordinates = new Coordinates(allCoordinates[i]) - gridStartCoords;

                if (prevCoordinates.x == currCoordinates.x)
                {
                    while (prevCoordinates.y != currCoordinates.y)
                    {
                        grid[prevCoordinates.y, prevCoordinates.x] = '#';
                        prevCoordinates.y = (prevCoordinates.y < currCoordinates.y) ? prevCoordinates.y + 1 : prevCoordinates.y - 1;
                    }
                } else if (prevCoordinates.y == currCoordinates.y)
                {
                    while (prevCoordinates.x != currCoordinates.x)
                    {
                        grid[prevCoordinates.y, prevCoordinates.x] = '#';
                        prevCoordinates.x = (prevCoordinates.x < currCoordinates.x) ? prevCoordinates.x + 1 : prevCoordinates.x - 1;
                    }
                } else
                {
                    throw new Exception("Moving diagonally is not supported");
                }
            }

            grid[prevCoordinates.y, prevCoordinates.x] = '#';
        }

        int settledSand = 0;
        Coordinates sandSourceCoords = new Coordinates(500 - gridStartCoords.x, 0);
        bool sandDroppedIntoAbyss = false;
        while (!sandDroppedIntoAbyss)
        {
            Coordinates sandCoordinates = new Coordinates(sandSourceCoords);
            while (true)
            {
                if (grid.GetLength(0) == sandCoordinates.y + 1)
                {
                    sandDroppedIntoAbyss = true;
                    break;
                } else if (grid[sandCoordinates.y + 1, sandCoordinates.x] == '\0')
                {
                    ++sandCoordinates.y;
                    continue;
                }

                if (-1 == sandCoordinates.x - 1)
                {
                    sandDroppedIntoAbyss = true;
                    break;
                }
                else if (grid[sandCoordinates.y + 1, sandCoordinates.x - 1] == '\0')
                {
                    --sandCoordinates.x;
                    ++sandCoordinates.y;
                    continue;
                }

                if (grid.GetLength(1) == sandCoordinates.x + 1)
                {
                    sandDroppedIntoAbyss = true;
                    break;
                }
                else if (grid[sandCoordinates.y + 1, sandCoordinates.x + 1] == '\0')
                {
                    ++sandCoordinates.x;
                    ++sandCoordinates.y;
                    continue;
                }

                grid[sandCoordinates.y, sandCoordinates.x] = 'o';
                ++settledSand;
                break;
            }
        }

        return settledSand;
    }

    private class Coordinates
    {
        public int x;
        public int y;

        public Coordinates(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public Coordinates(string coordinates)
        {
            string[] coordicatesArr = coordinates.Split(',');

            this.x = Convert.ToInt32(coordicatesArr[0]);
            this.y = Convert.ToInt32(coordicatesArr[1]);
        }

        public Coordinates(Coordinates coordinates)
        {
            this.x = coordinates.x;
            this.y = coordinates.y;
        }

        public static Coordinates operator -(Coordinates coordinates1, Coordinates coordinates2)
        {
            return new Coordinates(coordinates1.x - coordinates2.x, coordinates1.y - coordinates2.y);
        }
    }
}
