namespace MyRestApi.DTO
{
    public class CreateReservationDTO
    {
        public Guid UserId { get; set; }
        public Guid ScheduleId { get; set; }
        public List<Guid> SeatIds { get; set; } = new();
    }
}