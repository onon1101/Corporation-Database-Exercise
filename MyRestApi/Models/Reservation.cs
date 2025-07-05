namespace MyRestApi.Models;

public enum ReservationStatus
{
    Pending = 0,
    Paid,
    Cancelled
}
public class Reservation
{
    public int Id { set; get; }
    public int UserId { get; }
    public int ScheduleId { get; }
    public ReservationStatus Status { set; get; }
    public int ReservedAt { set; get; }
}