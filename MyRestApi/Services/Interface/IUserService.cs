using MyRestApi.DTO;
using MyRestApi.Models;

namespace MyRestApi.Services;

public interface IUserService
{
    Task<Result<Guid>> RegisterUserAsync(UserRegisterDTO user);
    Task<Result<User>> AuthenticateUserAsync(string email, string password);
    Task<Result<User>> AuthenticateUsernameAsync(string name, string password);
    Task<Result<User>> GetUserById(Guid id);
    Task DeleteUser(Guid id);
}