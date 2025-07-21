using MyRestApi.DTO;
using MyRestApi.Models;

namespace MyRestApi.Services
{
    public interface IScheduleService
    {
        Task<Result<Guid>> CreateScheduleAsync(CreateScheduleDTO dto);
        Task<Result<IEnumerable<Schedule>>> GetAllSchedulesAsync();
        Task<Result<IEnumerable<Schedule>>> GetSchedulesByMovieAsync(Guid movieId);
        Task<Result<bool>> DeleteScheduleAsync(Guid id);
    }
}