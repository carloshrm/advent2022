namespace Namespace
{
    internal class Program
    {
        private static List<Solution> solutions = new List<Solution>();
        private static void Main(string[] args)
        {
            //solutions.Add(new Day1());
            solutions.Add(new Day2());
            solutions.ForEach(x => x.runSolution());
        }
    }
}