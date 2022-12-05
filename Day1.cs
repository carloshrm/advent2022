namespace Namespace
{
    internal class Day1 : Solution<int>
    {
        private IEnumerable<int> _packValues;
        public Day1() : base(1)
        {
            setCaloriePackValues();
        }

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

}