
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Threading;
using System.Net.Http;
using System;

namespace HttpToQueueWebhook
{
    public static class HttpTrigger
    {
        [FunctionName("HttpTrigger")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "post")]HttpRequest req, 
            ILogger log,
            [Queue("process")]out ProcessRequest process)
        {
            log.LogInformation("Webhook request from Logic Apps received.");

            string requestBody = new StreamReader(req.Body).ReadToEnd();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            string callbackUrl = data?.callbackUrl;

            //This will drop a message in a queue that QueueTrigger will pick up
            process = new ProcessRequest { callbackUrl = callbackUrl, data = "some data" };
            return new AcceptedResult();
        }
        
    }

    public class ProcessRequest
    {
        public string callbackUrl { get; set; }
        public string data { get; set; }
    }

    public class ProcessResponse
    {
        public string data { get; set; }
    }

}
