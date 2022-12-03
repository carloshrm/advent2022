using Microsoft.Extensions.Configuration;

namespace Namespace
{
    internal class Program
    {
        private static List<Solution> solutions = new List<Solution>();
        private static void Main(string[] args)
        {
            readSecrets();
            solutions.Add(new Day3());
            solutions.ForEach(x => x.runSolution());
        }

        private static void readSecrets()
        {
            var config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();
            foreach (var child in config.GetChildren())
            {
                Environment.SetEnvironmentVariable(child.Key, child.Value);
            }
        }
    }
}