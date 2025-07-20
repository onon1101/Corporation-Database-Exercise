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

        public async Task<Result<Guid>> CreateScheduleAsync(Schedule schedule)
        {
            var id = await _repo.CreateSchedule(schedule);
            return Result<Guid>.Success(id);
        }

        public async Task<Result<IEnumerable<Schedule>>> GetAllSchedulesAsync()
        {
            var list = await _repo.GetAllSchedules();
            return Result<IEnumerable<Schedule>>.Success(list);
        }

        public async Task<Result<IEnumerable<Schedule>>> GetSchedulesByMovieAsync(Guid movieId)
        {
            var list = await _repo.GetSchedulesByMovie(movieId);
            return Result<IEnumerable<Schedule>>.Success(list);
        }

        public async Task<Result<bool>> DeleteScheduleAsync(Guid id)
        {
            var ok = await _repo.DeleteSchedule(id);
            return Result<bool>.Success(ok);
        }
    }
}