namespace MyRestApi.Models
{
    public class Seat
    {
        public Guid Id { get; set; }
        public Guid TheaterId { get; set; }
        public string SeatNumber { get; set; }
    }
}