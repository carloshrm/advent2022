using System.Diagnostics;

namespace Namespace
{
    abstract class Solution<T>
    {
        private readonly string reportTemplate = "\n >> Solutions for day: {0} << \n part one :: {1} \n part two :: {2}";
        private readonly string bmTemplate = "\n :: Runtime << \n part one :: {0} \n part two :: {1}";
        protected int day;
        protected (string[] data, string[] example) input;

        protected Solution(int day)
        {
            this.day = day;
            var helper = FileElf.callElf();
            input = (helper.getInput(day), helper.getExample(day));
        }

        public virtual void runSolution(bool benchmark = false)
        {
            var sw = benchmark ? new Stopwatch() : null;

            sw?.Start();
            var resultOne = partOne();
            sw?.Stop();

            var runtimeOne = sw?.Elapsed;

            sw?.Restart();
            var resultTwo = partTwo();
            sw?.Stop();

            var runtimeTwo = sw?.Elapsed;
            Console.WriteLine(string.Format(reportTemplate, day, resultOne, resultTwo));
            if (benchmark)
                Console.WriteLine(string.Format(bmTemplate, runtimeOne, runtimeTwo));
        }
        protected virtual T partOne()
        {
            foreach (var line in input.example)
            {
                Console.WriteLine(line);
            }
            return default;
        }

        protected virtual T partTwo()
        {
            foreach (var line in input.data)
            {
                Console.WriteLine(line);
            }
            return default;
        }
    }

}