using MyRestApi.Models;

namespace MyRestApi.Services
{
    public interface IMovieService
    {
        Task<Result<Guid>> CreateMovieAsync(Movie movie);
        Task<Result<IEnumerable<Movie>>> GetAllMoviesAsync();
        Task<Result<Movie>> GetMovieByIdAsync(Guid id);
        Task<Result<bool>> UpdateMovieAsync(Guid id, Movie movie);
        Task<Result<bool>> DeleteMovieAsync(Guid id);
    }
}