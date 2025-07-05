using Microsoft.AspNetCore.Mvc;
using MyRestApi.Models;
using MyRestApi.Services;

namespace MyRestApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MovieController : ControllerBase
{
    private readonly IMovieService _moviceService;
    public MovieController(IMovieService service)
    {
        _moviceService = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<Movie>>> GetAllMovie()
    {
        List<Movie> movies = await _moviceService.GetAllMovies();
        return Ok(movies);
    }
}
