namespace MyRestApi.DTO
{
    public class UpdateMovieDTO
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int? Duration { get; set; }
        public string? Rating { get; set; }
        public string? PosterUrl { get; set; }
    }
}