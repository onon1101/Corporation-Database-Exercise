using Microsoft.AspNetCore.Mvc;
using MyRestApi.Models;

namespace MyRestApi.Services;

public interface IMovieService
{
    Task<List<Movie>> GetAllMovies(Guid id);
    Task<Movie> GetMovie(int movieId, Guid id);
    void Modify(int movieId, Guid id);
    void AddMovie(Movie movie, Guid id);
    void DeleteMovie(int movieId, Guid id);
}