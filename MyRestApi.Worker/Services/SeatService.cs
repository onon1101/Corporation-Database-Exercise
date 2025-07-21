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

        public async Task<Result<Guid>> CreateSeatAsync(Seat seat)
        {
            var result = await _repo.CreateSeat(seat);
            return Result<Guid>.Success(result);
        }

        public async Task<Result<IEnumerable<Seat>>> GetSeatsByTheaterAsync(Guid theaterId)
        {
            var result = await _repo.GetSeatsByTheater(theaterId);
            return Result<IEnumerable<Seat>>.Success(result);
        }

        public async Task<Result<bool>> DeleteSeatAsync(Guid id)
        {
            var result = await _repo.DeleteSeat(id);
            return Result<bool>.Success(result);
        }
    }
}