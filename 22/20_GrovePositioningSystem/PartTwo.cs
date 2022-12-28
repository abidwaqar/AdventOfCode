namespace AdventOfCode._22._20_GrovePositioningSystem;

internal static class PartTwo
{
    // T = O(n^2) | S = O(n)
    // Where n is the count of input numbers
    public static long Solve()
    {
        int index = 0;
        List<long> numbers = new();
        List<int> mixedNumbers = new();
        foreach (string input in File.ReadAllLines("../../../Input/prod.txt"))
        {
            numbers.Add(Convert.ToInt64(input) * 811589153);
            mixedNumbers.Add(index);
            ++index;
        }

        for (int i = 0; i < 10; ++i)
        {
            for (int j = 0; j < numbers.Count; ++j)
            {
                int mixedNumbersIndex = mixedNumbers.FindIndex(x => x == j);
                mixedNumbers.Remove(j);
                mixedNumbers.Insert(GetCircularIndex(mixedNumbersIndex, numbers[j], mixedNumbers.Count), j);
            }
        }

        int zeroNumbersIndex = numbers.FindIndex(x => x == 0);
        int zeroMixedNumbersIndex = mixedNumbers.FindIndex(x => x == zeroNumbersIndex);

        return numbers[mixedNumbers[(zeroMixedNumbersIndex + 1000) % numbers.Count]] + numbers[mixedNumbers[(zeroMixedNumbersIndex + 2000) % numbers.Count]] + numbers[mixedNumbers[(zeroMixedNumbersIndex + 3000) % numbers.Count]];
    }

    private static int GetCircularIndex(int index, long num, int length)
    {
        if (index + num < 0)
        {
            return (int)(length + ((index + num) % length));
        }

        return (int)((index + num) % length);
    }
}