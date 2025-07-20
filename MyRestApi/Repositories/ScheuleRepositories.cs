using Dapper;
using MyRestApi.Models;
using System.Data;

namespace MyRestApi.Repositories
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly IDbConnection _db;

        public ScheduleRepository(IDbConnection db)
        {
            _db = db;
        }

        public async Task<Guid> CreateSchedule(Schedule schedule)
        {
            const string sql = @"
                INSERT INTO schedules (movie_id, theater_id, start_time, end_time)
                VALUES (@MovieId, @TheaterId, @StartTime, @EndTime)
                RETURNING id;
            ";

            var parameters = new DynamicParameters();
            parameters.Add("MovieId", schedule.MovieId, DbType.Guid);
            parameters.Add("TheaterId", schedule.TheaterId, DbType.Guid);
            parameters.Add("StartTime", schedule.StartTime, DbType.DateTime);
            parameters.Add("EndTime", schedule.EndTime, DbType.DateTime);

            return (Guid)(await _db.ExecuteScalarAsync(sql, parameters))!;
        }

        public async Task<IEnumerable<Schedule>> GetAllSchedules()
        {
            const string sql = @"
                SELECT 
                    id,
                    movie_id AS MovieId,
                    theater_id AS TheaterId,
                    start_time AS StartTime,
                    end_time AS EndTime
                FROM schedules";
            return await _db.QueryAsync<Schedule>(sql);
        }

        public async Task<IEnumerable<Schedule>> GetSchedulesByMovie(Guid movieId)
        {
            const string sql = @"
                SELECT 
                    id,
                    movie_id AS MovieId,
                    theater_id AS TheaterId,
                    start_time AS StartTime,
                    end_time AS EndTime
                FROM schedules
                WHERE movie_id = @MovieId;";

            var parameters = new DynamicParameters();
            parameters.Add("MovieId", movieId, DbType.Guid);

            return await _db.QueryAsync<Schedule>(sql, parameters);
        }

        public async Task<bool> DeleteSchedule(Guid id)
        {
            const string sql = "DELETE FROM schedules WHERE id = @Id;";

            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Guid);

            return await _db.ExecuteAsync(sql, parameters) > 0;
        }
    }
}