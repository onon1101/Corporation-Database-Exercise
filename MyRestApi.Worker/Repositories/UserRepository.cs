using Dapper;
using System.Data;
using MyRestApi.Models;
using MyRestApi.DTO;

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
        var parameters = new DynamicParameters();
        parameters.Add("Id", id, DbType.Guid);
        var result = await _db.QuerySingleOrDefaultAsync<int?>(sql, parameters);
        return result.HasValue;
    }

    public async Task<bool> IsUserExist(UserRegisterDTO user)
    {
        const string sql = @"SELECT 1 FROM users WHERE username = @Username AND email = @Email";
        var parameters = new DynamicParameters();
        parameters.Add("Username", user.Username, DbType.String);
        parameters.Add("Email", user.Email, DbType.String);
        var result = await _db.QuerySingleOrDefaultAsync<int?>(sql, parameters);
        return result.HasValue;
    }

    public async Task<Result<Guid>> CreateUser(UserRegisterDTO user)
    {
        const string sql = @"INSERT INTO users (Username, Email, Password)
                             VALUES (@Username, @Email, @Password)
                             RETURNING id;";
        var parameters = new DynamicParameters();
        parameters.Add("Username", user.Username, DbType.String);
        parameters.Add("Email", user.Email, DbType.String);
        parameters.Add("Password", user.Password, DbType.String);

        var result = await _db.ExecuteScalarAsync(sql, parameters);
        var id = result as Guid?;
        if (id == null)
            throw new Exception("Insert user failed: ID is null.");
        return Result<Guid>.Success(id.Value);
    }

    public async Task<User?> GetUserById(Guid id)
    {
        const string sql = @"SELECT * FROM users WHERE id = @Id;";
        var parameters = new DynamicParameters();
        parameters.Add("Id", id, DbType.Guid);
        return await _db.QueryFirstOrDefaultAsync<User>(sql, parameters);
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        const string sql = @"SELECT * FROM users WHERE email = @Email";
        var parameters = new DynamicParameters();
        parameters.Add("Email", email, DbType.String);
        return await _db.QueryFirstOrDefaultAsync<User>(sql, parameters);
    }

    public async Task<User?> GetUserByName(string name)
    {
        const string sql = @"SELECT * FROM users WHERE username = @Username";
        var parameters = new DynamicParameters();
        parameters.Add("Username", name, DbType.String);
        return await _db.QueryFirstOrDefaultAsync<User>(sql, parameters);
    }

    public async Task PatchUser(User user)
    {
        const string sql = @"UPDATE users
                             SET username = @Username,
                                 email = @Email,
                                 password = @Password
                             WHERE id = @Id";
        var parameters = new DynamicParameters();
        parameters.Add("Id", user.Id, DbType.Guid);
        parameters.Add("Username", user.Username, DbType.String);
        parameters.Add("Email", user.Email, DbType.String);
        parameters.Add("Password", user.Password, DbType.String);
        await _db.ExecuteAsync(sql, parameters);
    }

    public async Task DeleteUser(Guid id)
    {
        // const string sql = @"DELETE FROM users WHERE id = @Id";
        const string sql = @"UPDATE users
                             SET is_deleted = TRUE
                             WHERE id = @Id";
        var parameters = new DynamicParameters();
        parameters.Add("Id", id, DbType.Guid);
        await _db.ExecuteAsync(sql, parameters);
    }

    public async Task<UserRole> GetUserPermission(Guid userId)
    {
        const string sql = @"SELECT permission
                             FROM users
                             WHERE id = @Id";

        var parameters = new DynamicParameters();
        parameters.Add("Id", DbType.Guid);
        var userPermissionCode = await _db.QueryFirstOrDefaultAsync<int>(sql, parameters);
        return (UserRole)userPermissionCode;
    }
}