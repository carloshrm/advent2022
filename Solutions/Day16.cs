using System.Text;

using Solutions;

namespace Advent2022
{
    internal class Day16 : Solution<int, int>
    {
        private Dictionary<string, ElfValve> valves { get; set; }
        public int timeLeft { get; set; }

        public Day16() : base(16)
        {
            valves = new Dictionary<string, ElfValve>();
            input = (input.example, input.example);
            timeLeft = 30;
        }

        protected override int partOne()
        {
            foreach (var ln in input.data)
            {
                var info = ln.Split(";");
                int.TryParse(info[0].AsSpan(info[0].IndexOf("=") + 1), out int flowRate);
                var newValve = new ElfValve(flowRate);
                readValves(info[1]).ForEach(v => newValve.setLink(v));
                valves.Add(readValves(info[0])[0], newValve);
            }

            int releasedPressure = 0;
            var openValves = new Queue<ElfValve>();
            openValves.Enqueue(valves.First().Value);


            return releasedPressure;
        }

        protected override int partTwo()
        {
            return base.partTwo();
        }

        private List<string> readValves(string input)
        {
            var valveList = new List<string>();
            var currentValve = new StringBuilder();
            for (int i = 1; i < input.Length; i++)
            {
                char ch = input[i];
                if (char.IsUpper(ch))
                    currentValve.Append(ch);

                if (ch == ' ' && currentValve.Length != 0)
                {
                    valveList.Add(currentValve.ToString());
                    currentValve.Clear();
                }
            }
            if (currentValve.Length != 0) valveList.Add(currentValve.ToString());
            return valveList;
        }

    }

    internal class ElfValve
    {
        public int flowRate { get; set; }
        public List<string> links { get; set; }
        public bool isClosed { get; set; }

        public ElfValve(int rate)
        {
            links = new List<string>();
            flowRate = rate;
            isClosed = true;
        }

        public void setLink(string targetId)
        {
            links.Add(targetId);
        }

        public void open()
        {
            isClosed = false;
        }
    }
}