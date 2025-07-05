using Microsoft.AspNetCore.Http.HttpResults;
using MyRestApi.Models;
using MyRestApi.Repositories;
using MyRestApi.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repo;

    public UserService(IUserRepository repo)
    {
        _repo = repo;
    }
    public async Task<bool> IsUserExist(Guid id)
    {
        bool exist = await _repo.IsUserExist(id);
        return exist;
        
    }
    public async Task<bool> IsUserExist(User user)
    {
        bool exist = await _repo.IsUserExist(user);
        return exist;
    }
    public async Task<Guid> RegisterUserAsync(User user)
    {
        var existing = await _repo.GetUserByEmail(user.Email);

        if (existing != null)
        {
            throw new Exception("Email already exist");
        }

        var userId = await _repo.AddUserAsync(user);
        if (userId == null)
        {
            throw new Exception("Failed to create user.");
        }

        return userId.Value;
    }
    public async Task<User> GetUserById(int id)
    {
        User? user = await _repo.GetUserById(id);
        if (null == user)
        {
            return null;
        }

        return user;
    }
    public async Task<User> GetUserByEmail(string email)
    {
        User? user = await _repo.GetUserByEmail(email);
        if (user == null)
        {
            throw new Exception("User does not exist.");
        }

        return user;
    }

    public async Task DeleteUser(Guid id)
    {
        await _repo.DeleteUser(id);
    }

    public async Task PatchUser(User user)
    {
        await _repo.PatchUser(user);
    }
    
}