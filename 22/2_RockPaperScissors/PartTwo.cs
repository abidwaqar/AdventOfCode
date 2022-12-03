namespace AdventOfCode._22._2_RockPaperScissors
{
    internal static class PartTwo
    {
        private readonly static IDictionary<char, int> outcomeScores = new Dictionary<char, int>
        {
            {'L', 0 },
            {'D', 3 },
            {'W', 6 }
        };

        private readonly static IDictionary<char, int> selectionScores = new Dictionary<char, int>
        {
            {'R', 1 },
            {'P', 2 },
            {'S', 3 }
        };

        private readonly static IDictionary<char, char> defeats = new Dictionary<char, char>
        {
            {'R', 'S' },
            {'S', 'P' },
            {'P', 'R' }
        };

        private readonly static IDictionary<char, char> defeatsFrom = new Dictionary<char, char>
        {
            {'R', 'P' },
            {'S', 'R' },
            {'P', 'S' }
        };

        // T = O(n) | S = O(1)
        public static int Solve()
        {
            int finalScore = 0;
            foreach (var input in File.ReadAllLines("../../../input.txt"))
            {
                var processedInput = input.Split(' ');
                var opponentSelection = MapSelection(processedInput[0][0]);
                var outcome = MapOutcome(processedInput[1][0]);

                finalScore += OurSelectionScore(opponentSelection, outcome) + OutcomeScore(outcome);
            }

            return finalScore;
        }

        private static char MapSelection(char selection)
        {
            if (selection == 'A')
            {
                return 'R';
            } else if (selection == 'B')
            {
                return 'P';
            } else if (selection == 'C')
            {
                return 'S';
            }

            throw new Exception("Mapping for input nor found");
        }

        private static char MapOutcome(char outcome)
        {
            if (outcome == 'X')
            {
                return 'L';
            }
            else if (outcome == 'Y')
            {
                return 'D';
            }
            else if (outcome == 'Z')
            {
                return 'W';
            }

            throw new Exception("Mapping for input nor found");
        }

        private static int OutcomeScore(char outcome)
        {
            return outcomeScores[outcome];
        }

        private static int OurSelectionScore(char opponentSelection, char outcome)
        {
            if (outcome == 'L')
            {
                return selectionScores[defeats[opponentSelection]];
            } else if (outcome == 'D')
            {
                return selectionScores[opponentSelection];
            } else if (outcome == 'W')
            {
                return selectionScores[defeatsFrom[opponentSelection]];
            }

            throw new Exception("Invalid input");
        }
    }
}
