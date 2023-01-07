namespace AdventOfCode._22._8_TreetopTreehouse
{
    internal static class PartTwo
    {
        // T = O(h * w * (h + w)) | S = O(h * w)
        // Where h and w are height and width of the grid respectively
        public static int Solve()
        {
            List<List<int>> grid = new List<List<int>>();
            foreach (var input in File.ReadAllLines("../../../Input/prod.txt"))
            {
                List<int> row = new List<int>();
                foreach (int height in input)
                {
                    row.Add(height - 48);
                }

                grid.Add(row);
            }

            int highestScenicScore = 0;
            for (int i = 0; i < grid.Count; ++i)
            {
                for (int j = 0; j < grid[i].Count; ++j)
                {
                    int currTreeHeight = grid[i][j];
                    int scenicScore = 1;

                    int viewCount = 0;
                    for (int k = i - 1; k >= 0; --k)
                    {
                        ++viewCount;
                        if (grid[k][j] >= currTreeHeight)
                        {
                            break;
                        }
                    }

                    scenicScore *= viewCount;

                    viewCount = 0;
                    for (int k = i + 1; k < grid.Count; ++k)
                    {
                        ++viewCount;
                        if (grid[k][j] >= currTreeHeight)
                        {
                            break;
                        }
                    }

                    scenicScore *= viewCount;

                    viewCount = 0;
                    for (int k = j - 1; k >= 0; --k)
                    {
                        ++viewCount;
                        if (grid[i][k] >= currTreeHeight)
                        {
                            break;
                        }
                    }

                    scenicScore *= viewCount;

                    viewCount = 0;
                    for (int k = j + 1; k < grid[i].Count; ++k)
                    {
                        ++viewCount;
                        if (grid[i][k] >= currTreeHeight)
                        {
                            break;
                        }
                    }

                    scenicScore *= viewCount;

                    highestScenicScore = Math.Max(highestScenicScore, scenicScore);
                }
            }

            return highestScenicScore;
        }
    }
}
