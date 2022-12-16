using System.Text.RegularExpressions;

using Solutions;

namespace Advent2022
{
    internal class Day11 : Solution<int, ulong>
    {
        public Day11() : base(11) { }

        protected override int partOne()
        {
            var monkeys = new List<Monkey>();
            foreach (var m in MonkeyService.readMonkey(input.data))
                monkeys.Add(MonkeyService.parseMonkey(m));
            for (int i = 0; i < 20; i++)
            {
                foreach (var m in monkeys)
                    m.takeTurn(monkeys);
            }
            return new List<Monkey>(monkeys).OrderByDescending(m => m.inpCount).Take(2).Aggregate(1, (prod, mky) => (int)mky.inpCount * prod);
        }

        protected override ulong partTwo()
        {
            var monkeys = new List<Monkey>();
            foreach (var m in MonkeyService.readMonkey(input.data))
                monkeys.Add(MonkeyService.parseMonkey(m));
            var globalMod = MonkeyService.getGlobalMod(monkeys);
            for (int i = 0; i < 10000; i++)
            {
                foreach (var m in monkeys)
                    m.takeTurn(monkeys, globalMod);
            }
            return new List<Monkey>(monkeys).OrderByDescending(m => m.inpCount).Take(2).Aggregate((ulong)1, (prod, mky) => mky.inpCount * prod);
        }
    }

    internal class PackItem
    {
        public ulong worry;
        public PackItem(ulong worry) => this.worry = worry;
    }

    internal class Monkey
    {
        private IList<PackItem> inventory;
        private Action<PackItem> inspect;
        public int divisor;
        public int[] buddies;
        public ulong inpCount { get; set; }

        public Monkey(IList<PackItem> inv, Action<PackItem> insp, int div, IEnumerable<int> buddies)
        {
            inspect = insp;
            inventory = inv;
            divisor = div;
            this.buddies = buddies.ToArray();
        }

        public void takeTurn(IList<Monkey> pack, int worryMod = 0)
        {
            foreach (var item in new List<PackItem>(inventory))
            {
                inspect(item);
                item.worry = worryMod == 0 ? item.worry / 3 : item.worry % (ulong)worryMod;
                pack[buddies[(int)item.worry % divisor == 0 ? 0 : 1]].catchItems(item);
                inventory.Remove(item);
                inpCount++;
            }
        }

        public void catchItems(PackItem i) => inventory.Add(i);
    }

    internal static class MonkeyService
    {
        public static int getGlobalMod(IEnumerable<Monkey> pack) => pack.Aggregate(1, (p, m) => m.divisor * p);

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
            var items = Regex.Matches(source[1], @"\d+").Select(m => new PackItem(ulong.Parse(m.Value))).ToList();
            int divi = int.Parse(source[3][source[3].LastIndexOf(" ")..]);
            var buddies = Regex.Matches(source[4] + source[5], @"\d+").Select(b => int.Parse(b.Value));
            return new Monkey(items, inspectionSetup(source[2]), divi, buddies);
        }

        private static Action<PackItem> inspectionSetup(string info)
        {
            var ins = info.Split(" ");
            var op = ins[ins.Length - 2];

            ulong.TryParse(ins[ins.Length - 1], out ulong n);

            return (PackItem item) =>
            {
                ulong mult = n == 0 ? item.worry : n;
                item.worry = op switch
                {
                    "+" => checked(item.worry + mult),
                    "*" => checked(item.worry * mult),
                    _ => 0
                };
            };
        }
    }
}