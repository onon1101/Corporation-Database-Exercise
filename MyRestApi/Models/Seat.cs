namespace MyRestApi.Models;

public class Seat
{
    public int Id { set; get; }
    public int TheaterId { set; get; }
    public string SeatNumber { set; get; } = "";
}