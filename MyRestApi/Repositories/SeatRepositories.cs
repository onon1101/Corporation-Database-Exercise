using System.Collections.Immutable;
using System.Data;
using Dapper;
using MyRestApi.Models;

namespace MyRestApi.Repositories;

public class SeatRepository: ISeatRepository
{
    private readonly IDbConnection _db;

    public SeatRepository(IDbConnection db)
    {
        _db = db;
    }
    public async Task<Seat?> GetSeatById(int id)
    {
        const string sql = @"SELECT * FROM seats WHERE Id = @id";
        return await _db.QuerySingleOrDefaultAsync<Seat>(sql, new { Id = id });
    }
    public async Task AddSeat(Seat seat)
    {
        const string sql = @"INSERT INTO seats (theater_id, seat_number) 
        VALUES (@TheaterId, @SeatNumber)";
        await _db.ExecuteAsync(sql, seat);
    }
    public async Task AddSeatByNumber(int theaterId, int number)
    {
        const string sql = @"INSERT INTO seats (theater_id, seat_number) VALUES (@TheaterId, @SeatNumber)";
        for (int i = 1; i <= number; i++)
        {
            await _db.ExecuteAsync(sql, new { TheaterId = theaterId, SeatNumber = $"A{i}" });
        }
    }
    public async Task ModifySeat(Seat seat)
    {
        const string sql = @"UPDATE seats 
        SET seat_number = @SeatNumber
        WHERE id = @Id";
        await _db.ExecuteAsync(sql, seat); 
    }
    public async Task DeleteSeat(int id)
    {
        const string sql = @"DELETE FROM seats WHERE id = @Id";
        await _db.ExecuteAsync(sql, new { Id = id});
    }
}