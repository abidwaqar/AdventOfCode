namespace AdventOfCode._22._22_MonkeyMap;

internal static class PartOne
{
    // T = O(n) | S = O(h * w)
    // Where n is the sum of all the number of tiles to move in the instruction, h is the height of the map and w is the width of the map.
    public static int Solve()
    {
        List<List<char>> map = new();
        int rowIdx = -1, colIdx = -1;
        string[] inputArr = File.ReadAllLines("../../../Input/prod.txt");
        for (int i = 0; !string.IsNullOrEmpty(inputArr[i]); ++i)
        {
            List<char> row = new();
            for (int j = 0; j < inputArr[i].Length; ++j)
            {
                char character = inputArr[i][j];

                row.Add(character);
                if (character == '.' && rowIdx == -1 && colIdx == -1)
                {
                    rowIdx = i;
                    colIdx = j;
                }
            }

            map.Add(row);
        }

        string instructions = inputArr[inputArr.Length - 1];

        Direction currDirection = Direction.RIGHT;
        int steps = 0;
        foreach (char character in instructions)
        {
            if (char.IsLetter(character))
            {
                Move(ref rowIdx, ref colIdx, currDirection, steps, map);
                currDirection = GetUpdatedDirection(currDirection, character == 'R' ? Direction.RIGHT : Direction.LEFT);
                steps = 0;
                continue;
            }

            steps = steps * 10 + (character - '0');
        }

        Move(ref rowIdx, ref colIdx, currDirection, steps, map);

        return ((rowIdx + 1) * 1000) + ((colIdx + 1) * 4) + (int)currDirection;
    }

    private enum Direction
    {
        RIGHT = 0,
        DOWN = 1,
        LEFT = 2,
        UP = 3
    }

    private static Direction GetUpdatedDirection(Direction currDirection, Direction turn)
    {
        return currDirection switch
        {
            Direction.RIGHT => turn == Direction.RIGHT ? Direction.DOWN : Direction.UP,
            Direction.DOWN => turn == Direction.RIGHT ? Direction.LEFT : Direction.RIGHT,
            Direction.LEFT => turn == Direction.RIGHT ? Direction.UP : Direction.DOWN,
            Direction.UP => turn == Direction.RIGHT ? Direction.RIGHT : Direction.LEFT,
            _ => throw new Exception("Direction not handled")
        }; ;
    }

    private static void Move(ref int rowIdx, ref int colIdx, Direction currDirection, int steps, List<List<char>> map)
    {
        for (int i = 0; i < steps; ++i)
        {
            int oldColIdx = colIdx, oldRowIdx = rowIdx;
            switch (currDirection)
            {
                case Direction.RIGHT:
                    colIdx = GetNextColumnIndex(rowIdx, colIdx, map);
                    break;
                case Direction.DOWN:
                    rowIdx = GetNextRowIndex(rowIdx, colIdx, map);
                    break;
                case Direction.LEFT:
                    colIdx = GetPrevColumnIndex(rowIdx, colIdx, map);
                    break;
                case Direction.UP:
                    rowIdx = GetPrevRowIndex(rowIdx, colIdx, map);
                    break;
                default:
                    throw new Exception("Direction not handled");
            }

            if (oldColIdx == colIdx && oldRowIdx == rowIdx)
            {
                break;
            }
        }
    }

    private static int GetNextColumnIndex(int rowIdx, int colIdx, List<List<char>> map)
    {
        for (int i = colIdx + 1; i < colIdx + 1 + map[rowIdx].Count; ++i)
        {
            int index = i % map[rowIdx].Count;

            if (map[rowIdx][index] == '.')
            {
                return index;
            }
            else if (map[rowIdx][index] == '#')
            {
                return colIdx;
            }
        }

        throw new Exception("Logical error");
    }

    private static int GetPrevColumnIndex(int rowIdx, int colIdx, List<List<char>> map)
    {
        for (int i = colIdx - 1; i >= colIdx - 1 - map[rowIdx].Count; --i)
        {
            int index = i < 0 ? map[rowIdx].Count + (i % map[rowIdx].Count) : i;

            if (map[rowIdx][index] == '.')
            {
                return index;
            }
            else if (map[rowIdx][index] == '#')
            {
                return colIdx;
            }
        }

        throw new Exception("Logical error");
    }

    private static int GetNextRowIndex(int rowIdx, int colIdx, List<List<char>> map)
    {
        for (int i = rowIdx + 1; i < rowIdx + 1 + map.Count; ++i)
        {
            int index = i % map.Count;

            if (map[index].Count <= colIdx)
            {
                continue;
            }

            if (map[index][colIdx] == '.')
            {
                return index;
            }
            else if (map[index][colIdx] == '#')
            {
                return rowIdx;
            }
        }

        throw new Exception("Logical error");
    }

    private static int GetPrevRowIndex(int rowIdx, int colIdx, List<List<char>> map)
    {
        for (int i = rowIdx - 1; i >= rowIdx - 1 - map.Count; --i)
        {
            int index = i < 0 ? map.Count + (i % map.Count) : i;

            if (map[index].Count <= colIdx)
            {
                continue;
            }

            if (map[index][colIdx] == '.')
            {
                return index;
            }
            else if (map[index][colIdx] == '#')
            {
                return rowIdx;
            }
        }

        throw new Exception("Logical error");
    }
}
