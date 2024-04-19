using Polly;
using Polly.Retry;

namespace RetryResilience;

public static class SmsPipeLine
{
    public const string KeyPipeLine = "SsmsPipeLine";

    public static void StrategySms(int maxRetry)
    {
        var retryStrategyOptions = new RetryStrategyOptions<HttpResponseMessage>
        {
            MaxRetryAttempts = maxRetry,
            ShouldHandle = new PredicateBuilder<HttpResponseMessage>()
                .Handle<HttpRequestException>()
                .HandleResult(response => response.StatusCode == System.Net.HttpStatusCode.InternalServerError),

            Delay = TimeSpan.FromSeconds(1),
            MaxDelay = TimeSpan.FromSeconds(15),
            BackoffType = DelayBackoffType.Exponential,
            UseJitter = true,
        };

    }
}