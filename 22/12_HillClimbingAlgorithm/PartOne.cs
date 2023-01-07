namespace AdventOfCode._22._12_HillClimbingAlgorithm;

internal static class PartOne
{
    // T = O(h * w) | S = O(h * w)
    // Where h and w are height and width of the grid respectively.
    public static int Solve()
    {
        int initialI = 0, initialJ = 0;
        List<List<char>> grid = new List<List<char>>();
        foreach (string input in File.ReadAllLines("../../../Input/prod.txt"))
        {
            List<char> row = new List<char>();
            foreach (char character in input)
            {
                if (character == 'S')
                {
                    initialI = grid.Count;
                    initialJ = row.Count;
                }

                row.Add(character);
            }

            grid.Add(row);
        }

        Queue<Tuple<int, int>> queue = new Queue<Tuple<int, int>>();
        bool[,] visited = new bool[grid.Count, grid[0].Count];

        queue.Enqueue(new Tuple<int, int>(initialI, initialJ));
        visited[initialI, initialJ] = true;

        int fewestStepsRequired = 0;
        while (queue.Count != 0)
        {
            for (int i = queue.Count - 1; i >= 0; --i)
            {
                Tuple<int, int> currentCell = queue.Dequeue(); ;
                if (grid[currentCell.Item1][currentCell.Item2] == 'E')
                {
                    return fewestStepsRequired;
                }

                ProcessCell(grid, queue, visited, currentCell.Item1, currentCell.Item2, currentCell.Item1 - 1, currentCell.Item2);
                ProcessCell(grid, queue, visited, currentCell.Item1, currentCell.Item2, currentCell.Item1 + 1, currentCell.Item2);
                ProcessCell(grid, queue, visited, currentCell.Item1, currentCell.Item2, currentCell.Item1, currentCell.Item2 - 1);
                ProcessCell(grid, queue, visited, currentCell.Item1, currentCell.Item2, currentCell.Item1, currentCell.Item2 + 1);
            }

            ++fewestStepsRequired;
        }

        throw new Exception("Not possible to reach the destination");
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
