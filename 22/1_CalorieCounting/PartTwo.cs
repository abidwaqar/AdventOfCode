namespace AdventOfCode._22._1_CalorieCounting
{
    internal static class PartTwo
    {
        public static int Solve()
        {
            int k = 3;

            var minHeap = new PriorityQueue<int, int>();
            while (true)
            {
                string? input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                {
                    break;
                }

                int currentCalories = Convert.ToInt32(input);
                while (true)
                {
                    input = Console.ReadLine();
                    if (string.IsNullOrEmpty(input))
                    {
                        break;
                    }

                    currentCalories += Convert.ToInt32(input);
                }

                minHeap.Enqueue(currentCalories, currentCalories);
                if (minHeap.Count > k)
                {
                    minHeap.Dequeue();
                }
            }

            var top3Calories = 0;
            while (minHeap.Count != 0)
            {
                top3Calories += minHeap.Dequeue();
            }

            return top3Calories;
        }
    }
}
