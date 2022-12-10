using System.Text;
using System.Text.RegularExpressions;

namespace Solutions
{
    internal class Day5 : Solution<string>
    {
        private int rowCount;
        private IEnumerable<string> crateInfo;
        public Day5() : base(5)
        {
            crateInfo = input.data.TakeWhile(line => !string.IsNullOrEmpty(line));
            rowCount = int.Parse(crateInfo.Last().Max().ToString());
        }

        protected override string partOne()
        {
            var crateStacks = setupCrateStacks();

            for (int i = crateStacks.Count() + 1; i < input.data.Length; i++)
            {
                var match = Regex.Matches(input.data[i], @"\d+").Select(m => int.Parse(m.Value));

                int count = match.First();
                while (count-- > 0)
                    crateStacks.ElementAt(match.Last() - 1).Push(crateStacks.ElementAt(match.ElementAt(1) - 1).Pop());
            }
            return readResult(crateStacks);
        }

        protected override string partTwo()
        {
            var crateStacks = setupCrateStacks();

            for (int i = crateStacks.Count() + 1; i < input.data.Length; i++)
            {
                var match = Regex.Matches(input.data[i], @"\d+").Select(m => int.Parse(m.Value));

                int count = match.First();
                var temp = new Stack<char>();
                while (count-- > 0)
                    temp.Push(crateStacks.ElementAt(match.ElementAt(1) - 1).Pop());

                while (temp.Any())
                    crateStacks.ElementAt(match.Last() - 1).Push(temp.Pop());
            }

            return readResult(crateStacks);
        }

        private IEnumerable<Stack<char>> setupCrateStacks()
        {
            var stacks = new Stack<char>[rowCount].Select(_ => new Stack<char>()).ToArray();
            for (int i = crateInfo.Count() - 2; i >= 0; i--)
            {
                var crateRow = crateInfo.ElementAt(i).Chunk(4);

                for (int j = 0; j < crateRow.Count(); j++)
                {
                    var currentCrate = crateRow.ElementAt(j)[1];
                    if (currentCrate != ' ')
                        stacks[j].Push(currentCrate);
                }
            }
            return stacks;
        }

        private string readResult(IEnumerable<Stack<char>> stacks)
        {
            var result = new StringBuilder();
            foreach (var stack in stacks)
            {
                if (stack.TryPeek(out char crateChar))
                    result.Append(crateChar);
            }
            return result.ToString();
        }
    }
}