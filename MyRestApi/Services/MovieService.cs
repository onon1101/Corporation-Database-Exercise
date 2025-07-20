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

        public async Task<Result<Guid>> CreateMovieAsync(Movie movie)
        {
            var id = await _repo.CreateMovie(movie);
            return Result<Guid>.Success(id);
        }

        public async Task<Result<IEnumerable<Movie>>> GetAllMoviesAsync()
        {
            var list = await _repo.GetAllMovies();
            return Result<IEnumerable<Movie>>.Success(list);
        }

        public async Task<Result<Movie?>> GetMovieByIdAsync(Guid id)
        {
            var movie = await _repo.GetMovieById(id);
            return Result<Movie?>.Success(movie);
        }

        public async Task<Result<bool>> UpdateMovieAsync(Guid id, Movie movie)
        {
            var ok = await _repo.UpdateMovie(id, movie);
            return Result<bool>.Success(ok);
        }

        public async Task<Result<bool>> DeleteMovieAsync(Guid id)
        {
            var ok = await _repo.DeleteMovie(id);
            return Result<bool>.Success(ok);
        }
    }
}