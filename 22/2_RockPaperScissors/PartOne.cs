namespace AdventOfCode._22._2_RockPaperScissors
{
    internal static class PartOne
    {
        private readonly static int loseScore = 0;
        private readonly static int drawScore = 3;
        private readonly static int winScore = 6;
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

        // T = O(n) | S = O(1)
        public static int Solve()
        {
            int finalScore = 0;
            foreach (var input in File.ReadAllLines("../../../Input/prod.txt"))
            {
                var processedInput = input.Split(' ');
                var opponentSelection = MapSelection(processedInput[0][0]);
                var ourSelection = MapSelection(processedInput[1][0]);

                finalScore += OurSelectionScore(ourSelection) + OutcomeScore(opponentSelection, ourSelection);
            }

            return finalScore;
        }

        private static char MapSelection(char selection)
        {
            if (selection == 'X' || selection == 'A')
            {
                return 'R';
            } else if (selection == 'Y' || selection == 'B')
            {
                return 'P';
            } else if (selection == 'Z' || selection == 'C')
            {
                return 'S';
            }

            throw new Exception("Mapping for input nor found");
        }

        private static int OutcomeScore(char opponentSelection, char ourSelection)
        {
            if (opponentSelection == ourSelection)
            {
                return drawScore;
            } else if (defeats[opponentSelection] == ourSelection)
            {
                return loseScore;
            } else if (defeats[ourSelection] == opponentSelection)
            {
                return winScore;
            }

            throw new Exception("Invalid input");
        }

        private static int OurSelectionScore(char ourSelection)
        {
            return selectionScores[ourSelection];
        }
    }
}
