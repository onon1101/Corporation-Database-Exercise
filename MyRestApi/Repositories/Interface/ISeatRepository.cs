using MyRestApi.Models;

namespace MyRestApi.Repositories
{
    public interface ISeatRepository
    {
        Task<Guid> CreateSeat(Seat seat);
        Task<IEnumerable<Seat>> GetSeatsByTheater(Guid theaterId);
        Task<bool> DeleteSeat(Guid id);
    }
}