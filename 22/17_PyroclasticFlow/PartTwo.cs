﻿using System.Text;

namespace AdventOfCode._22._17_PyroclasticFlow;

internal static class PartTwo
{
    // T = O(n * m * l * k) | S = O(n * m)
    // Where n is the count of rocks for which we need to run the simulation (1000000000000) and m is the width of chamber (7).
    // l is the max rock width and k is the max rock height
    // 1000000000000 is the worst case, on avg n will be much much smaller due to memoization.
    public static long Solve()
    {
        List<List<List<char>>> rocks = new()
        {
            new() { new() { '#', '#', '#', '#' } },
            new() { new() { '.', '#', '.' }, new() { '#', '#', '#' }, new() { '.', '#', '.' } },
            new() { new() { '.', '.', '#' }, new() { '.', '.', '#' }, new() { '#', '#', '#' } },
            new() { new() { '#' }, new() { '#' }, new() { '#' }, new() { '#' } },
            new() { new() { '#', '#' }, new() { '#', '#' } },
        };

        long rocksCountThatNeedsToStop = 1000000000000;
        string jetPattern = File.ReadAllLines("../../../Input/prod.txt")[0];

        int highestGroundIdx = -1;
        int jetPatternIdx = 0;
        List<List<char>> chamber = new();
        Dictionary<string, Tuple<long, long>> seen = new();
        long added = 0;
        for (long i = 0; i < rocksCountThatNeedsToStop; ++i)
        {
            List<List<char>> currRock = rocks[(int)(i % rocks.Count)];

            for (int j = currRock.Count + 3 - (chamber.Count - (highestGroundIdx + 1)); j > 0; --j)
            {
                chamber.Add(new() { '.', '.', '.', '.', '.', '.', '.' });
            }

            int rowIdx = highestGroundIdx + 3 + currRock.Count, colIdx = 2;
            while (true)
            {
                int newColIdx = colIdx;
                if (jetPattern[jetPatternIdx] == '<')
                {
                    --newColIdx;

                }
                else if (jetPattern[jetPatternIdx] == '>')
                {
                    ++newColIdx;
                }
                else
                {
                    throw new Exception("Undefined jet pattern");
                }

                colIdx = IsValidPosition(currRock, chamber, rowIdx, newColIdx) ? newColIdx : colIdx;
                jetPatternIdx = (jetPatternIdx + 1) % jetPattern.Length;

                if (!IsValidPosition(currRock, chamber, rowIdx - 1, colIdx))
                {
                    break;
                }

                --rowIdx;
            }

            highestGroundIdx = Math.Max(highestGroundIdx, rowIdx);
            for (int j = 0; j < currRock.Count; ++j)
            {
                for (int k = 0; k < currRock[j].Count; ++k)
                {
                    if (currRock[j][k] == '#')
                    {
                        chamber[rowIdx - j][colIdx + k] = '#';
                    }
                }
            }

            string pattern = GetPattern(chamber, 30, (int)(i % rocks.Count), jetPatternIdx);
            if (seen.ContainsKey(pattern))
            {
                (long rockIdx, long top) = seen[pattern];
                long deltaTop = highestGroundIdx - top;
                long deltaRockIdx = i - rockIdx;
                long amount = (rocksCountThatNeedsToStop - i) / deltaRockIdx;
                added += (amount * deltaTop);
                i += (amount * deltaRockIdx);
            }

            seen[pattern] = new(i, highestGroundIdx);
        }

        return added + highestGroundIdx + 1;
    }

    private static string GetPattern(List<List<char>> chamber, int n, int rockIdx, int jetPatternIdx)
    {
        StringBuilder patternBuilder = new();
        for (int i = 0;i < n; ++i)
        {
            int rowIdx = chamber.Count - 1 - i;
            if (rowIdx < 0)
            {
                break;
            }

            foreach (char character in chamber[rowIdx])
            {
                patternBuilder.Append(character);
            }
        }

        return patternBuilder.Append("|" + rockIdx + "|" + jetPatternIdx).ToString();
    }

    private static bool IsValidPosition(List<List<char>> rock, List<List<char>> chamber, int rowIdx, int colIdx)
    {
        if (colIdx < 0 || colIdx + rock[0].Count > chamber[0].Count || rowIdx - (rock.Count - 1) < 0)
        {
            return false;
        }

        for (int j = 0; j < rock.Count; ++j)
        {
            for (int k = 0; k < rock[j].Count; ++k)
            {
                if (rock[j][k] == '#' && chamber[rowIdx - j][colIdx + k] == '#')
                {
                    return false;
                }
            }
        }

        return true;
    }
}
