using Microsoft.AspNetCore.Http.HttpResults;
using MyRestApi.Models;
using MyRestApi.Repositories;
using MyRestApi.Services;


public class TheaterService : ITheaterService
{
    private readonly IUserRepository _userRepo;
    private readonly ITheaterRepository _repo;

    public TheaterService(IUserRepository userRepo, ITheaterRepository repo)
    {
        _repo = repo;
        _userRepo = userRepo;
    }

    public async Task Add(Theater theater, int movieId, Guid id)
    {
        bool exist = await _userRepo.IsUserExist(id);
        if (!exist)
        {
            throw new Exception("user not found");
        }

        
    }

}