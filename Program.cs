using Microsoft.Extensions.Configuration;
using Solutions;
using System.Reflection;

namespace Advent2022
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            readSecrets();
            foreach (var s in instantiateSolutions()) s.runSolution();
        }

        private static void readSecrets()
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