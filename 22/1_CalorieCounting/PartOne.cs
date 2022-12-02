namespace AdventOfCode._22._1_CalorieCounting
{
    internal static class PartOne
    {
        // T = O(n) | S = O(1)
        public static int Solve()
        {
            int maxCalories = 0;
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

                maxCalories = Math.Max(maxCalories, currentCalories);
            }

            return maxCalories;
        }

        // T = O(n * log(k)) | S = O(k)
        // In this case k is constant so we can say T = O(n) | S = O(1)
        // Pattern used https://emre.me/coding-patterns/top-k-numbers/
        public static int PartTwo()
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
