namespace Solutions
{
    internal class Day4 : Solution<int, int>
    {
        public Day4() : base(4)
        {
        }

        private (int start, int end) parsePairValues(string pair)
        {
            int splitIndex = pair.IndexOf('-');
            int.TryParse(pair[..splitIndex], out int fpStart);
            int.TryParse(pair[(splitIndex + 1)..], out int fpEnd);
            return (fpStart, fpEnd);
        }

        protected override int partOne()
        {
            int count = 0;
            foreach (var set in input.data)
            {

                int splitI = set.IndexOf(',');

                var firstPair = parsePairValues(set[..splitI]);
                var secondPair = parsePairValues(set[(splitI + 1)..]);

                if (firstPair.start == secondPair.start)
                    count++;
                else
                {
                    if (firstPair.start < secondPair.start)
                    {
                        if (firstPair.end >= secondPair.end)
                            count++;
                    }
                    else
                    {
                        if (secondPair.end >= firstPair.end)
                            count++;
                    }
                }
            }
            return count;
        }

        protected override int partTwo()
        {
            int count = 0;
            foreach (var set in input.data)
            {
                int splitI = set.IndexOf(',');

                var firstPair = parsePairValues(set[..splitI]);
                var secondPair = parsePairValues(set[(splitI + 1)..]);


                if (firstPair.start == secondPair.start)
                    count++;
                else
                {
                    if (firstPair.start < secondPair.start)
                    {
                        if (firstPair.end >= secondPair.start)
                            count++;
                    }
                    else
                    {
                        if (secondPair.end >= firstPair.start)
                            count++;
                    }
                }
            }
            return count;
        }
    }
}