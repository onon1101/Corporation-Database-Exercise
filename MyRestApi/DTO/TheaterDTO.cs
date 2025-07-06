namespace MyRestApi.DTO
{
    public class TheaterDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int TotalSeats { get; set; }
    }
}