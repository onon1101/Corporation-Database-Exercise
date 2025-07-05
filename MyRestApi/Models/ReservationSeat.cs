namespace MyRestApi.Models
{
    public class ReservationSeat
    {
        public Guid Id { get; set; }
        public Guid ReservationId { get; set; }
        public Guid SeatId { get; set; }
    }
}