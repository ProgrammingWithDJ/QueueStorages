using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace StorageFuncall
{
    public class Function1
    {
        [FunctionName("StorageFunCall")]
        public void Run([QueueTrigger("weatherdata", Connection = "WeatherDataConnection")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
        }
    }
}
