using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading.Tasks;
using Api.DTO;
namespace UserService.Eventing;

public class KafkaProducer: IDisposable
{
    private readonly string _bootstrapServers;
    private readonly string _userRegisterTopic;
    private readonly string _userDeleteTopic;
    private readonly IProducer<Null, string> _producer;
    private readonly IKafkaTopicResolver _topicResolver;
    private readonly string _logTopic;

    public KafkaProducer(IConfiguration configuration, IKafkaTopicResolver topicResolver)
    {
        // _bootstrapServers = configuration["Kafka:BootstrapServers"];
        // _userRegisterTopic = configuration["Kafka:UserRegisterTopic"];
        // _userDeleteTopic = configuration["Kafka:UserDeleteTopic"];
        // _logTopic = configuration["Kafka:LogTopic"];

        _topicResolver = topicResolver;
        var config = new ProducerConfig
        {
            BootstrapServers = configuration["Kafka:BootstrapServers"],
            BrokerAddressFamily = BrokerAddressFamily.V4,
            ClientDnsLookup = ClientDnsLookup.UseAllDnsIps
        };

        _producer = new ProducerBuilder<Null, string>(config).Build();
    }

    public async Task SendAsync<T>(KafkaTopicType topicType, T data)
    {
        var topic = _topicResolver.Resolve(topicType);
        var message = new Message<Null, string>
        {
            Value = JsonSerializer.Serialize(data)
        };
        await _producer.ProduceAsync(topic, message);
    }

    public void Dispose()
    {
        _producer?.Flush();
        _producer?.Dispose();
    }

    // public async Task SendUserRegisteredAsync(UserRegisterEventDTO registerEvent)
    // {
    //     var config = new ProducerConfig
    //     {
    //         // BootstrapServers = _bootstrapServers, 
    //         BootstrapServers = "localhost:9092", 
    //         BrokerAddressFamily = BrokerAddressFamily.V4,
    //         ClientDnsLookup = ClientDnsLookup.UseAllDnsIps
    //     };
    //     using var producer = new ProducerBuilder<Null, string>(config).Build();
    //
    //     var message = new Message<Null, string> { Value = JsonSerializer.Serialize(registerEvent) };
    //     await producer.ProduceAsync(_userRegisterTopic, message);
    // }
    //
    // public async Task SendUserDeletedAsync(UserDeleteEventDTO deleteEvent)
    // {
    //     var config = new ProducerConfig
    //     {
    //         BootstrapServers = "localhost:9092",
    //         BrokerAddressFamily = BrokerAddressFamily.V4,
    //         ClientDnsLookup = ClientDnsLookup.UseAllDnsIps
    //     };
    //     using var producer = new ProducerBuilder<Null, string>(config).Build();
    //     
    //     var message = new Message<Null, string> { Value = JsonSerializer.Serialize(deleteEvent) };
    //     await producer.ProduceAsync(_userDeleteTopic, message);
    // }
    //
    // public async Task SendLogAsync(LogDTO logEvent)
    // {
    //     var config = new ProducerConfig
    //     {
    //         BootstrapServers = "localhost:9092",
    //         BrokerAddressFamily = BrokerAddressFamily.V4,
    //         ClientDnsLookup = ClientDnsLookup.UseAllDnsIps
    //     };
    //     
    //     using var producer = new ProducerBuilder<Null, string>(config).Build();
    //
    //     var message = new Message<Null, string> { Value = JsonSerializer.Serialize(logEvent) };
    //     await producer.ProduceAsync(_logTopic, message);
    // }
}