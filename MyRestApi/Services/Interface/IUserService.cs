using MyRestApi.Models;

namespace MyRestApi.Services;

public interface IUserService
{
    Task<Guid> RegisterUserAsync(User user);
    Task<User?> AuthenticateUserAsync(string email, string password);
    Task<User?> AuthenticateUsernameAsync(string name, string password);
    Task<User?> GetUserById(Guid id);
    Task DeleteUser(Guid id);
}