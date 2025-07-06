namespace MyRestApi.DTO
{
    public class CreateSeatDTO
    {
        public Guid TheaterId { get; set; }
        public string SeatNumber { get; set; }
    }
}