namespace AdventOfCode._22._20_GrovePositioningSystem;

internal static class PartOne
{
    // T = O(n^2) | S = O(n)
    // Where n is the count of input numbers
    public static int Solve()
    {
        int index = 0;
        List<int> numbers = new();
        List<int> mixedNumbers = new();
        foreach (string input in File.ReadAllLines("../../../Input/prod.txt"))
        {
            numbers.Add(Convert.ToInt32(input));
            mixedNumbers.Add(index);
            ++index;
        }

        for (int i = 0; i < numbers.Count; ++i)
        {
            int mixedNumbersIndex = mixedNumbers.FindIndex(x => x == i);
            mixedNumbers.Remove(i);
            mixedNumbers.Insert(GetCircularIndex(mixedNumbersIndex, numbers[i], mixedNumbers.Count), i);
        }

        int zeroNumbersIndex = numbers.FindIndex(x => x == 0);
        int zeroMixedNumbersIndex = mixedNumbers.FindIndex(x => x == zeroNumbersIndex);

        return numbers[mixedNumbers[(zeroMixedNumbersIndex + 1000) % numbers.Count]] + numbers[mixedNumbers[(zeroMixedNumbersIndex + 2000) % numbers.Count]] + numbers[mixedNumbers[(zeroMixedNumbersIndex + 3000) % numbers.Count]];
    }

    private static int GetCircularIndex(int index, int num, int length)
    {
        if (index + num < 0)
        {
            return length + ((index + num) % length);
        }

        return (index + num) % length; 
    }
}