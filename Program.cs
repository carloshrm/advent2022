using Microsoft.Extensions.Configuration;

namespace Namespace
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            readSecrets();
            new Day7().runSolution(true);
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