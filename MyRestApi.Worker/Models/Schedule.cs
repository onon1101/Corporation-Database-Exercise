namespace MyRestApi.Models
{
    public class Schedule
    {
        public Guid Id { get; set; }
        public Guid MovieId { get; set; }
        public Guid TheaterId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}