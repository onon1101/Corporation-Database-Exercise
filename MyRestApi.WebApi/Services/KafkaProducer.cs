using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyRestApi.WebApi.Services
{
    public class KafkaProducer
    {
        private readonly string _bootstrapServers;
        private readonly string _movieTopic;

        public KafkaProducer(IConfiguration configuration)
        {
            _bootstrapServers = configuration["Kafka:BootstrapServers"];
            _movieTopic = configuration["Kafka:MovieTopic"];
        }

        public async Task SendMovieAsync<T>(T data)
        {
            // var config = new ProducerConfig { BootstrapServers = _bootstrapServers };
            var config = new ProducerConfig
            {
                BootstrapServers = "kafka:9092",
                BrokerAddressFamily = BrokerAddressFamily.V4,
                ClientDnsLookup = ClientDnsLookup.UseAllDnsIps
            };
            using var producer = new ProducerBuilder<Null, string>(config).Build();

            var message = new Message<Null, string> { Value = JsonSerializer.Serialize(data) };
            await producer.ProduceAsync(_movieTopic, message);
        }
    }
}