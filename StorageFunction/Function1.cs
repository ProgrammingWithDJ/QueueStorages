using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace StorageFunction
{
    public class Function1
    {
        [FunctionName("ProcessWeatherData")]
        public void Run([ServiceBusTrigger("weatherdata", Connection = "weatherDataQueue")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
        }
    }
}
