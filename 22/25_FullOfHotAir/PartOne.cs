namespace AdventOfCode._22._25_FullOfHotAir;

internal static class PartOne
{
    private static Dictionary<char, int> snafuCharToInt = new Dictionary<char, int>()
    {
        {'=', -2 },
        {'-', -1 },
        {'0', 0 },
        {'1', 1 },
        {'2', 2 },
    };

    private static Dictionary<int, char> intToSnafuChar = new Dictionary<int, char>()
    {
        {-2, '=' },
        {-1, '-' },
        {0, '0' },
        {1, '1' },
        {2, '2' },
    };

    // T = O(n * m) | S = O(o)
    // Where n is the count of snafu number in input, m is the longest snafu number in input and o is the length of output snafu number.
    public static string Solve()
    {
        long result = 0;
        foreach (string input in File.ReadAllLines("../../../Input/prod.txt"))
        {
            result += ToDecimalNumber(input);
        }

        return ToSnafuNumber(result);
    }

    private static long ToDecimalNumber(string snafuNumber)
    {
        long result = 0;
        for (int i = 0; i < snafuNumber.Length; ++i)
        {
            var power = (long)Math.Pow(5, i);
            var digit = snafuNumber[snafuNumber.Length - 1 - i];

            result += power * snafuCharToInt[digit];
        }

        return result;
    }

    private static string ToSnafuNumber(long decimalNumber)
    {
        string snafuNumber = "";
        int i = 0;
        while (decimalNumber != 0)
        {
            long states = (long)Math.Pow(5, i + 1);
            long power = (long)Math.Pow(5, i);
            int remainder = (int)((decimalNumber % states) / power);

            remainder = remainder == 3 ? -2 : remainder;
            remainder = remainder == 4 ? -1 : remainder;

            snafuNumber = intToSnafuChar[remainder] + snafuNumber;

            decimalNumber -= remainder * power;

            ++i;
        }

        return snafuNumber;
    }
}
