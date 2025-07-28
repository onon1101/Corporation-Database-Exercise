using Api.DTO;
using Api.Services.Interface;
using Api.Utils;
using UserService.Eventing;
using UserService.Repositories;

namespace UserService.Services;

public class UserService : IUserService
{
    private readonly KafkaProducer _kafka;
    private readonly UserRepository _userRepository;

    public UserService(KafkaProducer kafka, UserRepository repository)
    {
        _kafka = kafka;
        _userRepository = repository;
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

        var logEvent = new LogDTO
        {
            Timestamp = DateTime.UtcNow,
            Level = "INFO",
            Message = $"user {dto.Username} is registered",
            Source = "user service",
        };

        await Task.WhenAll(
            _kafka.SendAsync(KafkaTopicType.UserRegistered, registerEvent),
            _kafka.SendAsync(KafkaTopicType.Log, logEvent)
        );

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

        var logEvent = new LogDTO
        {
            Level = "INFO",
            Message = $"user {dto.Username} is deleted",
            Source = "user service",
        };

        await Task.WhenAll(
            _kafka.SendAsync(KafkaTopicType.UserDeleted, deleteEvent),
            _kafka.SendAsync(KafkaTopicType.Log, logEvent));
        
        return Result<UserDeleteResponseDTO>.Success(new UserDeleteResponseDTO
        {
            Message = "user delete request is processing."
        });
    }

    public async Task<Result<UserGetByPhoneNumberResponseDTO>> GetByIdUser(UserGetByPhoneNumberRequestDTO dto)
    {
        return Result<UserGetByPhoneNumberResponseDTO>.Success(
            await _userRepository.GetByPhoneNumber(dto));
    }
}