namespace Namespace
{
    abstract class Solution
    {
        protected bool isExample = true;
        protected int day;
        protected (string[] data, string[] example) input;

        protected Solution(int day)
        {
            this.day = day;
            var helper = Elf.callElf();
            input = helper.getInput(day);
        }

        public virtual void runSolution()
        {
            Console.WriteLine($"\n >> Solutions for day: {day} << \n part one :: {partOne()} \n part two :: {partTwo()}");
        }
        protected abstract int partOne();
        protected abstract int partTwo();
    }

}