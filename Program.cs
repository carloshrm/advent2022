using System.Reflection;

using Microsoft.Extensions.Configuration;

using Solutions;

namespace Advent2022
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            readEnvSecrets();
            new Day11().runSolution(true);
            //foreach (var s in instantiateSolutions())
            //    s?.runSolution(true);
        }

        private static void readEnvSecrets()
        {
            foreach (var child in new ConfigurationBuilder().AddUserSecrets<Program>().Build().GetChildren())
                Environment.SetEnvironmentVariable(child.Key, child.Value);
        }

        private static IEnumerable<ISolution?> instantiateSolutions()
        {
            return Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.Name.Contains("Day"))
                .Select(t => (ISolution)Activator.CreateInstance(t));
        }
    }
}