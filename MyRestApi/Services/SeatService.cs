using MyRestApi.Models;
using MyRestApi.Repositories;

namespace MyRestApi.Services
{
    public class SeatService : ISeatService
    {
        private readonly ISeatRepository _repo;

        public SeatService(ISeatRepository repo)
        {
            _repo = repo;
        }

        public Task<Guid> CreateSeatAsync(Seat seat) => _repo.CreateSeat(seat);
        public Task<IEnumerable<Seat>> GetSeatsByTheaterAsync(Guid theaterId) => _repo.GetSeatsByTheater(theaterId);
        public Task<bool> DeleteSeatAsync(Guid id) => _repo.DeleteSeat(id);
    }
}