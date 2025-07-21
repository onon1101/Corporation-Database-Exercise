namespace MyRestApi.Repositories;

public interface IUserRepository
{
    Task<bool> IsUserExist(Guid id);
    Task<bool> IsUserExist(UserRegisterDTO user);
    Task<Result<Guid>> CreateUser(UserRegisterDTO user);
    Task<User?> GetUserById(Guid id);
    Task<User?> GetUserByEmail(string email);
    Task<User?> GetUserByName(string name);
    Task PatchUser(User user);
    Task DeleteUser(Guid id);
    Task<UserRole> GetUserPermission(Guid userId);
} 