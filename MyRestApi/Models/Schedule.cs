namespace MyRestApi.Models;

public class Schedule
{
    public int Id { set; get; }
    public int MovieId { set; get; }
    public int TheaterId { set; get; }
    public int StartTime{ set; get; }
    public int EndTime{ set; get; }
}