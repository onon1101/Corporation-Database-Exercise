using Dapper;
using MyRestApi.Models;
using System.Data;

namespace MyRestApi.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly IDbConnection _db;

        public ReservationRepository(IDbConnection db)
        {
            _db = db;
        }

        public async Task<Guid> CreateReservation(Reservation reservation, List<Guid> seatIds)
        {
            using var tx = _db.BeginTransaction();

            const string insertReservation = @"
                INSERT INTO reservations (user_id, schedule_id, status, reserved_at)
                VALUES (@UserId, @ScheduleId, @Status, @ReservedAt)
                RETURNING id;
            ";

            var reservationId = (Guid)(await _db.ExecuteScalarAsync(insertReservation, reservation, tx))!;

            foreach (var seatId in seatIds)
            {
                const string insertSeat = @"
                    INSERT INTO reservation_seats (reservation_id, seat_id)
                    VALUES (@ReservationId, @SeatId);
                ";
                await _db.ExecuteAsync(insertSeat, new { ReservationId = reservationId, SeatId = seatId }, tx);
            }

            tx.Commit();
            return reservationId;
        }

        public async Task<IEnumerable<Reservation>> GetReservationsByUser(Guid userId)
        {
            const string sql = "SELECT * FROM reservations WHERE user_id = @UserId;";
            return await _db.QueryAsync<Reservation>(sql, new { UserId = userId });
        }

        public async Task<bool> DeleteReservation(Guid id)
        {
            using var tx = _db.BeginTransaction();

            await _db.ExecuteAsync("DELETE FROM reservation_seats WHERE reservation_id = @Id;", new { Id = id }, tx);
            var rows = await _db.ExecuteAsync("DELETE FROM reservations WHERE id = @Id;", new { Id = id }, tx);

            tx.Commit();
            return rows > 0;
        }
    }
}