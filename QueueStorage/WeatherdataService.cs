using Azure.Storage.Queues;
using QueueStorage;
using System;
using System.Text.Json;

public class WeatherdataService :BackgroundService
{
    private readonly ILogger<WeatherdataService> _logger;
    private readonly QueueClient _queueClient;

    public WeatherdataService(ILogger<WeatherdataService> logger,QueueClient queueClient)
	{
        _logger = logger;
        _queueClient = queueClient;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
      

        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Reading from the queue");
            var queueMessages = await _queueClient.ReceiveMessageAsync();

            if(queueMessages != null)
            {
                var weatherData = JsonSerializer.Deserialize<WeatherForecast>(queueMessages.Value.Body);
                _logger.LogInformation("Reading weather data" + weatherData);

             //   await _queueClient.DeleteMessageAsync(queueMessages.Value.MessageId, queueMessages.Value.PopReceipt);

            }

            await Task.Delay(TimeSpan.FromSeconds(10));
        }
    }
}
