using MyRestApi.Models;

namespace MyRestApi.Services
{
    public interface ISeatService
    {
        Task<Result<Guid>> CreateSeatAsync(Seat seat);
        Task<Result<IEnumerable<Seat>>> GetSeatsByTheaterAsync(Guid theaterId);
        Task<Result<bool>> DeleteSeatAsync(Guid id);
    }
}