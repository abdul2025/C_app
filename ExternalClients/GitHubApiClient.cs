using System.Net;
using System.Text.Json;
using consoleApp.Interfaces;
using consoleApp.Models;
using Microsoft.Extensions.Options;


namespace consoleApp.ExternalClients
{
    public class GitHubApiClient
    {

        private readonly HttpClient _httpClient;
        private readonly GitHubOptions _options;
        private static readonly JsonSerializerOptions _jsonOptions =
        new(JsonSerializerDefaults.Web);
        private readonly string? _token;
        private readonly IAppLogger _logger;



        public GitHubApiClient(HttpClient httpClient, IOptions<GitHubOptions> options, IAppLogger logger)
        {
            _httpClient = httpClient;
            _options = options.Value;
            _logger = logger;


            _httpClient.BaseAddress = new Uri(_options.ApiBaseUrl);

            _token = Environment.GetEnvironmentVariable("GITHUB_TOKEN") ?? "";
            
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("MyConsoleApp/1.0");

            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue(
                    "Bearer", _token);
        }


        public async Task<T?> GetReq<T>(string methodUrl, CancellationToken appLifetimeToken, int maxRetries = 3)
        {
            int attempt = 0;

            using var cts = CancellationTokenSource.CreateLinkedTokenSource(
                appLifetimeToken,  // My consoleApp token as if Ctrl+C occurred which has not time but notice the Ctrl+C, and CancellationToken doing the disposed action.                 
                new CancellationTokenSource(TimeSpan.FromSeconds(30)).Token  // local timeout to handle timeout
            );

            while (true) // keep running always
            {
                try
                {
                    var response = await _httpClient.GetAsync($"users/{methodUrl}", cts.Token); // Pass combined Token
                    response.EnsureSuccessStatusCode();

                    var stream = await response.Content.ReadAsStreamAsync(cts.Token);
                    return await JsonSerializer.DeserializeAsync<T>(stream, _jsonOptions, cts.Token);  // Pass combined Token
                }
                catch (HttpRequestException ex) when (IsTransient(ex) && attempt < maxRetries) //  check the condition to bubble up the error to the next try\catch 
                // 400 or 404 NOT procced for the retrial as API will always return the same HTTP STATUS.
                {
                    attempt++;
                    // exponential backoff if exceed, let it breath ... before hitting it again
                    TimeSpan delay = TimeSpan.FromSeconds(Math.Pow(2, attempt));  // 2s → 4s → 8s
                    
                    _logger.LogWarning("Attempt {Attempt}/{Max} failed. Retrying in {Delay}s…", new {attempt, maxRetries, delay.TotalSeconds});
                    
                    await Task.Delay(delay, cts.Token);  // Pass combined Token
                }
                catch (HttpRequestException ex) when (IsTransient(ex) && attempt >= maxRetries) // This is custom error response to ensure the retries field or exceed the given attempts 
                {
                    throw new HttpRequestException(
                        $"Request failed after {maxRetries} retries. Last error: {ex.Message}",
                        inner: ex,          // ← original exception preserved inside
                        statusCode: ex.StatusCode
                    );
                }
            }


        }

        // Determines whether the error is worth retrying
        private static bool IsTransient(HttpRequestException ex)
        {
            return ex.StatusCode is
                HttpStatusCode.RequestTimeout or       // 408
                // HttpStatusCode.NotFound or       // 404
                HttpStatusCode.TooManyRequests or      // 429
                HttpStatusCode.InternalServerError or  // 500
                HttpStatusCode.BadGateway or           // 502
                HttpStatusCode.ServiceUnavailable or   // 503
                HttpStatusCode.GatewayTimeout          // 504
                || ex.StatusCode is null;              // network-level failures (no response)
        }
    }
}


