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
            if (_db.State != ConnectionState.Open)
                _db.Open();

            using var tx = _db.BeginTransaction();

            const string insertReservation = @"
                INSERT INTO reservations (user_id, schedule_id, status, reserved_at)
                VALUES (@UserId, @ScheduleId, @Status::reservation_status, @ReservedAt)
                RETURNING id;
            ";

            var reservationParams = new DynamicParameters();
            reservationParams.Add("UserId", reservation.UserId, DbType.Guid);
            reservationParams.Add("ScheduleId", reservation.ScheduleId, DbType.Guid);
            reservationParams.Add("Status", reservation.Status.ToString(), DbType.String); // 假設 Status 為 enum
            reservationParams.Add("ReservedAt", reservation.ReservedAt, DbType.DateTime);

            var reservationId = (Guid)(await _db.ExecuteScalarAsync(insertReservation, reservationParams, tx))!;

            const string insertSeat = @"
                INSERT INTO reservation_seats (reservation_id, seat_id)
                VALUES (@ReservationId, @SeatId);
            ";

            foreach (var seatId in seatIds)
            {
                var seatParams = new DynamicParameters();
                seatParams.Add("ReservationId", reservationId, DbType.Guid);
                seatParams.Add("SeatId", seatId, DbType.Guid);
                await _db.ExecuteAsync(insertSeat, seatParams, tx);
            }

            tx.Commit();
            return reservationId;
        }

        public async Task<IEnumerable<Reservation>> GetReservationsByUser(Guid userId)
        {
            const string sql = @"
                SELECT 
                    id AS Id,
                    user_id AS UserId,
                    schedule_id AS ScheduleId,
                    status AS Status,
                    reserved_at AS ReservedAt
                FROM reservations
                WHERE user_id = @UserId;
            ";

            var parameters = new DynamicParameters();
            parameters.Add("UserId", userId, DbType.Guid);

            return await _db.QueryAsync<Reservation>(sql, parameters);
        }

        public async Task<bool> DeleteReservation(Guid id)
        {
            using var tx = _db.BeginTransaction();

            var param = new DynamicParameters();
            param.Add("Id", id, DbType.Guid);

            await _db.ExecuteAsync("DELETE FROM reservation_seats WHERE reservation_id = @Id;", param, tx);
            var rows = await _db.ExecuteAsync("DELETE FROM reservations WHERE id = @Id;", param, tx);

            tx.Commit();
            return rows > 0;
        }
    }
}