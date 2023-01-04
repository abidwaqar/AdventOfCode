namespace AdventOfCode._22._23_UnstableDiffusion;

internal static class PartTwo
{
    // T = O(n * m) | S = O(n)
    // Where n is the count of elfs and m is the number of iteration required before the elfs stop moving and it depends on the elfs arrangement
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
        for (int i = 1;; ++i)
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
                            }
                            else
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
                            }
                            else
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
                return i;
            }

            elfs = updatedElfs;

            Direction tempDirection = directions[0];
            directions.RemoveAt(0);
            directions.Add(tempDirection);
        }

        throw new Exception("Logical error");
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
