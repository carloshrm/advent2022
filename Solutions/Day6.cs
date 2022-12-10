namespace Solutions
{
    internal class Day6 : Solution<int>
    {
        public Day6() : base(6) { }

        protected override int partOne()
        {
            return findByMarker(4);
        }

        protected override int partTwo()
        {
            return findByMarker(14);
        }

        private int findByMarker(int markerLength)
        {
            for (int i = markerLength; i < input.data[0].Length - (markerLength - 1); i++)
            {
                if (input.data[0][(i - markerLength)..i].Distinct().Count() == markerLength)
                    return i;
            }
            return -1;
        }
    }
}