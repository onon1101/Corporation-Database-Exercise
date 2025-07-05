using MyRestApi.Models;
using MyRestApi.Repositories;

namespace MyRestApi.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _repo;

        public MovieService(IMovieRepository repo)
        {
            _repo = repo;
        }

        public Task<Guid> CreateMovieAsync(Movie movie) => _repo.CreateMovie(movie);
        public Task<IEnumerable<Movie>> GetAllMoviesAsync() => _repo.GetAllMovies();
        public Task<Movie?> GetMovieByIdAsync(Guid id) => _repo.GetMovieById(id);
        public Task<bool> UpdateMovieAsync(Guid id, Movie movie) => _repo.UpdateMovie(id, movie);
        public Task<bool> DeleteMovieAsync(Guid id) => _repo.DeleteMovie(id);
    }
}