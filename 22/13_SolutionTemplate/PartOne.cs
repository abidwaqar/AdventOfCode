using System.Collections;
using System.Text;

namespace AdventOfCode._22._13_SolutionTemplate;

internal static class PartOne
{
    // T = O(n * m) | S = O(m)
    // Where n is the count of packets and  m is the max depth of arrays in a packet.
    public static int Solve()
    {
        int result = 0;
        int index = 1;
        ArrayList? packet1 = null;
        ArrayList? packet2 = null;
        foreach (string input in File.ReadAllLines("../../../Input/prod.txt"))
        {
            if (input == string.Empty)
            {
                continue;
            }

            if (packet1 == null)
            {
                int tempIndex = 1;
                packet1 = ProcessInput(input, ref tempIndex);
            } else if (packet2 == null)
            {
                int tempIndex = 1;
                packet2 = ProcessInput(input, ref tempIndex);
            }

            if (packet1 != null && packet2 != null)
            {
                if (IsInRightOrder(packet1, packet2) == true)
                {
                    result += index;
                }

                packet1 = null;
                packet2 = null;
                ++index;
            }
        }

        return result;
    }

    private static ArrayList ProcessInput(string input, ref int index)
    {
        ArrayList result = new();

        while (index < input.Length)
        {
            char currChar = input[index];
            ++index;

            if (currChar == '[')
            {
                result.Add(ProcessInput(input, ref index));
            } else if (currChar == ']') 
            {
                break;
            } else if (currChar == ',')
            {
                continue;
            } else
            {
                StringBuilder integer = new(char.ToString(currChar));
                while (char.IsDigit(input[index]))
                {
                    integer.Append(char.ToString(input[index]));
                    ++index;
                }

                result.Add(Convert.ToInt32(integer.ToString()));
            }
        }

        return result;
    }

    private static bool? IsInRightOrder(ArrayList packet1, ArrayList packet2)
    {
        int index = 0;
        while (index < packet1.Count && index < packet2.Count)
        {
            var value1 = packet1[index];
            var value2 = packet2[index];
            ++index;

            if (value1?.GetType() == typeof(int) && value2?.GetType() == typeof(int))
            {
                if ((int)value1 < (int)value2)
                {
                    return true;
                } else if ((int)value1 > (int)value2)
                {
                    return false;
                }
            } else
            {
                ArrayList arrayList1 = (value1?.GetType() == typeof(int)) ? new() { value1 } : (ArrayList)value1!;
                ArrayList arrayList2 = (value2?.GetType() == typeof(int)) ? new() { value2 } : (ArrayList)value2!;

                bool? isInRightOrder = IsInRightOrder(arrayList1, arrayList2);
                if (isInRightOrder != null)
                {
                    return isInRightOrder;
                }
            }
        }

        if (packet1.Count < packet2.Count)
        {
            return true;
        } else if (packet1.Count > packet2.Count)
        {
            return false;
        } else
        {
            return null;
        }
    }
}
