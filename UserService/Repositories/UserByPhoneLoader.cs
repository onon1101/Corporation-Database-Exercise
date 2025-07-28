using System.Data;
using Api.DTO;
using Dapper;
using UserService.Repositories.Interface;

namespace UserService.Repositories;

public class UserByPhoneLoader: ICacheLoader<string, UserGetByPhoneNumberResponseDTO>
{
    private readonly IDbConnection _db;

    public UserByPhoneLoader(IDbConnection db)
    {
        _db = db;
    }

    public Task<UserGetByPhoneNumberResponseDTO?> LoadAsync(string phoneNumber)
    {
        const string sql = @"SELECT username FROM users WHERE phonenumber = @phonenumber";

        var parameters = new DynamicParameters();
        
        parameters.Add("@phonenumber", phoneNumber);
        return _db.QueryFirstOrDefaultAsync<UserGetByPhoneNumberResponseDTO>(sql, parameters);
    }
}