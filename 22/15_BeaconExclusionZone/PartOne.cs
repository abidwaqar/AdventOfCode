namespace AdventOfCode._22._15_BeaconExclusionZone;

internal static class PartOne
{
    // T = O(n * m) | S = O(n)
    // Where n is the count of sensors and m is Math.Abs(minSensorX - maxDistance) + Math.Abs(maxSensorX + maxDistance)
    public static int Solve()
    {
        int y = 10;

        string[] delimeters = new string[] { "Sensor at x=", ", y=", ": closest beacon is at x=", "", ", y=" };
        int maxDistance = 0;
        int minSensorX = int.MaxValue;
        int maxSensorX = int.MinValue;
        List<Tuple<Coordinates, Coordinates>> sensorsAndTheirClosestBeaconsCoords = new();
        foreach (string input in File.ReadAllLines("../../../Input/prod.txt"))
        {
            string[] processedInput = input.Split(delimeters, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            Coordinates sensorCoords = new(Convert.ToInt32(processedInput[0]), Convert.ToInt32(processedInput[1]));
            Coordinates beaconCoords = new(Convert.ToInt32(processedInput[2]), Convert.ToInt32(processedInput[3]));
            sensorsAndTheirClosestBeaconsCoords.Add(new(sensorCoords, beaconCoords));

            maxDistance = Math.Max(maxDistance, sensorCoords.ManhattanDistance(beaconCoords));
            minSensorX = Math.Min(minSensorX, sensorCoords.x);
            maxSensorX = Math.Max(maxSensorX, sensorCoords.x);
        }

        int result = 0;
        for (int i = minSensorX - maxDistance; i <= maxSensorX + maxDistance; ++i)
        {
            Coordinates currCoords = new(i, y);
            bool currCoordsCannotContainABeacon = false;
            bool currCoordsContainASensorOrBeacon = false;
            foreach (Tuple<Coordinates, Coordinates> sensorAndItsClosestBeaconCoords in sensorsAndTheirClosestBeaconsCoords)
            {
                if (currCoords == sensorAndItsClosestBeaconCoords.Item1 || currCoords == sensorAndItsClosestBeaconCoords.Item2)
                {
                    currCoordsContainASensorOrBeacon = true;
                    break;
                }

                if (sensorAndItsClosestBeaconCoords.Item1.ManhattanDistance(sensorAndItsClosestBeaconCoords.Item2) >= sensorAndItsClosestBeaconCoords.Item1.ManhattanDistance(currCoords))
                {
                    currCoordsCannotContainABeacon = true;
                }
            }

            if (!currCoordsContainASensorOrBeacon && currCoordsCannotContainABeacon)
            {
                ++result;
            }
        }

        return result;
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
