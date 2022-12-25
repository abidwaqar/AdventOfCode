namespace AdventOfCode._22._21_MonkeyMath;

internal static class PartOne
{
    // T = O(n) | S = O(n)
    // Where n is the number of monkeys
    public static long Solve()
    {
        Dictionary<string, Monkey> monkeys = new();
        foreach (string input in File.ReadAllLines("../../../input.txt"))
        {
            string[] processedInput = input.Split(new char[] { ':', ' ' }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            
            var name = processedInput[0];
            if (processedInput.Length == 2)
            {
                monkeys[name] = new Monkey(name, Convert.ToInt32(processedInput[1]));
            } else
            {
                monkeys[name] = new Monkey(name, processedInput[1], processedInput[3], (Operation)processedInput[2][0]);
            }
        }

        return GetMonkeyNumber("root", monkeys);
    }

    private enum Operation
    {
        Plus = '+',
        Minus = '-',
        Multiply = '*',
        Divide =  '/'
    }

    private class Monkey
    {
        public string Name;
        public int? Number;
        public string? child1Name;
        public string? child2Name;
        public Operation? Operation;

        public Monkey(string name, int number)
        {
            Name = name;
            Number = number;
            this.child1Name = null;
            this.child2Name = null;
            Operation = null;
        }

        public Monkey(string name, string child1Name, string child2Name, Operation operation)
        {
            Name = name;
            Number = null;
            this.child1Name = child1Name;
            this.child2Name = child2Name;
            Operation = operation;
        }
    }

    private static long GetMonkeyNumber(string monkeyName, IDictionary<string, Monkey> monkeys)
    {
        Monkey currMonkey = monkeys[monkeyName];

        if (monkeys[monkeyName].Number != null)
        {
            return currMonkey.Number.Value;
        }

        long child1Number = GetMonkeyNumber(currMonkey.child1Name, monkeys);
        long child2Number = GetMonkeyNumber(currMonkey.child2Name, monkeys);

        return currMonkey.Operation.Value switch
        {
            Operation.Plus => child1Number + child2Number,
            Operation.Minus => child1Number - child2Number,
            Operation.Multiply => child1Number * child2Number,
            Operation.Divide => child1Number / child2Number,
            _ => throw new Exception("Operation not handled")
        };
    }
}
