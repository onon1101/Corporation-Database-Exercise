using MyRestApi.Models;

public interface ITheaterService
{
    Task Add(Theater theater, int movieId, Guid id);
    Task<Theater> GetAllTheater();
    Task<Theater> GetTheater();
    Task Modfiy();
    Task Delete();
}