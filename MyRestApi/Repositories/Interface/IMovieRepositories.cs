using MyRestApi.Models;

namespace MyRestApi.Repositories;

public interface IMovieRepository
{
    Task AddMovie(Movie movie); // post
    Task PatchMovie(Movie movie); // patch
    Task<List<Movie>?> GetAllMovies(); // get
    Task<Movie?> GetMovieById(int idx); // get one.
    Task DeleteMovieById(int idx); // delete
}