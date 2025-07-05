using System.Data;
using Dapper;
using MyRestApi.Models;

namespace MyRestApi.Repositories;

public class ReservationSeatRepository : IReservationSeatRepository
{
    private readonly IDbConnection _db;

    public ReservationSeatRepository(IDbConnection db)
    {
        _db = db;
    }

    public async Task<ReservationSeat?> GetReservationSeat(int id)
    {
        const string sql = @"SELECT * FROM reservation_seats WHERE id = @Id";

        return await _db.QuerySingleOrDefaultAsync<ReservationSeat>(sql, id);
    }

    public async Task AddReservationSeat(Seat seat)
    {
        const string sql = @"INSERT INTO reservation_seats (reservation_id, seat_id)
        VALUES (@ReservationId, @SeatId)";
        await _db.ExecuteAsync(sql, seat);
    }
    public async Task DeleteReservationSeat(int id)
    {
        const string sql = @"DELETE FROM reservation_seats WHERE id = @Id";
        await _db.ExecuteAsync(sql, new { Id = id});
    }
}