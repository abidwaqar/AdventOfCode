namespace AdventOfCode._22._4_CampCleanup
{
    internal static class PartOne
    {
        // T = O(n) | S = O(1)
        public static int Solve()
        {
            int result = 0;
            foreach (var input in File.ReadAllLines("../../../input.txt"))
            {
                var sections = input.Split(',');
                
                var range1 = sections[0].Split("-");
                var range2 = sections[1].Split("-");
                
                var range1Start = Convert.ToInt32(range1[0]);
                var range1End = Convert.ToInt32(range1[1]);
                var range2Start = Convert.ToInt32(range2[0]);
                var range2End = Convert.ToInt32(range2[1]);

                if ((range1Start <= range2Start && range1End >= range2End) || (range2Start <= range1Start && range2End >= range1End))
                {
                    ++result;
                }
            }

            return result;
        }
    }
}
