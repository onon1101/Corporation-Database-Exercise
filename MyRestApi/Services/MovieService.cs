using System.ComponentModel;
using MyRestApi.Models;
using MyRestApi.Services;
using MyRestApi.Repositories;
using System.Diagnostics;
using Microsoft.AspNetCore.Routing.Constraints;

public class MovieService : IMovieService
{
    private readonly IMovieRepository _repo;
    private readonly IUserService _userService;

    public MovieService(IMovieRepository repo, IUserService userService)
    {
        _repo = repo;
        _userService = userService;
    }

    public async Task<List<Movie>> GetAllMovies(Guid guid)
    {
        var isUserExist = await _userService.IsUserExist(guid);
        if (!isUserExist)
        {
            throw new Exception("user not found");
        }

        List<Movie>? movies = await _repo.GetAllMovies();
        if (null == movies)
        {
            throw new Exception("here is no any movie.");
        }

        return movies;
    }

    public async Task<Movie> GetMovie(int movieId, Guid id)
    {
        var isUserExist = await _userService.IsUserExist(id);
        if (!isUserExist)
        {
            throw new Exception("user not found");

        }

        Movie? movie = await _repo.GetMovieById(movieId);
        if (movie == null)
        {
            throw new Exception("movie not found");
        }
        return movie;
    }

    public async void Modify(int movieId, Guid id)
    {
        var isUserExist = await _userService.IsUserExist(id);
        if (!isUserExist)
        {
            throw new Exception("user not found");

        }

        Movie? movie = await _repo.GetMovieById(movieId);
        if (movie == null)
        {
            throw new Exception("movie not found");
        }


    }

    public async void AddMovie(Movie movie, Guid id)
    {
        var isUserExist = await _userService.IsUserExist(id);

        if (!isUserExist)
        {
            throw new Exception("user not found");
        }
        await _repo.AddMovie(movie);
    }

    public async void DeleteMovie(int movieId, Guid id)
    {
        var isUserExist = await _userService.IsUserExist(id);
        if (!isUserExist)
        {
            throw new Exception("user not found");
        }
        await _repo.DeleteMovieById(movieId);
    }
}