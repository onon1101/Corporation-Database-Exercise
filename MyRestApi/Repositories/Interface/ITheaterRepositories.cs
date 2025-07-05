using MyRestApi.Models;

namespace MyRestApi.Repositories;

public interface ITheaterRepository
{
    // get
    Task<Theater?> GetTheaterById(int idx);
    Task<Theater?> GetTheaterByName(string name);
    Task<Theater?> GetTheaterByLocation(string location); // hard
    Task<List<Theater>> GetAllTheater();
    // post
    Task AddTheater(Theater theater);
    //patch
    Task PatchTheater(Theater theater);
    //delete
    Task DeleteTheaterById(int idx);
}