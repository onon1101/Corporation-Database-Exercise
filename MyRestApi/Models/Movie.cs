namespace MyRestApi.Models;

public class Movie
{
    public int Id { set; get; }
    public string Title { set; get; } = "";
    public string Description { set; get; } = "";
    public int Duration { set; get; }
    public string Rating { set; get; } = "";
    public string PosterUrl { set; get; } = "";
}