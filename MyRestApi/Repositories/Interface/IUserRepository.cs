using MyRestApi.Models;
namespace MyRestApi.Repositories;

public interface IUserRepository
{
    Task<bool> IsUserExist(Guid id);
    Task<bool> IsUserExist(User user);
    Task<Guid> CreateUser(User user);
    Task<User?> GetUserById(Guid id);
    Task<User?> GetUserByEmail(string email);
    Task PatchUser(User user);
    Task DeleteUser(Guid id);
} 