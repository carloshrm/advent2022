namespace Namespace
{
    internal class FileElf
    {
        private readonly string path = "data/day{0}{1}.txt";
        private static FileElf? _elf;

        private FileElf() { }

        public static FileElf callElf()
        {
            if (_elf == null)
                _elf = new FileElf();

            return _elf;
        }

        public string[] getInput(int day)
        {
            var mainData = getDataFromFile(day, isExample: false);
            if (mainData.Any() is false)
            {
                var request = HttpElf.callElf().getDataFromHttp(day);
                mainData = request.Result.Split("\n");
                File.WriteAllLines(string.Format(path, day, ""), mainData);
            }
            return mainData;
        }

        public string[] getExample(int day)
        {
            var exampleData = getDataFromFile(day, isExample: true); ;
            if (exampleData.Any() is false)
            {
                var request = HttpElf.callElf().getExampleFromHttp(day);
                exampleData = request.Result.Split("\n");
                File.WriteAllLines(string.Format(path, day, "e"), exampleData);
            }
            return exampleData;
        }

        private string[] getDataFromFile(int day, bool isExample)
        {
            try
            {
                return File.ReadAllLines(string.Format(path, day, isExample ? "e" : ""));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new string[0];
            }
        }
    }

}