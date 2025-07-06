using MyRestApi.Models;

namespace MyRestApi.Services
{
    public interface IMovieService
    {
        Task<Guid> CreateMovieAsync(Movie movie);
        Task<IEnumerable<Movie>> GetAllMoviesAsync();
        Task<Movie?> GetMovieByIdAsync(Guid id);
        Task<bool> UpdateMovieAsync(Guid id, Movie movie);
        Task<bool> DeleteMovieAsync(Guid id);
    }
}