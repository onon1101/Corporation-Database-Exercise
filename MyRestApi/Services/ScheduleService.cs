using MyRestApi.Models;
using MyRestApi.Repositories;

namespace MyRestApi.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly IScheduleRepository _repo;

        public ScheduleService(IScheduleRepository repo)
        {
            _repo = repo;
        }

        public Task<Guid> CreateScheduleAsync(Schedule schedule) => _repo.CreateSchedule(schedule);
        public Task<IEnumerable<Schedule>> GetAllSchedulesAsync() => _repo.GetAllSchedules();
        public Task<IEnumerable<Schedule>> GetSchedulesByMovieAsync(Guid movieId) => _repo.GetSchedulesByMovie(movieId);
        public Task<bool> DeleteScheduleAsync(Guid id) => _repo.DeleteSchedule(id);
    }
}