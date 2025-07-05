using MyRestApi.Models;

namespace MyRestApi.Services
{
    public interface IReservationService
    {
        Task<Guid> CreateReservationAsync(Reservation reservation, List<Guid> seatIds);
        Task<IEnumerable<Reservation>> GetReservationsByUserAsync(Guid userId);
        Task<bool> DeleteReservationAsync(Guid reservationId);
    }
}