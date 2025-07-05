using MyRestApi.Models;

namespace MyRestApi.Repositories;

public interface IReservationRepositories
{
    // get
    Task<Reservation?> GetReservationById(int id);

    // post
    Task<Reservation> AddReservation(Reservation reservate);

    // patch
    Task<Reservation> ModifyReservation(Reservation reservate);

    Task DeleteReservation(int id);
}