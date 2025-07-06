namespace MyRestApi.Models;

public class Theater
{
    public Guid id { set; get; }
    public string Name { set; get; } = string.Empty;
    public string Location { set; get; } = string.Empty;
    public int TotalSeats { set; get; }
}