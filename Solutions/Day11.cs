using System.Text.RegularExpressions;

using Solutions;

namespace Advent2022
{
    internal class Day11 : Solution<int>
    {
        private IList<Monkey> _monkeys;

        public Day11() : base(11)
        {
            _monkeys = new List<Monkey>();
            foreach (var m in MonkeyMaker.readMonkey(input.data))
                _monkeys.Add(MonkeyMaker.parseMonkey(m));
        }

        protected override int partOne()
        {
            for (int i = 0; i < 20; i++)
            {
                int j = 0;
                foreach (var m in _monkeys)
                {
                    m.takeTurn(_monkeys);
                    j++;
                }
            }
            return new List<Monkey>(_monkeys).OrderByDescending(m => m.inpCount).Take(2).Aggregate(1, (prod, mky) => mky.inpCount * prod);
        }
        protected override int partTwo() => base.partTwo();
    }

    internal class PackItem
    {
        public int worry;
        public PackItem(int worry) => this.worry = worry;
    }

    internal class Monkey
    {
        private IList<PackItem> inventory;
        private Action<PackItem> inspect;
        private Func<PackItem, int> decide;
        public int inpCount { get; set; }

        public Monkey(IList<PackItem> inv, Action<PackItem> inspect, Func<PackItem, int> decide)
        {
            this.inspect = inspect;
            this.decide = decide;
            inventory = inv;
        }

        public void takeTurn(IList<Monkey> buddies)
        {
            foreach (var item in new List<PackItem>(inventory))
            {
                inspect(item);
                item.worry /= 3;
                int throwTo = decide(item);
                buddies[throwTo].catchItem(item);
                inventory.Remove(item);
                inpCount++;
            }
        }

        public void catchItem(PackItem item) => inventory.Add(item);
    }

    internal static class MonkeyMaker
    {
        public static IEnumerable<string[]> readMonkey(string[] info)
        {
            int j = 0;
            for (int i = 0; i < info.Length; i++)
            {
                if (info[i] == "")
                {
                    yield return info[j..i];
                    j = i + 1;
                }
            }
        }

        public static Monkey parseMonkey(string[] source)
        {
            var items = Regex.Matches(source[1], @"\d+").Select(m => new PackItem(int.Parse(m.Value))).ToList();
            int test = int.Parse(source[3][source[3].LastIndexOf(" ")..]);
            var buddies = Regex.Matches(source[4] + source[5], @"\d+").Select(b => int.Parse(b.Value));
            return new Monkey(
                items,
                inspectionSetup(source[2]),
                decisionSetup(test, buddies));
        }

        private static Action<PackItem> inspectionSetup(string info)
        {
            var ins = info.Split(" ");
            var op = ins[ins.Length - 2];

            int.TryParse(ins[ins.Length - 1], out int n);

            return (PackItem item) =>
            {
                int mult = n == 0 ? item.worry : n;
                item.worry = op switch
                {
                    "+" => item.worry + mult,
                    "*" => item.worry * mult,
                    _ => 0
                };
            };
        }

        private static Func<PackItem, int> decisionSetup(int div, IEnumerable<int> buddies)
        {
            return (PackItem item) =>
            {
                return buddies.ElementAt(item.worry % div == 0 ? 0 : 1);
            };
        }
    }
}