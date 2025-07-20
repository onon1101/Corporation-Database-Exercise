using MyRestApi.DTO;
using MyRestApi.Models;
using MyRestApi.Repositories;

namespace MyRestApi.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _repo;

        public ReservationService(IReservationRepository repo)
        {
            _repo = repo;
        }

        // public async Task<Result<Guid>> CreateReservationAsync(Reservation reservation, List<Guid> seatIds)
        public async Task<Result<Guid>> CreateReservationAsync(CreateReservationDTO dto)
        {
            var reservation = new Reservation
            {
                UserId = dto.UserId,
                ScheduleId = dto.ScheduleId
            };
            var seatId = dto.SeatIds;
            var id = await _repo.CreateReservation(reservation, seatId);
            return Result<Guid>.Success(id);
        }

        public async Task<Result<IEnumerable<Reservation>>> GetReservationsByUserAsync(Guid userId)
    {
            var list = await _repo.GetReservationsByUser(userId);
            return Result<IEnumerable<Reservation>>.Success(list);
        }

        public async Task<Result<bool>> DeleteReservationAsync(Guid id)
        {
            var ok = await _repo.DeleteReservation(id);
            return Result<bool>.Success(ok);
        }
    }
}