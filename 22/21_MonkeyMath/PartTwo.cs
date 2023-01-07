namespace AdventOfCode._22._21_MonkeyMath;

internal static class PartTwo
{
    // T = O(n) | S = O(n)
    // Where n is the number of monkeys
    public static long Solve()
    {
        Dictionary<string, Monkey> monkeys = new();
        Dictionary<string, string> monkeyToParent = new();
        foreach (string input in File.ReadAllLines("../../../Input/prod.txt"))
        {
            string[] processedInput = input.Split(new char[] { ':', ' ' }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            var name = processedInput[0];
            if (processedInput.Length == 2)
            {
                monkeys[name] = new Monkey(name, Convert.ToInt32(processedInput[1]));
            }
            else
            {
                monkeys[name] = new Monkey(name, processedInput[1], processedInput[3], (Operation)processedInput[2][0]);
                monkeyToParent[processedInput[1]] = name;
                monkeyToParent[processedInput[3]] = name;
            }
        }

        ProcessMonkeyNumbers("root", monkeys);
        return GetMonkeyActualNumber("humn", monkeyToParent, monkeys);
    }

    private enum Operation
    {
        Plus = '+',
        Minus = '-',
        Multiply = '*',
        Divide = '/'
    }

    private class Monkey
    {
        public string Name;
        public long? Number;
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

    private static long ProcessMonkeyNumbers(string monkeyName, IDictionary<string, Monkey> monkeys)
    {
        Monkey currMonkey = monkeys[monkeyName];

        if (monkeys[monkeyName].Number != null)
        {
            return currMonkey.Number.Value;
        }

        long child1Number = ProcessMonkeyNumbers(currMonkey.child1Name, monkeys);
        long child2Number = ProcessMonkeyNumbers(currMonkey.child2Name, monkeys);

        currMonkey.Number = currMonkey.Operation.Value switch
        {
            Operation.Plus => child1Number + child2Number,
            Operation.Minus => child1Number - child2Number,
            Operation.Multiply => child1Number * child2Number,
            Operation.Divide => child1Number / child2Number,
            _ => throw new Exception("Operation not handled")
        };

        return currMonkey.Number.Value;
    }

    private static long GetMonkeyActualNumber(string monkeyName, IDictionary<string, string> monkeyToParent, IDictionary<string, Monkey> monkeys)
    {
        Monkey parentMonkey = monkeys[monkeyToParent[monkeyName]];
        Monkey currMonkey = monkeys[monkeyName];

        if (parentMonkey.Name == "root")
        {
            return (parentMonkey.child1Name == currMonkey.Name) ? monkeys[parentMonkey.child2Name].Number.Value : monkeys[parentMonkey.child1Name].Number.Value;
        }

        long parentMonkeyActualNumber = GetMonkeyActualNumber(parentMonkey.Name, monkeyToParent, monkeys);

        if (parentMonkey.child1Name == currMonkey.Name)
        {
            Monkey child2Monkey = monkeys[parentMonkey.child2Name];

            return parentMonkey.Operation.Value switch
            {
                Operation.Plus => parentMonkeyActualNumber - child2Monkey.Number.Value,
                Operation.Minus => parentMonkeyActualNumber + child2Monkey.Number.Value,
                Operation.Multiply => parentMonkeyActualNumber / child2Monkey.Number.Value,
                Operation.Divide => parentMonkeyActualNumber * child2Monkey.Number.Value,
                _ => throw new Exception("Operation not handled")
            };
        }

        Monkey child1Monkey = monkeys[parentMonkey.child1Name];

        return parentMonkey.Operation.Value switch
        {
            Operation.Plus => parentMonkeyActualNumber - child1Monkey.Number.Value,
            Operation.Minus => child1Monkey.Number.Value - parentMonkeyActualNumber,
            Operation.Multiply => parentMonkeyActualNumber / child1Monkey.Number.Value,
            Operation.Divide => child1Monkey.Number.Value / parentMonkeyActualNumber,
            _ => throw new Exception("Operation not handled")
        };
    }
}
