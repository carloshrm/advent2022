namespace Namespace
{
    internal class Day6 : Solution<int>
    {
        public Day6() : base(6) { }

        protected override int partOne()
        {
            return getStartByMarkerLength(3);
        }

        protected override int partTwo()
        {
            return getStartByMarkerLength(14);
        }

        private int getStartByMarkerLength(int length)
        {
            var bucket = new Queue<char>();
            for (int i = 0; i < input.data[0].Length; i++)
            {
                bucket.Enqueue(input.data[0][i]);
                if (bucket.Count == length)
                {
                    if (bucket.Distinct().Count() == length)
                        return i + 1;
                    else
                        bucket.Dequeue();
                }
            }
            return -1;
        }
    }
}