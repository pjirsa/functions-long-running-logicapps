using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using System;
using Flurl.Http;

namespace HttpToQueueWebhook
{
    public static class QueueTrigger
    {
        /// <summary>
        /// Queue trigger function to pick up item and do long work. Will then invoke
        /// the callback URL to have logic app continue
        /// </summary>
        [FunctionName("QueueTrigger")]
        public static async Task Run([QueueTrigger("process")]ProcessRequest item, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {item.data}");
            Thread.Sleep(TimeSpan.FromMinutes(3));
            ProcessResponse result = new ProcessResponse { data = "some result data" };
            await item.callbackUrl.PostJsonAsync(result);
        }
    }
}