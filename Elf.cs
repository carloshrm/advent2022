using System.Net;
using System.Net.Http.Headers;

namespace Namespace
{
    internal class Elf
    {
        private readonly string path = "data/day{0}{1}.txt";
        private readonly string siteURL = "https://adventofcode.com/";

        private static Elf? _elf;
        private static HttpClient? _httpClient;

        private Elf()
        {
            var httpClientHandler = new HttpClientHandler();
            _httpClient = new HttpClient(httpClientHandler);
            _httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("(+https://github.com/carloshrm/advent2022)"));
            _httpClient.BaseAddress = new Uri(siteURL);

            var cookieContainer = new CookieContainer();
            cookieContainer.Add(_httpClient.BaseAddress, new Cookie("session", Environment.GetEnvironmentVariable("session")));
            httpClientHandler.CookieContainer = cookieContainer;
        }

        public static Elf callElf()
        {
            if (_elf == null)
                _elf = new Elf();

            return _elf;
        }

        public string[] getInput(int day)
        {
            var mainData = getDataFromFile(day, isExample: false);
            if (mainData.Any() is false)
            {
                var request = getDataFromHttp(day);
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
                var request = getExampleFromHttp(day);
                exampleData = request.Result.Split("\n");
                File.WriteAllLines(string.Format(path, day, "e"), exampleData);
            }
            return exampleData;
        }

        private async Task<string> getDataFromHttp(int day)
        {
            var resp = _httpClient!.SendAsync(new HttpRequestMessage(HttpMethod.Get, $"/2022/day/{day}/input"));
            Console.WriteLine("http call");
            return await resp.Result.Content.ReadAsStringAsync();
        }

        private async Task<string> getExampleFromHttp(int day)
        {
            var resp = _httpClient!.SendAsync(new HttpRequestMessage(HttpMethod.Get, $"/2022/day/{day}"));
            var rawHtml = await resp.Result.Content.ReadAsStringAsync();
            Console.WriteLine("http call");
            var startingIndex = rawHtml.IndexOf("<pre><code>") + "<pre><code>".Length;
            var endingIndex = rawHtml.IndexOf("</code>", startingIndex);

            return rawHtml.Substring(startingIndex, endingIndex - startingIndex);
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