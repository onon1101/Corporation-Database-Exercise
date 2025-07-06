namespace MyRestApi.DTO
{
    public class MovieDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public string? Rating { get; set; }
        public string? PosterUrl { get; set; }
    }
}