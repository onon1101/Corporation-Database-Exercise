using MyRestApi.Models;

namespace MyRestApi.Repositories
{
    public interface IReservationRepository
    {
        Task<Guid> CreateReservation(Reservation reservation, List<Guid> seatIds);
        Task<IEnumerable<Reservation>> GetReservationsByUser(Guid userId);
        Task<bool> DeleteReservation(Guid id);
    }
}