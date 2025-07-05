namespace MyRestApi.Models;

public class ReservationSeat
{
    public int id { set; get; }
    public int ReservationId { get; }
    public int SeatId { set; get; }
}