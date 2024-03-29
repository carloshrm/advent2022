﻿namespace Solutions
{
    internal class Day3 : Solution<int, int>
    {
        public Day3() : base(3) { }

        private int getPriority(char item) => char.IsUpper(item) ? item - 'A' + 27 : item - ('a' - 1);

        protected override int partOne()
        {
            int prioritySum = 0;
            foreach (var sack in input.data)
            {
                var duplicatedItem = sack[0..(sack.Length / 2)].Intersect(sack[(sack.Length / 2)..]).First();
                prioritySum += getPriority(duplicatedItem);
            }
            return prioritySum;
        }

        protected override int partTwo()
        {
            int prioritySum = 0;
            foreach (var group in input.data.Chunk(3))
            {
                prioritySum += getPriority(group[0].Intersect(group[1]).Intersect(group[2]).First());
            }
            return prioritySum;
        }
    }
}