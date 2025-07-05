using MyRestApi.Models;
using MyRestApi.Repositories;

namespace MyRestApi.Services
{
    public class TheaterService : ITheaterService
    {
        private readonly ITheaterRepository _repo;
        private readonly ISeatRepository _seatRepo;

        public TheaterService(ITheaterRepository repo, ISeatRepository seatRepo)
        {
            _repo = repo;
            _seatRepo = seatRepo;
        }

        public async Task<Result<Guid>> CreateTheaterAsync(Theater theater)
        {
            Result<Guid> createTheater = await _repo.CreateTheater(theater);

            if (string.Empty != createTheater.Error)
            {
                return createTheater;
            }

            Guid theaterId = (Guid)createTheater.Ok;

            for (int i = 1; i < theater.TotalSeats + 1; i++)
            {
                var seat = new Seat
                {
                    TheaterId = theaterId,
                    SeatNumber = i.ToString()
                };
                await _seatRepo.CreateSeat(seat);
            }

            
            return Result<Guid>.Success(theaterId);
        } 

        public Task<bool> DeleteTheaterAsync(Guid id)
            => _repo.DeleteTheater(id);

        public Task<IEnumerable<Theater>> GetAllTheatersAsync()
            => _repo.GetAllTheaters();

        public Task<Theater?> GetTheaterByIdAsync(Guid id)
            => _repo.GetTheaterById(id);

        public Task<bool> UpdateTheaterAsync(Guid id, Theater data)
            => _repo.UpdateTheater(id, data);
    }
}