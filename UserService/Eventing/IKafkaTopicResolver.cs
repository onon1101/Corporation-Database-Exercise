using UserService.Eventing;

namespace UserService.Eventing;

public interface IKafkaTopicResolver
{
    string Resolve(KafkaTopicType topicType);
}
