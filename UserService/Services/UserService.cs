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

    public async Task<Result<UserRegisterResponseDTO>> RegisterUser(UserRegisterRequestDTO dto)
    {
        // 傳送 Kafka 註冊事件
        var registerEvent = new UserRegisterEventDTO
        {
            Username = dto.Username,
            Password = dto.Password,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
            RegisteredAt = DateTime.UtcNow
        };

        await _kafka.SendUserRegisteredAsync(registerEvent);

        return Result<UserRegisterResponseDTO>.Success(new UserRegisterResponseDTO
        {
            Message = "user register request is processing."
        });
    }

    public async Task<Result<UserDeleteResponseDTO>> DeleteUser(UserDeleteRequestDTO dto)
    {
        var deleteEvent = new UserDeleteEventDTO
        {
            Username = dto.Username,
            Password = dto.Password,
            DeletedOn = DateTime.UtcNow
        };
        
        await _kafka.SendUserDeletedAsync(deleteEvent);

        return Result<UserDeleteResponseDTO>.Success(new UserDeleteResponseDTO
        {
            Message = "user delete request is processing."
        });
        throw new NotImplementedException();
    }
}