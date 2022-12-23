using Solutions;

namespace Advent2022
{
    internal class Day13 : Solution<int, int>
    {
        private ElfDistressPacket[] dividerPackets = { new ElfDistressPacket("[[2]]"), new ElfDistressPacket("[[6]]") };
        private readonly List<ElfDistressPacket> distressSignal;

        public Day13() : base(13)
        {
            distressSignal = new List<ElfDistressPacket>();
        }

        protected override int partOne()
        {
            int result = 0;
            for (int ln = 0; ln < input.data.Length; ln += 3)
            {
                var first = new ElfDistressPacket(input.data[ln]);
                var second = new ElfDistressPacket(input.data[ln + 1]);

                distressSignal.Add(first);
                distressSignal.Add(second);

                if (first.CompareTo(second) == 1)
                    result += (ln / 3) + 1;

            }
            return result;
        }

        protected override int partTwo()
        {
            distressSignal.AddRange(dividerPackets);
            distressSignal.Sort((a, b) => b.CompareTo(a));

            int result = 1;
            foreach (var pckt in dividerPackets)
                result *= distressSignal.IndexOf(pckt) + 1;

            return result;
        }
    }
    internal class ElfDistressPacket : IComparable
    {
        private List<object> values;

        public ElfDistressPacket(string rawData)
        {
            values = ElfPacketHelper.readPacket(rawData);
        }

        public int CompareTo(object? obj) => ElfPacketHelper.comparePackets(this.values, ((ElfDistressPacket)obj).values);
    }

    internal class ElfPacketHelper
    {
        public static int comparePackets(object a, object b)
        {
            IList<object> aList = a as IList<object>;
            IList<object> bList = b as IList<object>;

            int i = 0, j = 0;
            int result = 0;
            while (i < aList.Count && j < bList.Count)
            {
                var ln = aList[i];
                var rn = bList[j];
                if (ln is int && rn is int)
                {
                    if ((int)ln != (int)rn)
                        return ((int)ln < (int)rn) ? 1 : -1;
                }
                else
                {
                    if (ln is not int && rn is int)
                    {
                        rn = new List<object>() { rn };
                    }

                    if (rn is not int && ln is int)
                    {
                        ln = new List<object>() { ln };
                    }
                    result = comparePackets((List<object>)ln, (List<object>)rn);
                }
                if (result != 0)
                    return result;

                i++;
                j++;
            }
            if (i < aList.Count || j < bList.Count)
                return (aList.Count < bList.Count) ? 1 : -1;
            else
                return 0;
        }

        public static List<object> readPacket(string source)
        {
            var parsedPacket = new List<object>();
            var listHolder = new Stack<List<object>>();
            var currentNumber = string.Empty;
            for (int i = 0; i < source.Length; i++)
            {
                if (char.IsDigit(source[i]))
                    currentNumber += source[i];
                else if (source[i] == '[')
                {
                    listHolder.Push(parsedPacket);
                    parsedPacket = new List<object>();
                }
                else
                {
                    if (source[i] == ',' || source[i] == ']')
                    {
                        if (int.TryParse(currentNumber, out int number))
                            parsedPacket.Add(number);
                        currentNumber = string.Empty;
                    }
                    if (source[i] == ']')
                    {
                        var innerList = parsedPacket;
                        parsedPacket = listHolder.Pop();
                        parsedPacket.Add(innerList);
                    }
                }
            }
            return parsedPacket;
        }
    }
}