using System;
using System.Threading;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace QueueFunction
{
    public static class QueueTrigger
    {
        [FunctionName("QueueTrigger")]
        public static void Run(
            [QueueTrigger("request")]string logicAppRequest, 
            ILogger log, 
            [Queue("response")] out string logicAppResponse)
        {
            log.LogInformation($"got the request from Logic Apps: {logicAppRequest}");
            Thread.Sleep(TimeSpan.FromMinutes(3));
            logicAppResponse = "Work Finished";
        }
    }
}
