namespace AdventOfCode._22._15_BeaconExclusionZone;

internal static class PartTwo
{
    // T = O(4000000 * n) | S = O(n)
    // Where n is the count of sensors. This is the avg case, the reason the time complexity is not O(4000000 * 4000000 * n) is because
    // of this optimization "j = sensorAndItsClosestBeaconCoords.Item1.x + beaconToSensorDistance - Math.Abs(sensorAndItsClosestBeaconCoords.Item1.y - i);"
    public static long Solve()
    {
        string[] delimeters = new string[] { "Sensor at x=", ", y=", ": closest beacon is at x=", "", ", y=" };
        List<Tuple<Coordinates, Coordinates>> sensorsAndTheirClosestBeaconsCoords = new();
        foreach (string input in File.ReadAllLines("../../../input.txt"))
        {
            string[] processedInput = input.Split(delimeters, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            Coordinates sensorCoords = new(Convert.ToInt32(processedInput[0]), Convert.ToInt32(processedInput[1]));
            Coordinates beaconCoords = new(Convert.ToInt32(processedInput[2]), Convert.ToInt32(processedInput[3]));
            sensorsAndTheirClosestBeaconsCoords.Add(new(sensorCoords, beaconCoords));
        }

        for (int i = 0; i < 4000000; ++i)
        {
            for (int j = 0; j < 4000000; ++j)
            {
                Coordinates currCoords = new(j, i);
                bool distressSingleFound = true;
                foreach (Tuple<Coordinates, Coordinates> sensorAndItsClosestBeaconCoords in sensorsAndTheirClosestBeaconsCoords)
                {
                    int beaconToSensorDistance = sensorAndItsClosestBeaconCoords.Item1.ManhattanDistance(sensorAndItsClosestBeaconCoords.Item2);
                    if (currCoords == sensorAndItsClosestBeaconCoords.Item1 || 
                        currCoords == sensorAndItsClosestBeaconCoords.Item2 ||
                        beaconToSensorDistance >= sensorAndItsClosestBeaconCoords.Item1.ManhattanDistance(currCoords))
                    {
                        j = sensorAndItsClosestBeaconCoords.Item1.x + beaconToSensorDistance - Math.Abs(sensorAndItsClosestBeaconCoords.Item1.y - i);
                        distressSingleFound = false;
                        break;
                    }
                }

                if (distressSingleFound)
                {
                    return ((long)j * 4000000) + i;
                }
            }
        }
        

        return -1;
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

        public int ManhattanDistance(Coordinates coordinates)
        {
            return Math.Abs(this.x - coordinates.x) + Math.Abs(this.y - coordinates.y);
        }

        public static bool operator ==(Coordinates coords1, Coordinates coords2)
        {
            return coords1.x == coords2.x && coords1.y == coords2.y;
        }
        public static bool operator !=(Coordinates coords1, Coordinates coords2)
        {
            return !(coords1 == coords2);
        }
    }
}
