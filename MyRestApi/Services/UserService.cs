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
    public async Task<Guid> RegisterUserAsync(User user)
    {
        return await _repo.CreateUser(user);
    }

    public async Task<User?> AuthenticateUserAsync(string email, string password)
    {
        var user = await _repo.GetUserByEmail(email);
        if (user == null || user.Password != password)
        {
            return null;
        }
        return user;
    }

    public async Task<User?> GetUserById(Guid id)
    {
        return await _repo.GetUserById(id);
    }
}