using Dapper;
using MyRestApi.Models;
using System.Data;
using Utils;

namespace MyRestApi.Repositories
{
    public class TheaterRepository : ITheaterRepository
    {
        private readonly IDbConnection _db;

        public TheaterRepository(IDbConnection db)
        {
            _db = db;
        }

        public async Task<bool> IsTheaterExist(Theater theater)
        {
            const string sql = @"
            SELECT 1 
            FROM theaters
            WHERE
                name = @Name 
                OR location = @Location
            ";

            var parameters = new DynamicParameters();
            parameters.Add("Name", theater.Name, DbType.String);
            parameters.Add("Location", theater.Location, DbType.String);

            var result = await _db.ExecuteScalarAsync<Guid?>(sql, parameters);
            return result != null;
        }

        public async Task<Result<Guid>> CreateTheater(Theater theater)
        {
            const string sql = @"
                INSERT INTO theaters (name, location, total_seats)
                VALUES (@Name, @Location, @TotalSeats)
                RETURNING id;
            ";

            var parameters = new DynamicParameters();
            parameters.Add("Name", theater.Name, DbType.String);
            parameters.Add("Location", theater.Location, DbType.String);
            parameters.Add("TotalSeats", theater.TotalSeats, DbType.Int32);

            var result = await _db.ExecuteScalarAsync(sql, parameters);
            return Result<Guid>.Success((Guid)result!);
        }

        public async Task<bool> DeleteTheater(Guid id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Guid);

            const string sql = "DELETE FROM theaters WHERE id = @Id";
            return await _db.ExecuteAsync(sql, parameters) > 0;
        }

        public async Task<IEnumerable<Theater>> GetAllTheaters()
        {
            const string sql = "SELECT id, name, location, total_seats AS TotalSeats FROM theaters";
            return await _db.QueryAsync<Theater>(sql);
        }

        public async Task<Theater?> GetTheaterById(Guid id)
        {
            const string sql = "SELECT id, name, location, total_seats AS TotalSeats FROM theaters WHERE id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Guid);

            return await _db.QueryFirstOrDefaultAsync<Theater>(sql, parameters);
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

            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Guid);
            parameters.Add("Name", data.Name, DbType.String);
            parameters.Add("Location", data.Location, DbType.String);
            parameters.Add("TotalSeats", data.TotalSeats, DbType.Int32);

            return await _db.ExecuteAsync(sql, parameters) > 0;
        }
    }
}