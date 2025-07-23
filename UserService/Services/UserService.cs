using Api.DTO;
using Api.Services.Interface;
using Api.Utils;
using UserService.Eventing;

namespace UserService.Services;

public class UserService : IUserService
{
    private readonly KafkaProducer _kafka;

    public UserService(KafkaProducer kafka)
    {
        _kafka = kafka;
    }

    public async Task<Result<string>> RegisterUser(UserRegisterRequestDTO dto)
    {
        // 模擬使用者註冊成功（應改為實際儲存到 DB）
        var userId = Guid.NewGuid();

        // 傳送 Kafka 註冊事件
        var registerEvent = new UserRegisterEventDTO
        {
            UserId = userId,
            Username = dto.Username,
            RegisteredAt = DateTime.UtcNow
        };

        await _kafka.SendUserRegisteredAsync(registerEvent);

        return Result<string>.Success("processing");
    }
}