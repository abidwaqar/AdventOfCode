namespace AdventOfCode._22._12_HillClimbingAlgorithm;

internal static class PartTwo
{
    // T = O(h * w) | S = O(h * w)
    // Where h and w are height and width of the grid respectively.
    public static int Solve()
    {
        List<Tuple<int, int>> startingPositions = new();
        List<List<char>> grid = new List<List<char>>();
        foreach (string input in File.ReadAllLines("../../../input.txt"))
        {
            List<char> row = new List<char>();
            foreach (char character in input)
            {
                if (character == 'S' || character == 'a')
                {
                    startingPositions.Add(new Tuple<int, int>(grid.Count, row.Count));
                }

                row.Add(character);
            }

            grid.Add(row);
        }

        int fewestStepsRequired = int.MaxValue;
        Queue<Tuple<int, int>> queue = new Queue<Tuple<int, int>>();
        foreach (Tuple<int, int> startingPosition in startingPositions)
        {
            queue.Enqueue(startingPosition);
            bool[,] visited = new bool[grid.Count, grid[0].Count];
            visited[startingPosition.Item1, startingPosition.Item2] = true;

            int stepsRequired = 0;
            while (queue.Count != 0)
            {
                for (int i = queue.Count - 1; i >= 0; --i)
                {
                    Tuple<int, int> currentCell = queue.Dequeue(); ;
                    if (grid[currentCell.Item1][currentCell.Item2] == 'E')
                    {
                        fewestStepsRequired = Math.Min(fewestStepsRequired, stepsRequired);
                        queue.Clear();
                        break;
                    }

                    ProcessCell(grid, queue, visited, currentCell.Item1, currentCell.Item2, currentCell.Item1 - 1, currentCell.Item2);
                    ProcessCell(grid, queue, visited, currentCell.Item1, currentCell.Item2, currentCell.Item1 + 1, currentCell.Item2);
                    ProcessCell(grid, queue, visited, currentCell.Item1, currentCell.Item2, currentCell.Item1, currentCell.Item2 - 1);
                    ProcessCell(grid, queue, visited, currentCell.Item1, currentCell.Item2, currentCell.Item1, currentCell.Item2 + 1);
                }

                ++stepsRequired;
            }
        }

        return fewestStepsRequired;
    }
    private static void ProcessCell(List<List<char>> grid, Queue<Tuple<int, int>> queue, bool[,] visited, int currI, int currJ, int nextI, int nextJ)
    {
        if (IsJumpPossible(grid, currI, currJ, nextI, nextJ) && !visited[nextI, nextJ])
        {
            queue.Enqueue(new Tuple<int, int>(nextI, nextJ));
            visited[nextI, nextJ] = true;
        }
    }

    private static bool IsJumpPossible(List<List<char>> grid, int currI, int currJ, int nextI, int nextJ)
    {
        if (nextI < 0 || nextI >= grid.Count || nextJ < 0 || nextJ >= grid[nextI].Count)
        {
            return false;
        }

        char currentCharacter = grid[currI][currJ] == 'S' ? 'a' : grid[currI][currJ];
        char nextCharacter = grid[nextI][nextJ] == 'E' ? 'z' : grid[nextI][nextJ];

        return currentCharacter + 1 >= nextCharacter;
    }
}
