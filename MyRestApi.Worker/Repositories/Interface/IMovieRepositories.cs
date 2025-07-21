using MyRestApi.Models;

namespace MyRestApi.Repositories
{
    public interface IMovieRepository
    {
        Task<Guid> CreateMovie(Movie movie);
        Task<IEnumerable<Movie>> GetAllMovies();
        Task<Movie?> GetMovieById(Guid id);
        Task<bool> UpdateMovie(Guid id, Movie movie);
        Task<bool> DeleteMovie(Guid id);
    }
}