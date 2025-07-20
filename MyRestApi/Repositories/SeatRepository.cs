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

            var parameters = new DynamicParameters();
            parameters.Add("TheaterId", seat.TheaterId, DbType.Guid);
            parameters.Add("SeatNumber", seat.SeatNumber, DbType.String);

            return (Guid)(await _db.ExecuteScalarAsync(sql, parameters))!;
        }

        public async Task<List<Guid>> CreateSeatsBulk(IEnumerable<Seat> seats)
        {
            var seatIds = new List<Guid>();
            const string sql = @"
                INSERT INTO seats (theater_id, seat_number)
                VALUES (@TheaterId, @SeatNumber)
                RETURNING id;";

            if (_db.State != ConnectionState.Open)
                _db.Open();

            using var tx = _db.BeginTransaction();

            foreach (var seat in seats)
            {
                var parameters = new DynamicParameters();
                parameters.Add("TheaterId", seat.TheaterId, DbType.Guid);
                parameters.Add("SeatNumber", seat.SeatNumber, DbType.String);

                var id = (Guid)(await _db.ExecuteScalarAsync(sql, parameters, tx))!;
                seatIds.Add(id);
            }

            tx.Commit();
            return seatIds;
        }

        public async Task<IEnumerable<Seat>> GetSeatsByTheater(Guid theaterId)
        {
            const string sql = @"SELECT 
                id AS Id,
                theater_id AS TheaterId, 
                seat_number AS SeatNumber
             FROM seats WHERE theater_id = @TheaterId;";

            var parameters = new DynamicParameters();
            parameters.Add("TheaterId", theaterId, DbType.Guid);

            return await _db.QueryAsync<Seat>(sql, parameters);
        }

        public async Task<bool> DeleteSeat(Guid id)
        {
            const string sql = "DELETE FROM seats WHERE id = @Id;";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Guid);

            return await _db.ExecuteAsync(sql, parameters) > 0;
        }
    }
}