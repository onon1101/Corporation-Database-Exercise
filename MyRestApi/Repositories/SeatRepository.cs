using Dapper;
using MyRestApi.Models;
using System.Data;

namespace MyRestApi.Repositories
{
    public class SeatRepository : ISeatRepository
    {
        private readonly IDbConnection _db;

        public SeatRepository(IDbConnection db)
        {
            _db = db;
        }

        public async Task<Guid> CreateSeat(Seat seat)
        {
            const string sql = @"
                INSERT INTO seats (theater_id, seat_number)
                VALUES (@TheaterId, @SeatNumber)
                RETURNING id;";
            return (Guid)(await _db.ExecuteScalarAsync(sql, seat))!;
        }

        public async Task<IEnumerable<Seat>> GetSeatsByTheater(Guid theaterId)
        {
            const string sql = "SELECT * FROM seats WHERE theater_id = @TheaterId;";
            return await _db.QueryAsync<Seat>(sql, new { TheaterId = theaterId });
        }

        public async Task<bool> DeleteSeat(Guid id)
        {
            const string sql = "DELETE FROM seats WHERE id = @Id;";
            return await _db.ExecuteAsync(sql, new { Id = id }) > 0;
        }
    }
}