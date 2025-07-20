using Microsoft.AspNetCore.Http.HttpResults;
using MyRestApi.Models;
using MyRestApi.Repositories;
using MyRestApi.Services;
using Utils;
using Serilog;
using MyRestApi.DTO;

public class UserService : IUserService
{
    private readonly IUserRepository _repo;

    public UserService(IUserRepository repo)
    {
        _repo = repo;
    }
    public async Task<Result<Guid>> RegisterUserAsync(UserRegisterDTO user)
    {
        bool isExist = await _repo.IsUserExist(user);
        if (isExist)
        {
            Log.Information($"user {user.Username} has alreadly been registered.");
            return Result<Guid>.Fail("unknow problem", ErrorStatusCode.UnknowIssue);
        }
        return await _repo.CreateUser(user);
    }

    public async Task<Result<User>> AuthenticateUserAsync(string email, string password)
    {
        var userEmail = await _repo.GetUserByEmail(email);
        if (userEmail == null || userEmail.Password != password)
        {
            Log.Information($"Email: {email} not found, or wrong password.");
            return Result<User>.Fail("unknow problem", ErrorStatusCode.UserIsNotExisted);
        }
        return Result<User>.Success(userEmail);
    }

    public async Task<Result<User>> AuthenticateUsernameAsync(string name, string password)
    {
        var user = await _repo.GetUserByName(name);
        if (user == null || user.Password != password)
        {
            return Result<User>.Fail("unknow problem", ErrorStatusCode.UserIsNotExisted);
        }
        return Result<User>.Success(user);
    }

    public async Task<Result<User>> AuthenticateUserMutilAsync(UserLoginEmailOrUname payload)
    {
        // 嘗試用 Email 驗證
        var userByEmail = await _repo.GetUserByEmail(payload.Value);
        if (userByEmail != null && userByEmail.Password == payload.Password)
        {
            return Result<User>.Success(userByEmail);
        }

        // 嘗試用 Username 驗證
        var userByName = await _repo.GetUserByName(payload.Value);
        if (userByName != null && userByName.Password == payload.Password)
        {
            return Result<User>.Success(userByName);
        }

        // 兩者都無效
        Log.Information($"Login failed for {payload.Value}: not found or wrong password.");
        return Result<User>.Fail("unknow problem", ErrorStatusCode.UserIsNotExisted);
    }
    public async Task<Result<User>> GetUserById(Guid id)
    {
        var result = await _repo.GetUserById(id);
        if (null == result)
        {
            return Result<User>.Fail("unknow problem", ErrorStatusCode.UnknowIssue);
        }

        return Result<User>.Success(result);
    }

    public async Task DeleteUser(Guid id)
    => await _repo.DeleteUser(id);
}