namespace Namespace
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            new Day1().runSolution();
        }
    }

    internal class Day1 : Solution
    {
        public Day1() : base(1)
        {
            setCaloriePackValues();
        }

        private IEnumerable<int> _packValues;
        private void setCaloriePackValues()
        {
            var elfPacks = new List<int>();
            int calorieSum = 0;
            foreach (var line in input.data)
            {
                if (line.Equals(""))
                {
                    if (calorieSum > elfPacks.LastOrDefault())
                        elfPacks.Add(calorieSum);
                    else
                    {
                        int i = elfPacks.Count - 1;
                        if (i < 0)
                            elfPacks.Insert(0, calorieSum);
                        else
                        {
                            while (i >= 0 && elfPacks[i] > calorieSum)
                            {
                                i--;
                            }
                            elfPacks.Insert(i + 1, calorieSum);
                        }

                    }

                    calorieSum = 0;
                }
                else
                {
                    int.TryParse(line, out int calorieValue);
                    calorieSum += calorieValue;
                }
            }
            _packValues = elfPacks;
        }


        protected override int partOne()
        {
            return _packValues.LastOrDefault();
        }

        protected override int partTwo()
        {
            return _packValues.TakeLast(3).Sum();
        }
    }

    abstract class Solution
    {
        protected bool isExample = true;
        protected int day;
        protected (string[] data, string[] example) input;

        protected Solution(int day)
        {
            this.day = day;
            var helper = Elf.callElf();
            input = helper.getInput(day);
        }

        public virtual void runSolution()
        {
            Console.WriteLine($"\n >> Solutions for day: {day} << \n part one :: {partOne()} \n part two :: {partTwo()}");
        }
        protected abstract int partOne();
        protected abstract int partTwo();
    }

}