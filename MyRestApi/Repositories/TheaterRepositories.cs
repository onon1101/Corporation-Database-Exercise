using Dapper;
using MyRestApi.Models;
using System.Data;

namespace MyRestApi.Repositories
{
    public class TheaterRepository : ITheaterRepository
    {
        private readonly IDbConnection _db;

        public TheaterRepository(IDbConnection db)
        {
            _db = db;
        }

        public async Task<Result<Guid>> CreateTheater(Theater theater)
        {
            const string checkSql = @"SELECT id, name FROM theaters WHERE name = @Name OR location = @Location";
            var existingId = await _db.ExecuteScalarAsync<Guid?>(checkSql, new { name = theater.Name, location = theater.Location });

            if (existingId.HasValue)
            {
                return Result<Guid>.Fail("[Theater] the theater is existed.");
            }

            const string sql = @"
                INSERT INTO theaters (name, location, total_seats)
                VALUES (@Name, @Location, @TotalSeats)
                RETURNING id;
            ";

            return Result<Guid>.Success((Guid)(await _db.ExecuteScalarAsync(sql, theater))!);
        }

        public async Task<bool> DeleteTheater(Guid id)
        {
            const string sql = "DELETE FROM theaters WHERE id = @Id";
            return await _db.ExecuteAsync(sql, new { Id = id }) > 0;
        }

        public async Task<IEnumerable<Theater>> GetAllTheaters()
        {
            const string sql = "SELECT id, name, location, total_seats AS TotalSeats FROM theaters";
            return await _db.QueryAsync<Theater>(sql);
        }

        public async Task<Theater?> GetTheaterById(Guid id)
        {
            const string sql = "SELECT id, name, location, total_seats AS TotalSeats FROM theaters WHERE id = @Id";
            return await _db.QueryFirstOrDefaultAsync<Theater>(sql, new { Id = id });
        }

        public async Task<bool> UpdateTheater(Guid id, Theater data)
        {
            const string sql = @"
                UPDATE theaters
                SET name = COALESCE(@Name, name),
                    location = COALESCE(@Location, location),
                    total_seats = COALESCE(@TotalSeats, total_seats)
                WHERE id = @Id;
            ";
            return await _db.ExecuteAsync(sql, new
            {
                Id = id,
                data.Name,
                data.Location,
                data.TotalSeats
            }) > 0;
        }
    }
}