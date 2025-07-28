namespace UserService.Eventing;

public class KafkaTopicResolver : IKafkaTopicResolver
{
    private readonly Dictionary<KafkaTopicType, string> _topicMap;

    public KafkaTopicResolver(IConfiguration configuration)
    {
        _topicMap = new  Dictionary<KafkaTopicType, string>
        {
            { KafkaTopicType.UserRegistered, configuration["Kafka:UserRegisteredTopic"] },
            { KafkaTopicType.UserDeleted , configuration["Kafka:UserDeletedTopic"] },
            { KafkaTopicType.Log , configuration["Kafka:LogTopic"] }
        };
    }

    public string Resolve(KafkaTopicType topicType)
    {
        if (!_topicMap.TryGetValue(topicType, out var topic))
        {
            throw new ArgumentException($"Kafka topic not conifgured for {topicType}");
        }

        return topic;
    }
    
    
}