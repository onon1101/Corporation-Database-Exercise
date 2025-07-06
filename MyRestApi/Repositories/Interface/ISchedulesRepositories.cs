using MyRestApi.Models;

namespace MyRestApi.Repositories
{
    public interface IScheduleRepository
    {
        Task<Guid> CreateSchedule(Schedule schedule);
        Task<IEnumerable<Schedule>> GetAllSchedules();
        Task<IEnumerable<Schedule>> GetSchedulesByMovie(Guid movieId);
        Task<bool> DeleteSchedule(Guid id);
    }
}