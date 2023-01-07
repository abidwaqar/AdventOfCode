namespace AdventOfCode._22._8_TreetopTreehouse
{
    internal static class PartOne
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

            int visibleTrees = (grid.Count * 2) + (grid.First().Count * 2) - 4;
            for (int i = 1; i < grid.Count - 1; ++i)
            {
                for (int j = 1; j < grid[i].Count - 1; ++j)
                {
                    int currTreeHeight = grid[i][j];
                    
                    bool isHidden = false;
                    for (int k = i - 1; k >= 0; --k)
                    {
                        if (grid[k][j] >= currTreeHeight)
                        {
                            isHidden = true;
                            break;
                        }
                    }
                    
                    if (!isHidden)
                    {
                        ++visibleTrees;
                        continue;
                    }

                    isHidden = false;
                    for (int k = i + 1; k < grid.Count; ++k)
                    {
                        if (grid[k][j] >= currTreeHeight)
                        {
                            isHidden = true;
                            break;
                        }
                    }

                    if (!isHidden)
                    {
                        ++visibleTrees;
                        continue;
                    }

                    isHidden = false;
                    for (int k = j - 1; k >= 0; --k)
                    {
                        if (grid[i][k] >= currTreeHeight)
                        {
                            isHidden = true;
                            break;
                        }
                    }

                    if (!isHidden)
                    {
                        ++visibleTrees;
                        continue;
                    }

                    isHidden = false;
                    for (int k = j + 1; k < grid[i].Count; ++k)
                    {
                        if (grid[i][k] >= currTreeHeight)
                        {
                            isHidden = true;
                            break;
                        }
                    }

                    if (!isHidden)
                    {
                        ++visibleTrees;
                        continue;
                    }
                }
            }

            return visibleTrees;
        }
    }
}
