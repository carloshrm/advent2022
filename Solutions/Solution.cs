using System.Diagnostics;

using Elves;

namespace Solutions
{
    abstract class Solution<T> : ISolution
    {
        private readonly string reportTemplate = "\n >> Solutions for day: {0} << \n part one :: {1} \n part two :: {2}";
        private readonly string benchTemplate = "\n :: Runtime << \n part one :: {0} \n part two :: {1}";
        protected int day { get; init; }
        protected (string[] data, string[] example) input { get; set; }

        protected Solution(int day)
        {
            this.day = day;
            var helper = FileElf.callElf();
            input = (helper.getInput(day), helper.getExample(day));
        }

        public virtual void runSolution(bool benchmark = false)
        {
            var watch = benchmark ? new Stopwatch() : null;
            watch?.Start();
            var resultOne = partOne();
            watch?.Stop();

            var runtimeOne = watch?.Elapsed;

            watch?.Restart();
            var resultTwo = partTwo();
            watch?.Stop();

            var runtimeTwo = watch?.Elapsed;
            Console.WriteLine(string.Format(reportTemplate, day, resultOne, resultTwo));
            if (benchmark)
                Console.WriteLine(string.Format(benchTemplate, runtimeOne, runtimeTwo));
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