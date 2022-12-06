using System.Text;
using System.Text.RegularExpressions;

namespace Namespace
{
    internal class Day5 : Solution<string>
    {
        public Day5() : base(5) { }

        protected override string partOne()
        {
            var crateStacks = setupCrateStacks();

            for (int i = crateStacks.Length + 1; i < input.data.Length; i++)
            {
                var match = Regex.Matches(input.data[i], @"\d+").Select(m => int.Parse(m.Value));

                int count = match.First();
                while (count-- > 0)
                    crateStacks[match.Last() - 1].Push(crateStacks[match.ElementAt(1) - 1].Pop());
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
                    temp.Push(crateStacks[match.ElementAt(1) - 1].Pop());

                while (temp.Any())
                    crateStacks[match.Last() - 1].Push(temp.Pop());
            }

            return readResult(crateStacks);
        }

        private Stack<char>[] setupCrateStacks()
        {
            var crates = input.data.TakeWhile(line => !string.IsNullOrEmpty(line));
            var stacks = new Stack<char>[int.Parse(crates.Last().Max().ToString())];

            for (int i = crates.Count() - 2; i >= 0; i--)
            {
                var rowCrates = crates.ElementAt(i).Chunk(4);

                for (int j = 0; j < rowCrates.Count(); j++)
                {
                    var currentCrate = rowCrates.ElementAt(j)[1];
                    if (currentCrate != ' ')
                    {
                        if (stacks[j] == null)
                            stacks[j] = new Stack<char>();

                        stacks[j].Push(currentCrate);
                    }
                }
            }
            return stacks;
        }

        private string readResult(Stack<char>[] stacks)
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