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
            return (Guid)(await _db.ExecuteScalarAsync(sql, schedule))!;
        }

        public async Task<IEnumerable<Schedule>> GetAllSchedules()
        {
            const string sql = "SELECT * FROM schedules;";
            return await _db.QueryAsync<Schedule>(sql);
        }

        public async Task<IEnumerable<Schedule>> GetSchedulesByMovie(Guid movieId)
        {
            const string sql = "SELECT * FROM schedules WHERE movie_id = @MovieId;";
            return await _db.QueryAsync<Schedule>(sql, new { MovieId = movieId });
        }

        public async Task<bool> DeleteSchedule(Guid id)
        {
            const string sql = "DELETE FROM schedules WHERE id = @Id;";
            return await _db.ExecuteAsync(sql, new { Id = id }) > 0;
        }
    }
}