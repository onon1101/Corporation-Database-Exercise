using MyRestApi.Models;

namespace MyRestApi.Repositories;

public interface IReservationSeatRepository
{
    // get
    Task<ReservationSeat?> GetReservationSeat(int id);
    // Task ModifyReservationSeat(Seat seat);
    Task AddReservationSeat(Seat seat);
    Task DeleteReservationSeat(int id);
}