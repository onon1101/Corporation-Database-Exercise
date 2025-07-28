using System.Data;
using System.Text.Json;
using Api.DTO;
using Dapper;
// using Microsoft.EntityFrameworkCore.Storage;
using StackExchange.Redis;
using UserService.Repositories.Interface;

namespace UserService.Repositories;

public class UserRepository
{
    private readonly RedisCacheWithLoader<string, UserGetByPhoneNumberResponseDTO> _cache;

    public UserRepository(IDatabase redis, IDbConnection db)
    {
        var loader = new UserByPhoneLoader(db);
        _cache = new RedisCacheWithLoader<string, UserGetByPhoneNumberResponseDTO>(
            redis,
            // phone => $"user:phone:{phone}",
            loader
        );
    }

    public async Task<UserGetByPhoneNumberResponseDTO?> GetByPhoneNumber(UserGetByPhoneNumberRequestDTO dto)
    {
        return await _cache.GetAsync(dto.PhoneNumber, phone => $"user:phone:{dto.PhoneNumber}");
    }

}