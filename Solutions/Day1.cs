namespace Solutions
{
    internal class Day1 : Solution<int, int>
    {
        private readonly IEnumerable<int> _packValues;
        public Day1() : base(1) => _packValues = setCaloriePackValues();

        protected override int partOne() => _packValues.LastOrDefault();

        protected override int partTwo() => _packValues.TakeLast(3).Sum();

        private List<int> setCaloriePackValues()
        {
            var elfPacks = new List<int>();
            int calorieSum = 0;
            foreach (var line in input.data)
            {
                if (string.IsNullOrEmpty(line))
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
                                i--;
                            elfPacks.Insert(i + 1, calorieSum);
                        }
                    }
                    calorieSum = 0;
                }
                else
                    calorieSum += int.Parse(line);
            }
            return elfPacks;
        }
    }
}