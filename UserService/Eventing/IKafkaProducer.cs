using Api.DTO;

namespace UserService.Eventing;

public interface IKafkaProducer
{
    Task SendUserRegisteredAsync(UserRegisterEventDTO registerEvent);
    Task SendUserDeletedAsync(UserDeleteEventDTO deleteEvent);
}