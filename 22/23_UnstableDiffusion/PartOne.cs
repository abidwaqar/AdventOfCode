namespace AdventOfCode._22._23_UnstableDiffusion;

internal static class PartOne
{
    // T = O(n) | S = O(n)
    // Where n is the count of elfs
    public static int Solve()
    {
        HashSet<Tuple<int, int>> elfs = new();
        string[] inputArr = File.ReadAllLines("../../../Input/prod.txt");
        for (int i = 0; i < inputArr.Length; ++i)
        {
            for (int j = 0; j < inputArr[i].Length; ++j)
            {
                if (inputArr[i][j] == '#')
                {
                    elfs.Add(new(j, -i + inputArr.Length - 1));
                }
            }
        }

        List<Direction> directions = new() { Direction.NORTH, Direction.SOUTH, Direction.WEST, Direction.EAST };
        for (int i = 0; i < 10; ++i)
        {
            HashSet<Tuple<int, int>> proposals = new();
            HashSet<Tuple<int, int>> conflicts = new();
            foreach (Tuple<int, int> elf in elfs)
            {
                if (!AdjacentPositionContainsElf(elf, elfs))
                {
                    continue;
                }

                foreach (Direction direction in directions)
                {
                    if (direction == Direction.NORTH)
                    {
                        if (!elfs.Contains(new(elf.Item1 - 1, elf.Item2 + 1)) && !elfs.Contains(new(elf.Item1, elf.Item2 + 1)) && !elfs.Contains(new(elf.Item1 + 1, elf.Item2 + 1)))
                        {
                            Tuple<int, int> proposal = new(elf.Item1, elf.Item2 + 1);
                            if (!proposals.Contains(proposal))
                            {
                                proposals.Add(proposal);
                            } else
                            {
                                conflicts.Add(proposal);
                            }

                            break;
                        }
                    } 
                    else if (direction == Direction.SOUTH)
                    {
                        if (!elfs.Contains(new(elf.Item1 - 1, elf.Item2 - 1)) && !elfs.Contains(new(elf.Item1, elf.Item2 - 1)) && !elfs.Contains(new(elf.Item1 + 1, elf.Item2 - 1)))
                        {
                            Tuple<int, int> proposal = new(elf.Item1, elf.Item2 - 1);
                            if (!proposals.Contains(proposal))
                            {
                                proposals.Add(proposal);
                            }
                            else
                            {
                                conflicts.Add(proposal);
                            }

                            break;
                        }
                    } 
                    else if (direction == Direction.WEST)
                    {
                        if (!elfs.Contains(new(elf.Item1 - 1, elf.Item2 + 1)) && !elfs.Contains(new(elf.Item1 - 1, elf.Item2)) && !elfs.Contains(new(elf.Item1 - 1, elf.Item2 - 1)))
                        {
                            Tuple<int, int> proposal = new(elf.Item1 - 1, elf.Item2);
                            if (!proposals.Contains(proposal))
                            {
                                proposals.Add(proposal);
                            }
                            else
                            {
                                conflicts.Add(proposal);
                            }

                            break;
                        }
                    }
                    else if (direction == Direction.EAST)
                    {
                        if (!elfs.Contains(new(elf.Item1 + 1, elf.Item2 + 1)) && !elfs.Contains(new(elf.Item1 + 1, elf.Item2)) && !elfs.Contains(new(elf.Item1 + 1, elf.Item2 - 1)))
                        {
                            Tuple<int, int> proposal = new(elf.Item1 + 1, elf.Item2);
                            if (!proposals.Contains(proposal))
                            {
                                proposals.Add(proposal);
                            }
                            else
                            {
                                conflicts.Add(proposal);
                            }

                            break;
                        }
                    }
                    else
                    {
                        throw new Exception("Direction not handled");
                    }
                }
            }

            HashSet<Tuple<int, int>> updatedElfs = new();
            foreach (Tuple<int, int> elf in elfs)
            {
                if (!AdjacentPositionContainsElf(elf, elfs))
                {
                    updatedElfs.Add(elf);
                    continue;
                }

                bool added = false;
                foreach (Direction direction in directions)
                {
                    if (direction == Direction.NORTH)
                    {
                        if (!elfs.Contains(new(elf.Item1 - 1, elf.Item2 + 1)) && !elfs.Contains(new(elf.Item1, elf.Item2 + 1)) && !elfs.Contains(new(elf.Item1 + 1, elf.Item2 + 1)))
                        {
                            Tuple<int, int> proposal = new(elf.Item1, elf.Item2 + 1);
                            if (!conflicts.Contains(proposal))
                            {
                                updatedElfs.Add(proposal);
                            } else
                            {
                                updatedElfs.Add(elf);
                            }

                            added = true;
                            break;
                        }
                    }
                    else if (direction == Direction.SOUTH)
                    {
                        if (!elfs.Contains(new(elf.Item1 - 1, elf.Item2 - 1)) && !elfs.Contains(new(elf.Item1, elf.Item2 - 1)) && !elfs.Contains(new(elf.Item1 + 1, elf.Item2 - 1)))
                        {
                            Tuple<int, int> proposal = new(elf.Item1, elf.Item2 - 1);
                            if (!conflicts.Contains(proposal))
                            {
                                updatedElfs.Add(proposal);
                            }
                            else
                            {
                                updatedElfs.Add(elf);
                            }

                            added = true;
                            break;
                        }
                    }
                    else if (direction == Direction.WEST)
                    {
                        if (!elfs.Contains(new(elf.Item1 - 1, elf.Item2 + 1)) && !elfs.Contains(new(elf.Item1 - 1, elf.Item2)) && !elfs.Contains(new(elf.Item1 - 1, elf.Item2 - 1)))
                        {
                            Tuple<int, int> proposal = new(elf.Item1 - 1, elf.Item2);
                            if (!conflicts.Contains(proposal))
                            {
                                updatedElfs.Add(proposal);
                            }
                            else
                            {
                                updatedElfs.Add(elf);
                            }

                            added = true;
                            break;
                        }
                    }
                    else if (direction == Direction.EAST)
                    {
                        if (!elfs.Contains(new(elf.Item1 + 1, elf.Item2 + 1)) && !elfs.Contains(new(elf.Item1 + 1, elf.Item2)) && !elfs.Contains(new(elf.Item1 + 1, elf.Item2 - 1)))
                        {
                            Tuple<int, int> proposal = new(elf.Item1 + 1, elf.Item2);
                            if (!conflicts.Contains(proposal))
                            {
                                updatedElfs.Add(proposal);
                            }
                            else
                            {
                                updatedElfs.Add(elf);
                            }

                            added = true;
                            break;
                        }
                    }
                    else
                    {
                        throw new Exception("Direction not handled");
                    }
                }

                if (!added)
                {
                    updatedElfs.Add(elf);
                }
            }
            if (elfs.SetEquals(updatedElfs))
            {
                break;
            }

            elfs = updatedElfs;

            Direction tempDirection = directions[0];
            directions.RemoveAt(0);
            directions.Add(tempDirection);
        }

        int minX = int.MaxValue, maxX = int.MinValue, minY = int.MaxValue, maxY = int.MinValue;
        foreach (Tuple<int, int> elf in elfs)
        {
            minX = Math.Min(minX, elf.Item1);
            maxX = Math.Max(maxX, elf.Item1);
            minY = Math.Min(minY, elf.Item2);
            maxY = Math.Max(maxY, elf.Item2);
        }

        return ((maxX - minX + 1) * (maxY - minY + 1)) - elfs.Count;
    }

    private enum Direction
    {
        NORTH,
        SOUTH,
        WEST,
        EAST,
    }

    private static bool AdjacentPositionContainsElf(Tuple<int, int> elf, HashSet<Tuple<int, int>> elfs)
    {
        return elfs.Contains(new(elf.Item1, elf.Item2 + 1)) || 
            elfs.Contains(new(elf.Item1 + 1, elf.Item2 + 1)) || 
            elfs.Contains(new(elf.Item1 + 1, elf.Item2)) || 
            elfs.Contains(new(elf.Item1 + 1, elf.Item2 - 1)) || 
            elfs.Contains(new(elf.Item1, elf.Item2 - 1)) || 
            elfs.Contains(new(elf.Item1 - 1, elf.Item2 - 1)) || 
            elfs.Contains(new(elf.Item1 - 1, elf.Item2)) || 
            elfs.Contains(new(elf.Item1 - 1, elf.Item2 + 1));
    }
}
