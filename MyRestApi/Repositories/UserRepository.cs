using Dapper;
using System.Data;
using MyRestApi.Models;

namespace MyRestApi.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IDbConnection _db;

    public UserRepository(IDbConnection db)
    {
        _db = db;
    }

    public async Task<bool> IsUserExist(Guid id)
    {
        const string sql = @"SELECT 1 FROM users WHERE id = @Id";
        var result = await _db.QuerySingleOrDefaultAsync<int?>(sql, new { Id = id });
        return result.HasValue;
    }
    public async Task<bool> IsUserExist(User user)
    {
        const string sql = @"SELECT 1 FROM users WHERE id = @Id";
        var result = await _db.QuerySingleOrDefaultAsync<int?>(sql, new { user.Id });
        return result.HasValue;
    }
    public async Task<Guid> CreateUser(User user)
    {
        const string sqlQuire = @"INSERT INTO users (Username, Email, Password)
        VALUES (@Username, @Email, @Password)
        RETURNING id;
        ";

        var result = await _db.ExecuteScalarAsync(sqlQuire, user);
        // var result = await _db.QueryAsync(sqlQuire, user);

        Guid? id = result as Guid?;
        if (id == null)
        {
            throw new Exception("Insert user failed: ID is null.");
        }
        return id.Value;
    }

    public async Task<User?> GetUserById(Guid id)
    {
        const string sql = @"SELECT * FROM users WHERE id = @Id;";
        return await _db.QueryFirstOrDefaultAsync<User>(sql, new { Id = id });
    }
    public async Task<User?> GetUserByEmail(string email)
    {
        const string sql_quire = @"SELECT * FROM users WHERE email = @Email";
        return await _db.QueryFirstOrDefaultAsync<User>(sql_quire, new { Email = email });
    }
    public async Task PatchUser(User user)
    {
        const string sql = @"UPDATE users
        SET username = @Username,
            email = @Email
            password = @Password
        WHERE id = @Id";

        await _db.ExecuteAsync(sql, user);
    }
    public async Task DeleteUser(Guid id)
    {
        const string sql = @"DELETE FROM users WHERE id = @Id";
        await _db.ExecuteAsync(sql, new { Id = id });

    }

}