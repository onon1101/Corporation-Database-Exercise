using MyRestApi.Models;

namespace MyRestApi.Repositories;

public interface ISeatRepository
{
    Task<Seat?> GetSeatById(int id);
    Task AddSeat(Seat seat);
    Task AddSeatByNumber(int theaterId, int number);
    Task ModifySeat(Seat seat);
    Task DeleteSeat(int id);
}