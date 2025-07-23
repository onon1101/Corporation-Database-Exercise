using Api.DTO;
using Api.Utils;

namespace Api.Services.Interface;

public interface IUserService 
{
    public Task<Result<string>> RegisterUser(UserRegisterRequestDTO dto);
}