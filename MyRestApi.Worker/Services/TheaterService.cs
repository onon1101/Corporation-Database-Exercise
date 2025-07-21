using MyRestApi.DTO;
using MyRestApi.Models;
using MyRestApi.Repositories;
using Utils;

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

        // public async Task<Result<Guid>> CreateTheaterAsync(Theater theater)
        public async Task<Result<Guid>> CreateTheaterAsync(CreateTheaterDTO dto)
        {
            var theater = new Theater
            {
                Name = dto.Name,
                Location = dto.Location,
                TotalSeats = dto.TotalSeats
            };
            var isExist = await _repo.IsTheaterExist(theater);

            if (isExist)
            {
                return Result<Guid>.Fail("[theater] ", ErrorStatusCode.UserIsExisted);
            }

            Result<Guid> createTheater = await _repo.CreateTheater(theater);
            if (!createTheater.IsSuccess)
            {
                return createTheater;
            }

            Guid theaterId = createTheater.Payload;

            var seats = new List<Seat>();
            for (int i = 1; i <= theater.TotalSeats; i++)
            {
                seats.Add(new Seat
                {
                    TheaterId = theaterId,
                    SeatNumber = i.ToString()
                });
            }

            await _seatRepo.CreateSeatsBulk(seats); 

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