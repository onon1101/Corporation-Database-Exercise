using MyRestApi.Models;

namespace MyRestApi.Services;

public interface IUserService
{
    Task<bool> IsUserExist(Guid id);
    Task<bool> IsUserExist(User user);
    Task<Guid> RegisterUserAsync(User user);
    Task<User> GetUserById(int id);
    Task<User> GetUserByEmail(string email);
    Task DeleteUser(Guid id);
    Task PatchUser(User user);
}