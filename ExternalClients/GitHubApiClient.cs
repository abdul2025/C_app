using System.Text.Json;

namespace consoleApp.ExternalClients
{
    public class GitHubApiClient
    {

        private readonly HttpClient _httpClient;
        private readonly string based_url = "https://api.github.com/users";
        private string token = Environment.GetEnvironmentVariable("GITHUB_TOKEN");

        public GitHubApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;

            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("MyConsoleApp/1.0");

            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue(
                    "Bearer", token);
        }

        private static readonly JsonSerializerOptions _options =
        new(JsonSerializerDefaults.Web);

        public async Task<T> GetReq<T>(string method_url)
        {

            var url = $"{based_url}/{method_url}";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var stream = await response.Content.ReadAsStreamAsync();


            return await JsonSerializer.DeserializeAsync<T>(stream, _options);
        }
    }
}