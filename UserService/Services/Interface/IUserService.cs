using Api.DTO;
using Api.Utils;

namespace Api.Services.Interface;

public interface IUserService 
{
    public Task<Result<UserRegisterResponseDTO>> RegisterUser(UserRegisterRequestDTO dto);
    public Task<Result<UserDeleteResponseDTO>> DeleteUser(UserDeleteRequestDTO dto);
}