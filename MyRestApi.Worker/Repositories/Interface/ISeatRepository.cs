using MyRestApi.Models;

namespace MyRestApi.Repositories
{
    public interface ISeatRepository
    {
        Task<Guid> CreateSeat(Seat seat);
        Task<List<Guid>> CreateSeatsBulk(IEnumerable<Seat> seats);
        Task<IEnumerable<Seat>> GetSeatsByTheater(Guid theaterId);
        Task<bool> DeleteSeat(Guid id);
    }
}