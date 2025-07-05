namespace MyRestApi.Models
{
    public class Reservation
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ScheduleId { get; set; }
        public string Status { get; set; } = "Pending";
        public DateTime ReservedAt { get; set; } = DateTime.UtcNow;
    }
}