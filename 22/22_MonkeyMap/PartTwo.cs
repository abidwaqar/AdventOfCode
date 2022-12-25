namespace AdventOfCode._22._22_MonkeyMap;

internal static class PartTwo
{
    // T = O(n) | S = O(h * w)
    // Where n is the sum of all the number of tiles to move in the instruction, h is the height of the map and w is the width of the map.
    // NOTE: This solution is hard coded for the given input. It'll not work on any other input.
    public static int Solve()
    {
        List<List<char>> map = new();
        int rowIdx = -1, colIdx = -1;
        string[] inputArr = File.ReadAllLines("../../../22/22_MonkeyMap/prod.txt");
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
                int oldRowIdx = rowIdx, oldColIdx = colIdx;
                Direction oldDirection = currDirection;  

                Move(ref rowIdx, ref colIdx, ref currDirection, steps, map);

                //PrintMap(oldRowIdx, oldColIdx, rowIdx, colIdx, map, steps, oldDirection);

                currDirection = GetUpdatedDirection(currDirection, character == 'R' ? Direction.RIGHT : Direction.LEFT);
                steps = 0;
                continue;
            }

            steps = steps * 10 + (character - '0');
        }

        Move(ref rowIdx, ref colIdx, ref currDirection, steps, map);

        return ((rowIdx + 1) * 1000) + ((colIdx + 1) * 4) + (int)currDirection;
    }

    private static void PrintMap(int lastRowIdx, int lastColIdx, int rowIdx, int colIdx, List<List<char>> map, int steps, Direction oldDirection)
    {
        for (int i = Math.Max(0, Math.Min(lastRowIdx, rowIdx) - 1); i <= Math.Min(map.Count - 1, Math.Max(lastRowIdx, rowIdx) + 1); ++i)
        {
            for (int j = 0; j < map[i].Count; ++j)
            {
                if (i == lastRowIdx && j == lastColIdx)
                {
                    Console.Write('L');
                } else if (i == rowIdx && j == colIdx)
                {
                    Console.Write('C');
                } else
                {
                    Console.Write(map[i][j]);
                }

            }

            Console.WriteLine(i);
        }

        Console.WriteLine("Direction: " + oldDirection.ToString());
        Console.WriteLine("Steps: " + steps);

        Console.Clear();
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

    private static void Move(ref int rowIdx, ref int colIdx, ref Direction currDirection, int steps, List<List<char>> map)
    {
        for (int i = 0; i < steps; ++i)
        {
            int oldColIdx = colIdx, oldRowIdx = rowIdx;
            switch (currDirection)
            {
                case Direction.RIGHT:
                    if (colIdx + 1 < map[rowIdx].Count)
                    {
                        if (map[rowIdx][colIdx + 1] == '.')
                        {
                            ++colIdx;
                        }
                    } else
                    {
                        if (rowIdx < 50)
                        {
                            int newRowIdx = 100 + (49 - rowIdx);
                            int newColIdx = 99;

                            if (map[newRowIdx][newColIdx] == '.')
                            {
                                rowIdx = newRowIdx;
                                colIdx = newColIdx;
                                currDirection = Direction.LEFT;
                            }
                        } else if (rowIdx < 100)
                        {
                            int newRowIdx = 49;
                            int newColIdx = 100 + (rowIdx - 50);

                            if (map[newRowIdx][newColIdx] == '.')
                            {
                                rowIdx = newRowIdx;
                                colIdx = newColIdx;
                                currDirection = Direction.UP;
                            }
                        } else if (rowIdx < 150)
                        {
                            int newRowIdx = 149 - rowIdx;
                            int newColIdx = 149;

                            if (map[newRowIdx][newColIdx] == '.')
                            {
                                rowIdx = newRowIdx;
                                colIdx = newColIdx;
                                currDirection = Direction.LEFT;
                            }
                        } else // rowIdx < 200
                        {
                            int newRowIdx = 149;
                            int newColIdx = 50 + (rowIdx - 150);

                            if (map[newRowIdx][newColIdx] == '.')
                            {
                                rowIdx = newRowIdx;
                                colIdx = newColIdx;
                                currDirection = Direction.UP;
                            }
                        }
                    }
                    break;
                case Direction.DOWN:
                    if (rowIdx + 1 < map.Count && colIdx < map[rowIdx + 1].Count)
                    {
                        if (map[rowIdx + 1][colIdx] == '.')
                        {
                            ++rowIdx;
                        }
                    }
                    else
                    {
                        if (colIdx < 50)
                        {
                            int newRowIdx = 0;
                            int newColIdx = 100 + colIdx;

                            if (map[newRowIdx][newColIdx] == '.')
                            {
                                rowIdx = newRowIdx;
                                colIdx = newColIdx;
                                currDirection = Direction.DOWN;
                            }
                        }
                        else if (colIdx < 100)
                        {
                            int newRowIdx = 150 + (colIdx - 50);
                            int newColIdx = 49;

                            if (map[newRowIdx][newColIdx] == '.')
                            {
                                rowIdx = newRowIdx;
                                colIdx = newColIdx;
                                currDirection = Direction.LEFT;
                            }
                        }
                        else // colIdx < 150
                        {
                            int newRowIdx = 50 + (colIdx - 100);
                            int newColIdx = 99;

                            if (map[newRowIdx][newColIdx] == '.')
                            {
                                rowIdx = newRowIdx;
                                colIdx = newColIdx;
                                currDirection = Direction.LEFT;
                            }
                        }
                    }
                    break;
                case Direction.LEFT:
                    if (colIdx - 1 >= 0 && map[rowIdx][colIdx - 1] != ' ')
                    {
                        if (map[rowIdx][colIdx - 1] == '.')
                        {
                            --colIdx;
                        }
                    }
                    else
                    {
                        if (rowIdx < 50)
                        {
                            int newRowIdx = 100 + (49 - rowIdx);
                            int newColIdx = 0;

                            if (map[newRowIdx][newColIdx] == '.')
                            {
                                rowIdx = newRowIdx;
                                colIdx = newColIdx;
                                currDirection = Direction.RIGHT;
                            }
                        }
                        else if (rowIdx < 100)
                        {
                            int newRowIdx = 100;
                            int newColIdx = rowIdx - 50;

                            if (map[newRowIdx][newColIdx] == '.')
                            {
                                rowIdx = newRowIdx;
                                colIdx = newColIdx;
                                currDirection = Direction.DOWN;
                            }
                        }
                        else if (rowIdx < 150)
                        {
                            int newRowIdx = 149 - rowIdx;
                            int newColIdx = 50;

                            if (map[newRowIdx][newColIdx] == '.')
                            {
                                rowIdx = newRowIdx;
                                colIdx = newColIdx;
                                currDirection = Direction.RIGHT;
                            }
                        }
                        else // rowIdx < 200
                        {
                            int newRowIdx = 0;
                            int newColIdx =  50 + (rowIdx - 150);

                            if (map[newRowIdx][newColIdx] == '.')
                            {
                                rowIdx = newRowIdx;
                                colIdx = newColIdx;
                                currDirection = Direction.DOWN;
                            }
                        }
                    }
                    break;
                case Direction.UP:
                    if (rowIdx - 1 >= 0 && map[rowIdx - 1][colIdx] != ' ')
                    {
                        if (map[rowIdx - 1][colIdx] == '.')
                        {
                            --rowIdx;
                        }
                    }
                    else
                    {
                        if (colIdx < 50)
                        {
                            int newRowIdx = 50 + colIdx;
                            int newColIdx = 50;

                            if (map[newRowIdx][newColIdx] == '.')
                            {
                                rowIdx = newRowIdx;
                                colIdx = newColIdx;
                                currDirection = Direction.RIGHT;
                            }
                        }
                        else if (colIdx < 100)
                        {
                            int newRowIdx = 150 + (colIdx - 50);
                            int newColIdx = 0;

                            if (map[newRowIdx][newColIdx] == '.')
                            {
                                rowIdx = newRowIdx;
                                colIdx = newColIdx;
                                currDirection = Direction.RIGHT;
                            }
                        }
                        else // colIdx < 150
                        {
                            int newRowIdx = 199;
                            int newColIdx = colIdx - 100;

                            if (map[newRowIdx][newColIdx] == '.')
                            {
                                rowIdx = newRowIdx;
                                colIdx = newColIdx;
                                currDirection = Direction.UP;
                            }
                        }
                    }
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
}