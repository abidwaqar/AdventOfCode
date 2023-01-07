namespace AdventOfCode._22._11_MonkeyInTheMiddle;

internal static class PartTwo
{
    // T = O(m * n * o) | S = O(m * n)
    // Where m is monkey count, n is items count, and o is rounds count
    public static long Solve()
    {
        StringSplitOptions stringSplitOptions = StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries;
        IList<Monkey> monkeys = new List<Monkey>();
        Monkey currentMonkey = new Monkey(0);
        long superModulo = 1;
        foreach (string input in File.ReadAllLines("../../../Input/prod.txt"))
        {
            if (input.Contains("Monkey"))
            {
                int monkeyId = Convert.ToInt32(input.Split(new string[] { "Monkey", ":" }, stringSplitOptions)[0]);
                currentMonkey = new Monkey(monkeyId);

                monkeys.Add(currentMonkey);
            }
            else if (input.Contains("Starting items:"))
            {
                string[] itemsWorryLevels = input.Split(new string[] { "Starting items:", "," }, stringSplitOptions);
                foreach (string item in itemsWorryLevels)
                {
                    currentMonkey.ItemsWorryLevels.Add(Convert.ToInt64(item));
                }
            }
            else if (input.Contains("Operation: new = old"))
            {
                string[] inputArrUnprocessed = input.Split(new string[] { "Operation: new = old", " " }, stringSplitOptions);
                currentMonkey.Operator = (Operator)inputArrUnprocessed[0][0];
                if (inputArrUnprocessed[1] == "old")
                {
                    currentMonkey.operandIsOldValue = true;
                }
                else
                {
                    currentMonkey.operandIsOldValue = false;
                    currentMonkey.Operand = Convert.ToInt32(inputArrUnprocessed[1]);
                }
            }
            else if (input.Contains("Test: divisible by"))
            {
                string divisibleBy = input.Split("Test: divisible by", stringSplitOptions)[0];
                currentMonkey.DivisibleBy = Convert.ToInt32(divisibleBy);
                superModulo *= currentMonkey.DivisibleBy;
            }
            else if (input.Contains("If true: throw to monkey"))
            {
                string toMonkeyIfDivisible = input.Split("If true: throw to monkey", stringSplitOptions)[0];
                currentMonkey.ToMonkeyIfDivisible = Convert.ToInt32(toMonkeyIfDivisible);
            }
            else if (input.Contains("If false: throw to monkey"))
            {
                string toMonkeyIfNotDivisible = input.Split("If false: throw to monkey", stringSplitOptions)[0];
                currentMonkey.ToMonkeyIfNotDivisible = Convert.ToInt32(toMonkeyIfNotDivisible);
            }
            else if (input == string.Empty)
            {
                continue;
            }
            else
            {
                throw new Exception("Invalid Input");
            }
        }

        for (int i = 0; i < 10000; ++i)
        {
            foreach (Monkey monkey in monkeys)
            {
                monkey.ProcessRound(monkeys[monkey.ToMonkeyIfDivisible], monkeys[monkey.ToMonkeyIfNotDivisible], superModulo);
            }
        }

        int mostActiveMonkeysCount = 2;
        PriorityQueue<long, long> mostActiveMonkeysItemInspectionCount = new PriorityQueue<long, long>();
        foreach (Monkey monkey in monkeys)
        {
            mostActiveMonkeysItemInspectionCount.Enqueue(monkey.ItemsInspectedCount, monkey.ItemsInspectedCount);
            if (mostActiveMonkeysItemInspectionCount.Count > mostActiveMonkeysCount)
            {
                mostActiveMonkeysItemInspectionCount.Dequeue();
            }
        }

        long monkeyBusiness = 1;
        while (mostActiveMonkeysItemInspectionCount.Count != 0)
        {
            monkeyBusiness *= mostActiveMonkeysItemInspectionCount.Dequeue();
        }

        return monkeyBusiness;
    }

    private enum Operator
    {
        Plus = '+',
        Minus = '-',
        Multiply = '*'
    }

    private class Monkey
    {
        public int Id;
        public IList<long> ItemsWorryLevels;
        public Operator Operator;
        public bool operandIsOldValue;
        public int Operand;
        public int DivisibleBy;
        public int ToMonkeyIfDivisible;
        public int ToMonkeyIfNotDivisible;
        public long ItemsInspectedCount;

        public Monkey(int id)
        {
            this.Id = id;
            this.ItemsWorryLevels = new List<long>();
        }

        public void ProcessRound(Monkey toMonkeyReferenceIfDivisible, Monkey toMonkeyReferenceIfNotDivisible, long superModulo)
        {
            for (int i = ItemsWorryLevels.Count - 1; i >= 0; --i)
            {
                long oldItemWorryLevel = ItemsWorryLevels[i];
                long newItemWorryLevel = (Operator) switch
                {
                    Operator.Plus => oldItemWorryLevel + (operandIsOldValue ? oldItemWorryLevel : Operand),
                    Operator.Minus => oldItemWorryLevel - (operandIsOldValue ? oldItemWorryLevel : Operand),
                    Operator.Multiply => oldItemWorryLevel * (operandIsOldValue ? oldItemWorryLevel : Operand),
                    _ => throw new Exception("Operator not supported")
                };

                newItemWorryLevel %= superModulo;

                ItemsWorryLevels.RemoveAt(i);
                if (newItemWorryLevel % DivisibleBy == 0)
                {
                    toMonkeyReferenceIfDivisible.ItemsWorryLevels.Add(newItemWorryLevel);
                }
                else
                {
                    toMonkeyReferenceIfNotDivisible.ItemsWorryLevels.Add(newItemWorryLevel);
                }

                ++ItemsInspectedCount;
            }
        }
    }
}
