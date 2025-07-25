using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading.Tasks;
using Api.DTO;
namespace UserService.Eventing;

public class KafkaProducer: IKafkaProducer
{
    private readonly string _bootstrapServers;
    private readonly string _userRegisterTopic;
    private readonly string _userDeleteTopic;

    public KafkaProducer(IConfiguration configuration)
    {
        _bootstrapServers = configuration["Kafka:BootstrapServers"];
        _userRegisterTopic = configuration["Kafka:UserRegisterTopic"];
        _userDeleteTopic = configuration["Kafka:UserDeleteTopic"];
    }

    public async Task SendUserRegisteredAsync(UserRegisterEventDTO registerEvent)
    {
        var config = new ProducerConfig
        {
            // BootstrapServers = _bootstrapServers, 
            BootstrapServers = "localhost:9092", 
            BrokerAddressFamily = BrokerAddressFamily.V4,
            ClientDnsLookup = ClientDnsLookup.UseAllDnsIps
        };
        using var producer = new ProducerBuilder<Null, string>(config).Build();

        var message = new Message<Null, string> { Value = JsonSerializer.Serialize(registerEvent) };
        await producer.ProduceAsync(_userRegisterTopic, message);
    }

    public async Task SendUserDeletedAsync(UserDeleteEventDTO deleteEvent)
    {
        var config = new ProducerConfig
        {
            BootstrapServers = "localhost:9092",
            BrokerAddressFamily = BrokerAddressFamily.V4,
            ClientDnsLookup = ClientDnsLookup.UseAllDnsIps
        };
        using var producer = new ProducerBuilder<Null, string>(config).Build();
        
        var message = new Message<Null, string> { Value = JsonSerializer.Serialize(deleteEvent) };
        await producer.ProduceAsync(_userDeleteTopic, message);
    }
}