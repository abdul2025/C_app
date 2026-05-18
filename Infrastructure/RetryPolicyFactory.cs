using consoleApp.Interfaces;
using Polly;
using Polly.Retry;
using System.Net;


namespace consoleApp.Infrastructure
{
    public static class RetryPolicyFactory
        {
            // This is as configuring the Polly engin which ill injected in services ...
            public static AsyncRetryPolicy<HttpResponseMessage> CreateHttpRetryPolicy(
                IAppLogger logger,
                int maxRetries)
            {
                return Policy<HttpResponseMessage>
                    .Handle<HttpRequestException>(ex => IsTransient(ex))
                    .OrResult(response =>
                        (int)response.StatusCode >= 300 ||
                        response.StatusCode == HttpStatusCode.TooManyRequests)
                    .WaitAndRetryAsync(
                        maxRetries, // Register retrial attempts
                        attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)), // Every 2s
                        (outcome, delay, attempt, context) =>
                        {
                            var requestId = context["RequestId"]; // value passed as per each request in context

                            logger.LogWarning(
                                $"Retry {attempt} for {requestId} after {delay.TotalSeconds}s",
                                new { attempt, maxRetries, requestId, delay.TotalSeconds });
                        });
            }

            private static bool IsTransient(HttpRequestException ex)
            {
                return ex.StatusCode is
                    HttpStatusCode.RequestTimeout or
                    // HttpStatusCode.NotFound or
                    HttpStatusCode.TooManyRequests or
                    HttpStatusCode.InternalServerError or
                    HttpStatusCode.BadGateway or
                    HttpStatusCode.ServiceUnavailable or
                    HttpStatusCode.GatewayTimeout
                    || ex.StatusCode is null;
            }
        }
}