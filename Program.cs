namespace Namespace
{
    internal class Program
    {
        private const string path = "./day{0}{1}.txt";

        private static void Main(string[] args)
        {
            var inputFile = File.ReadAllLines(string.Format(path, 1, ""));
            var elfPacks = new List<int>();

            int calorieSum = 0;
            foreach (var line in inputFile)
            {
                if (line.Equals(""))
                {
                    elfPacks.Add(calorieSum);
                    calorieSum = 0;
                }
                else
                {
                    int.TryParse(line, out int calorieValue);
                    calorieSum += calorieValue;
                }
            }
            Console.WriteLine(elfPacks.Max());
        }
    }


}