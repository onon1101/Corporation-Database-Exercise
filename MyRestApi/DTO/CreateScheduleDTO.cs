namespace MyRestApi.DTO
{
    public class CreateScheduleDTO
    {
        public Guid MovieId { get; set; }
        public Guid TheaterId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}