namespace estate_emporium.Utils;
using Polly;
using Polly.Extensions.Http;
using Polly.Retry;
using System;
using System.Net.Http;
using System.Threading.Tasks;
public class PollyUtils
{
    public static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        return HttpPolicyExtensions
       .HandleTransientHttpError()
       .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
       .WaitAndRetryAsync(5, retryAttempt =>
       {
           var delay = TimeSpan.FromSeconds(Math.Pow(2, retryAttempt));
           Console.WriteLine($"Retrying in {delay.TotalSeconds} seconds...");
           return delay;
       },
           onRetry: (outcome, timespan, retryAttempt, context) =>
           {
               Console.WriteLine($"Retry {retryAttempt} encountered an error: {outcome.Exception?.Message ?? outcome.Result.ReasonPhrase}. Waiting {timespan.TotalSeconds} seconds before next retry.");
           });
    }
}

