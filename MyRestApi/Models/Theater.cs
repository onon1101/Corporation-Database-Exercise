namespace MyRestApi.Models;

public class Theater
{
    public int Id { set; get; }
    public string Name { set; get; } = string.Empty;
    public string Location { set; get; } = string.Empty;
    public int TotalSeats { set; get; }
}