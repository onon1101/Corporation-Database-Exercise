using MyRestApi.Models;

namespace MyRestApi.Services
{
    public interface IScheduleService
    {
        Task<Guid> CreateScheduleAsync(Schedule schedule);
        Task<IEnumerable<Schedule>> GetAllSchedulesAsync();
        Task<IEnumerable<Schedule>> GetSchedulesByMovieAsync(Guid movieId);
        Task<bool> DeleteScheduleAsync(Guid id);
    }
}