namespace MyRestApi.DTO
{
    public class CreateMovieDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; } // unit: mins.
        public string? Rating { get; set; }
        public string? PosterUrl { get; set; }
    }
}