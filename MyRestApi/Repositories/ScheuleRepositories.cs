using System.Data;
using Dapper;
using MyRestApi.Models;

namespace MyRestApi.Repositories;

public class ScheduleRepository : IScheduleRepository
{
    private readonly IDbConnection _db;

    public ScheduleRepository(IDbConnection db)
    {
        _db = db;
    }
    public async Task<Schedule?> GetScheduleById(int id)
    {
        const string sql = @"SELECT * FROM schedules WHERE id = @Id";
        return await _db.QuerySingleOrDefaultAsync<Schedule>(sql, new { Id = id } );
    }
    public async Task AddSchedule(Schedule schedule)
    {
        const string sql = @"INSERT INTO schedules (movie_id, theater_id, start_time, end_time)
        VALUES (MovieId, TheaterId, StartTime, @EndTime)";

        await _db.ExecuteAsync(sql, schedule);
    }
    public async Task ModifySchedule(Schedule schedule)
    {
        const string sql = @"UPDATE schedules
        SET movie_id = @MovieID, 
            theater_id = @TheaterId,
            start_time = @StartTime,
            end_time = @EndTime
        WHERE id = @Id";
        await _db.ExecuteAsync(sql, schedule);
    }
    public async Task DeleteSchedule(Schedule schedule)
    {
        const string sql = @"DELETE FROM schedules WHERE id = @Id";

        await _db.ExecuteAsync(sql, schedule);
    }
}