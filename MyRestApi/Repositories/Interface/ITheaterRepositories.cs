using MyRestApi.Models;

namespace MyRestApi.Repositories
{
    public interface ITheaterRepository
    {
        Task<bool> IsTheaterExist(Theater theater);
        Task<Result<Guid>> CreateTheater(Theater theater);
        Task<bool> DeleteTheater(Guid id);
        Task<IEnumerable<Theater>> GetAllTheaters();
        Task<Theater?> GetTheaterById(Guid id);
        Task<bool> UpdateTheater(Guid id, Theater updateData);
    }
}