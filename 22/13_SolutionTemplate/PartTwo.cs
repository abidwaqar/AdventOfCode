using System.Collections;
using System.Text;

namespace AdventOfCode._22._13_SolutionTemplate;

internal static class PartTwo
{
    // T = O((n * m) * log(n * m)) | S = O(m + log(n * m))
    // Where n is the count of packets and  m is the max depth of arrays in a packet.
    public static int Solve()
    {
        List<ArrayList> packets = new List<ArrayList>();
        foreach (string input in File.ReadAllLines("../../../Input/prod.txt"))
        {
            if (input == string.Empty)
            {
                continue;
            }

            int tempIndex = 1;
            packets.Add(ProcessInput(input, ref tempIndex));
        }

        ArrayList dividerPacket1 = new ArrayList() { new ArrayList() { 2 } };
        ArrayList dividerPacket2 = new ArrayList() { new ArrayList() { 6 } };

        packets.Add(dividerPacket1);
        packets.Add(dividerPacket2);

        packets.Sort((x, y) => IsInRightOrder(x, y));

        return (packets.IndexOf(dividerPacket1) + 1) * (packets.IndexOf(dividerPacket2) + 1);
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
            }
            else if (currChar == ']')
            {
                break;
            }
            else if (currChar == ',')
            {
                continue;
            }
            else
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

    private static int IsInRightOrder(ArrayList packet1, ArrayList packet2)
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
                    return -1;
                }
                else if ((int)value1 > (int)value2)
                {
                    return 1;
                }
            }
            else
            {
                ArrayList arrayList1 = (value1?.GetType() == typeof(int)) ? new() { value1 } : (ArrayList)value1!;
                ArrayList arrayList2 = (value2?.GetType() == typeof(int)) ? new() { value2 } : (ArrayList)value2!;

                int isInRightOrder = IsInRightOrder(arrayList1, arrayList2);
                if (isInRightOrder != 0)
                {
                    return isInRightOrder;
                }
            }
        }

        if (packet1.Count < packet2.Count)
        {
            return -1;
        }
        else if (packet1.Count > packet2.Count)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
}
