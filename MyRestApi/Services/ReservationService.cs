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

        public Task<Guid> CreateReservationAsync(Reservation reservation, List<Guid> seatIds)
            => _repo.CreateReservation(reservation, seatIds);

        public Task<IEnumerable<Reservation>> GetReservationsByUserAsync(Guid userId)
            => _repo.GetReservationsByUser(userId);

        public Task<bool> DeleteReservationAsync(Guid id)
            => _repo.DeleteReservation(id);
    }
}