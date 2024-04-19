using Polly;
using Polly.Retry;
using Service.Model;

namespace RetryResilience;

public static class SmsPipeLine
{
    public const string KeyPipeLine = "smsPipeLine";

    public static RetryStrategyOptions<SmsResponse> StrategySms(int maxRetry)
    {
        return new RetryStrategyOptions<SmsResponse>
        {
            MaxRetryAttempts = maxRetry,
            ShouldHandle = new PredicateBuilder<SmsResponse>()
                .Handle<Exception>(),
               // .HandleResult(response => response.StatusCode == System.Net.HttpStatusCode.InternalServerError),

            Delay = TimeSpan.FromSeconds(1),
            MaxDelay = TimeSpan.FromSeconds(15),
            BackoffType = DelayBackoffType.Exponential,
            UseJitter = true,
            OnRetry = static retryData =>
            {
                var now = DateTime.Now.TimeOfDay;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Retry {retryData.AttemptNumber + 1}" +
                                  $" Time= m{now.Minutes}:s{now.Seconds}:ms{now.Milliseconds}");
                return default;
            },
        };
    }
}