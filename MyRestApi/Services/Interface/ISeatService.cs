using MyRestApi.Models;

namespace MyRestApi.Services
{
    public interface ISeatService
    {
        Task<Guid> CreateSeatAsync(Seat seat);
        Task<IEnumerable<Seat>> GetSeatsByTheaterAsync(Guid theaterId);
        Task<bool> DeleteSeatAsync(Guid id);
    }
}