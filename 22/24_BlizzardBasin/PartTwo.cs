namespace AdventOfCode._22._24_BlizzardBasin;

internal static class PartTwo
{
    // T = O(h * w * m) | S = O(h * w)
    // Where h is heigth of valley, w is width of valley and m is (h + w) in best case scenerio and infinity in worst case scenerio.
    public static int Solve()
    {
        string[] inputArr = File.ReadAllLines("../../../Input/prod.txt");

        HashSet<Tuple<int, int, Direction>> blizzards = new();
        for (int i = 1; i < inputArr.Length - 1; ++i)
        {
            for (int j = 1; j < inputArr[i].Length - 1; ++j)
            {
                if (inputArr[i][j] != '.')
                {
                    blizzards.Add(new(i, j, (Direction)inputArr[i][j]));
                }
            }
        }

        int valleyLength = inputArr.Length;
        int valleyWidth = inputArr[0].Length;
        var startingPoint = new Tuple<int, int>(0, 1);
        var extractionPoint = new Tuple<int, int>(valleyLength - 1, valleyWidth - 2);

        return FewestMinutesToReachGoal(valleyLength, valleyWidth, ref blizzards, startingPoint, extractionPoint) + 
            FewestMinutesToReachGoal(valleyLength, valleyWidth, ref blizzards, extractionPoint, startingPoint) + 
            FewestMinutesToReachGoal(valleyLength, valleyWidth, ref blizzards, startingPoint, extractionPoint);
    }

    private enum Direction
    {
        RIGHT = '>',
        DOWN = 'v',
        LEFT = '<',
        UP = '^',
    }

    private static int FewestMinutesToReachGoal(int valleyLength, int valleyWidth, ref HashSet<Tuple<int, int, Direction>> blizzards, Tuple<int, int> startingPoint, Tuple<int, int> extractionPoint)
    {
        int time = 0;
        var currentSet = new HashSet<Tuple<int, int>>
        {
            startingPoint
        };
        while (currentSet.Count != 0)
        {
            var nextSet = new HashSet<Tuple<int, int>>();
            foreach (var expeditionPoint in currentSet)
            {
                if (expeditionPoint.Equals(extractionPoint))
                {
                    return time;
                }

                var newExpeditionPoint = expeditionPoint;
                if (ValidPoint(newExpeditionPoint, valleyLength, valleyWidth, blizzards, startingPoint, extractionPoint))
                {
                    nextSet.Add(newExpeditionPoint);
                }

                newExpeditionPoint = new Tuple<int, int>(expeditionPoint.Item1 - 1, expeditionPoint.Item2);
                if (ValidPoint(newExpeditionPoint, valleyLength, valleyWidth, blizzards, startingPoint, extractionPoint))
                {
                    nextSet.Add(newExpeditionPoint);
                }

                newExpeditionPoint = new Tuple<int, int>(expeditionPoint.Item1 + 1, expeditionPoint.Item2);
                if (ValidPoint(newExpeditionPoint, valleyLength, valleyWidth, blizzards, startingPoint, extractionPoint))
                {
                    nextSet.Add(newExpeditionPoint);
                }

                newExpeditionPoint = new Tuple<int, int>(expeditionPoint.Item1, expeditionPoint.Item2 - 1);
                if (ValidPoint(newExpeditionPoint, valleyLength, valleyWidth, blizzards, startingPoint, extractionPoint))
                {
                    nextSet.Add(newExpeditionPoint);
                }

                newExpeditionPoint = new Tuple<int, int>(expeditionPoint.Item1, expeditionPoint.Item2 + 1);
                if (ValidPoint(newExpeditionPoint, valleyLength, valleyWidth, blizzards, startingPoint, extractionPoint))
                {
                    nextSet.Add(newExpeditionPoint);
                }
            }

            var newBlizzards = new HashSet<Tuple<int, int, Direction>>();
            foreach (var blizzard in blizzards)
            {
                int newX = blizzard.Item1, newY = blizzard.Item2;
                switch (blizzard.Item3)
                {
                    case Direction.UP:
                        if (--newX == 0) newX = valleyLength - 2;
                        break;
                    case Direction.DOWN:
                        if (++newX == valleyLength - 1) newX = 1;
                        break;
                    case Direction.LEFT:
                        if (--newY == 0) newY = valleyWidth - 2;
                        break;
                    case Direction.RIGHT:
                        if (++newY == valleyWidth - 1) newY = 1;
                        break;
                    default:
                        throw new Exception("Direction not handled");
                }

                newBlizzards.Add(new(newX, newY, blizzard.Item3));
            }

            currentSet = nextSet;
            blizzards = newBlizzards;
            ++time;
        }

        throw new Exception("Logical error");
    }

    private static bool ValidPoint(Tuple<int, int> point, int valleyLength, int valleyWidth, HashSet<Tuple<int, int, Direction>> blizzards, Tuple<int, int> startingPoint, Tuple<int, int> extractionPoint)
    {
        if (point.Equals(startingPoint) || point.Equals(extractionPoint))
        {
            return true;
        }

        if (point.Item1 <= 0 || point.Item1 >= valleyLength - 1 || point.Item2 <= 0 || point.Item2 >= valleyWidth - 1)
        {
            return false;
        }

        if (blizzards.Contains(new(point.Item1 - 1 != 0 ? point.Item1 - 1 : valleyLength - 2, point.Item2, Direction.DOWN)) ||
            blizzards.Contains(new(point.Item1 + 1 != valleyLength - 1 ? point.Item1 + 1 : 1, point.Item2, Direction.UP)) ||
            blizzards.Contains(new(point.Item1, point.Item2 - 1 != 0 ? point.Item2 - 1 : valleyWidth - 2, Direction.RIGHT)) ||
            blizzards.Contains(new(point.Item1, point.Item2 + 1 != valleyWidth - 1 ? point.Item2 + 1 : 1, Direction.LEFT)))
        {
            return false;
        }

        return true;
    }
}
