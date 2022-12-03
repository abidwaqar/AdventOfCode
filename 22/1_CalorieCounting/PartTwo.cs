namespace AdventOfCode._22._1_CalorieCounting
{
    internal static class PartTwo
    {
        public static int Solve()
        {
            var minHeap = new PriorityQueue<int, int>();
            var k = 3;
            var currentCalories = 0;
            foreach (var input in File.ReadAllLines("../../../input.txt"))
            {
                if (input == string.Empty)
                {
                    minHeap.Enqueue(currentCalories, currentCalories);
                    if (minHeap.Count > k)
                    {
                        minHeap.Dequeue();
                    }

                    currentCalories = 0;
                } else
                {
                    currentCalories += Convert.ToInt32(input);
                }
            }

            minHeap.Enqueue(currentCalories, currentCalories);
            if (minHeap.Count > k)
            {
                minHeap.Dequeue();
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
