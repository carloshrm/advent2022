﻿namespace Solutions
{
    internal class Day2 : Solution<int, int>
    {
        public Day2() : base(2) { }

        protected override int partOne()
        {
            int score = 0;
            foreach (var match in input.data)
            {
                int elfHand = match[0] - '@';
                int myHand = match[2] - 'W';

                score += myHand;
                if (myHand == elfHand)
                    score += 3;
                else
                {
                    if ((myHand == 1 && elfHand == 3) ||
                        (myHand == 2 && elfHand == 1) ||
                        (myHand == 3 && elfHand == 2))
                    {
                        score += 6;
                    }
                }
            }
            return score;
        }

        protected override int partTwo()
        {
            int[] matchingWin = { 2, 3, 1 };
            int[] matchingLoss = { 3, 1, 2 };
            int score = 0;
            foreach (var match in input.data)
            {
                int elfHand = match[0] - '@';
                int outcome = match[2] - 'W';

                score += (outcome - 1) * 3;

                if (outcome == 3)
                    score += matchingWin[elfHand - 1];
                else if (outcome == 1)
                    score += matchingLoss[elfHand - 1];
                else
                    score += elfHand;
            }
            return score;
        }
    }
}