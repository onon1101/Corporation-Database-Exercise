using MyRestApi.Models;

namespace MyRestApi.Services
{
    public interface ITheaterService
    {
        Task<Result<Guid>> CreateTheaterAsync(Theater theater);
        Task<bool> DeleteTheaterAsync(Guid id);
        Task<IEnumerable<Theater>> GetAllTheatersAsync();
        Task<Theater?> GetTheaterByIdAsync(Guid id);
        Task<bool> UpdateTheaterAsync(Guid id, Theater updateData);
    }
}