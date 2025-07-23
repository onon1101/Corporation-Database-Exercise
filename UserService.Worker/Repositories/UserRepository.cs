using Dapper;
using System.Data;
using Api.DTO;

namespace UserService.Worker.Repositories;

public class UserRepository(IDbConnection db)
{
    private readonly IDbConnection _db = db;

    public async Task RegisterUser(UserRegisterEventDTO user)
    {
        const string sql = @"INSERT INTO users (id, username, registered_at)
                            VALUES (@UserId, @Username, @RegisteredAt);";
        var parameters = new DynamicParameters();
        parameters.Add("UserId", user.UserId, DbType.Guid);
        parameters.Add("Username", user.Username, DbType.String);
        parameters.Add("RegisteredAt", user.RegisteredAt, DbType.DateTime);

        await _db.ExecuteAsync(sql, parameters);
    }
    
}