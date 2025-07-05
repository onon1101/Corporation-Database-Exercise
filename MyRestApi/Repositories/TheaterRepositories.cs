using System.Collections.Immutable;
using System.Data;
using Dapper;
using MyRestApi.Models;

namespace MyRestApi.Repositories;

public class TheaterRepositiry: ITheaterRepository
{
    private readonly IDbConnection _db;

    public TheaterRepositiry(IDbConnection db)
    {
        _db = db;
    }
    public async Task<Theater?> GetTheaterById(int idx)
    {
        const string sql = @"SELECT * FROM theaters WHERE id = @Id";
        return await _db.QuerySingleOrDefaultAsync<Theater>(sql, new { Id = idx});
    }
    public async Task<Theater?> GetTheaterByName(string name)
    {
        const string sql = @"SELECT * FROM theaters WHERE name = @Name";
        return await _db.QuerySingleOrDefaultAsync<Theater>(sql, new { Name = name });
    }

    public async Task<Theater?> GetTheaterByLocation(string location)
    {
        const string sql = @"SELECT * FROM theaters WHERE location = @Location";
        return await _db.QuerySingleOrDefaultAsync<Theater>(sql, new { Location = location});
    }

    public async Task<List<Theater>> GetAllTheater()
    {
        const string sql = @"SELECT * FROM theaters";
        return (await _db.QueryAsync<Theater>(sql)).ToList();
    }
    public async Task AddTheater(Theater theater)
    {
        const string sql = @"INSERT INTO theaters (name, location, total_seats)
        VALUES (@Name, @Location, @TotalSeats)";

        await _db.ExecuteAsync(sql, theater);
    }
    public async Task PatchTheater(Theater theater)
    {
        const string sql = @"UPDATE theaters
        SET total_seats = @TotalSeats
        WHERE id = @Id";
        await _db.ExecuteAsync(sql, theater);
    }
    public async Task DeleteTheaterById(int idx)
    {
        const string sql = @"DELETE FROM theaters WHERE id = @Id";
        await _db.ExecuteAsync(sql, new { Id = idx } );
    }
}