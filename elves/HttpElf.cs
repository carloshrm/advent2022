using System.Net;
using System.Net.Http.Headers;

namespace Namespace
{
    internal class HttpElf
    {
        private static HttpClient? _httpClient;
        private readonly string siteURL = "https://adventofcode.com/";
        private static HttpElf? _elf;

        private HttpElf()
        {
            var httpClientHandler = new HttpClientHandler();
            _httpClient = new HttpClient(httpClientHandler);
            _httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("(+https://github.com/carloshrm/advent2022)"));
            _httpClient.BaseAddress = new Uri(siteURL);

            var cookieContainer = new CookieContainer();
            cookieContainer.Add(_httpClient.BaseAddress, new Cookie("session", Environment.GetEnvironmentVariable("session")));
            httpClientHandler.CookieContainer = cookieContainer;
        }

        public static HttpElf callElf()
        {
            if (_elf == null)
                _elf = new HttpElf();

            return _elf;
        }

        public async Task<string> getDataFromHttp(int day)
        {
            var resp = _httpClient!.SendAsync(new HttpRequestMessage(HttpMethod.Get, $"/2022/day/{day}/input"));
            Console.WriteLine("http call");
            return await resp.Result.Content.ReadAsStringAsync();
        }

        public async Task<string> getExampleFromHttp(int day)
        {
            var resp = _httpClient!.SendAsync(new HttpRequestMessage(HttpMethod.Get, $"/2022/day/{day}"));
            var rawHtml = await resp.Result.Content.ReadAsStringAsync();
            Console.WriteLine("http call");
            return findExample(rawHtml);
        }

        private string findExample(string rawHtml)
        {
            var result = string.Empty;
            int start = 0;
            do
            {
                var startingIndex = rawHtml.IndexOf("<pre><code>", start) + "<pre><code>".Length;
                var endingIndex = rawHtml.IndexOf("</code>", startingIndex);
                result = rawHtml.Substring(startingIndex, endingIndex - startingIndex);
                start = endingIndex;
            } while (result.Contains("<"));

            return result;
        }
    }

}