using System.Data;
using MyRestApi.Models;
using Dapper;

namespace MyRestApi.Repositories;

public class ReservationRepository : IReservationRepositories
{
    private readonly IDbConnection _db;

    public ReservationRepository(IDbConnection db)
    {
        _db = db;
    }
    public async Task<Reservation?> GetReservationById(int id)
    {
        const string sql = @"SELECT * FROM reservations WHERE id = @Id";
        return await _db.QuerySingleOrDefaultAsync<Reservation>(sql, new { Id = id });

    }
    public async Task<Reservation> AddReservation(Reservation reservate)
    {
        const string sql = @"INSERT INTO reservations (user_id, schedule_id, status)
                         VALUES (@UserId, @ScheduleId, @Status)
                         RETURNING *";
        var result = await _db.QuerySingleAsync<Reservation>(sql, reservate);
        return result;
    }
    public async Task<Reservation> ModifyReservation(Reservation reservate)
    {
        const string sql = @"UPDATE reservations
        SET reservations_status = @ReservationsStatus
        WHERE id = Id";
        return await _db.QuerySingleAsync<Reservation>(sql, reservate);
    }
    public async Task DeleteReservation(int id)
    {
        const string sql = @"DELETE FROM reservations WHERE id = @Id";
        await _db.ExecuteAsync(sql, id);
    }
}