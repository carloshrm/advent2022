using System.Text.RegularExpressions;

namespace Solutions
{
    internal class Day15 : Solution<int, int>
    {
        public Day15() : base(15)
        {
            //input = (input.example, input.example);

        }

        protected override int partOne()
        {
            var field = new List<ElfSensor>();

            foreach (var i in input.data)
            {
                var items = Regex.Matches(i, @"-?\d+").Select(val => int.Parse(val.Value));
                field.Add(new ElfSensor((items.ElementAt(0), items.ElementAt(1)), (items.ElementAt(2), items.ElementAt(3))));
            }
            int targetY = 2000000;

            int count = 0;
            var lowEdge = field.Min(s => s.pos.x - s.range);
            var highEdge = field.Max(s => s.pos.x + s.range);

            var existingBeacon = field.First().beacon;
            for (int i = lowEdge; i < highEdge; i++)
            {
                foreach (var sens in field)
                {
                    if (Math.Abs(sens.pos.x - i) + Math.Abs(sens.pos.y - targetY) <= sens.range)
                    {
                        if (sens.beacon.x == i && sens.beacon.y == targetY)
                        {
                            if (existingBeacon != sens.beacon)
                                existingBeacon = sens.beacon;
                        }
                        else
                        {
                            count++;
                            break;
                        }
                    }
                }
            }
            return count;
        }

        protected override int partTwo() => base.partTwo();
    }

    internal class ElfSensor
    {
        public (int x, int y) pos;
        public (int x, int y) beacon;
        public int range;

        public ElfSensor((int x, int y) pos, (int x, int y) beacon)
        {
            this.pos = pos;
            this.beacon = beacon;
            range = Math.Abs(pos.x - beacon.x) + Math.Abs(pos.y - beacon.y);
        }
    }


}