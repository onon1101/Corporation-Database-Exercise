using MyRestApi.DTO;
using MyRestApi.Models;

namespace MyRestApi.Services
{
    public interface IReservationService
    {
        // Task<Result<Guid>> CreateReservationAsync(Reservation reservation, List<Guid> seatIds);
        Task<Result<Guid>> CreateReservationAsync(CreateReservationDTO dto);
        Task<Result<IEnumerable<Reservation>>> GetReservationsByUserAsync(Guid userId);
        Task<Result<bool>> DeleteReservationAsync(Guid reservationId);
    }
}