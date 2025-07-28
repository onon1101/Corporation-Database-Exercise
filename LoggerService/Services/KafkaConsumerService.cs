using System.Text.Json;
using Confluent.Kafka;
using LoggerService.Repositories;
using Microsoft.Extensions.Logging.Abstractions;
using LoggerService.Worker.Models;

namespace LoggerService.Services;

public class KafkaConsumerService: BackgroundService
{
    private readonly MongoBusRepository _repository;
    public KafkaConsumerService(MongoBusRepository repository)
    {
        _repository = repository;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = "localhost:9092",
            GroupId = "log-service-worker-group",
            AutoOffsetReset = AutoOffsetReset.Earliest,
        };

        using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
        consumer.Subscribe("log.send");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var result = consumer.Consume(stoppingToken);
                var dto = JsonSerializer.Deserialize<LogEntry>(result.Message.Value);

                if (dto != null)
                {
                    await _repository.Storage(dto);
                }
            }
            catch (ConsumeException e)
            {
                Console.Error.WriteLine(e);
            }
        }
        consumer.Close();
    }
}