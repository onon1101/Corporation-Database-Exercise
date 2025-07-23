
using System.Text.Json;
using Api.DTO;
using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;
using UserService.Worker.Models;
using UserService.Worker.Repositories;

namespace UserService.Worker.Services;

public class KafkaConsumerService: BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public KafkaConsumerService(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var config = new ConsumerConfig 
        {
            BootstrapServers = "localhost:9092",
            GroupId = "user-service-worker-group",
            AutoOffsetReset = AutoOffsetReset.Earliest,
        };

        using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
        consumer.Subscribe("user.registered");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var result = consumer.Consume(stoppingToken);
                var dto = JsonSerializer.Deserialize<UserRegisterEventDTO>(result.Message.Value);

                if (dto != null)
                {
                    using var scope = _serviceScopeFactory.CreateScope();
                    var repo = scope.ServiceProvider.GetRequiredService<UserRepository>();
                    await repo.RegisterUser(dto);
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