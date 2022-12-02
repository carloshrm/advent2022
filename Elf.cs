namespace Namespace
{
    internal class Elf
    {
        private const string path = "data/day{0}{1}.txt";
        private static Elf? elf;

        private Elf() { }

        public static Elf callElf()
        {
            if (elf == null)
                elf = new Elf();

            return elf;
        }

        public (string[], string[]) getInput(int day)
        {
            try
            {
                return (File.ReadAllLines(string.Format(path, day, "")), File.ReadAllLines(string.Format(path, day, "e")));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return (new string[0], new string[0]);
            }
        }
    }

}