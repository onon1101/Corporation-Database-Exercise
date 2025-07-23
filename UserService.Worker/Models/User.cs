namespace UserService.Worker.Models;

public class User
{
    public Guid Id { get; set; }
    public string Username { get; set; } = default!;
    public DateTime RegisteredAt { get; set; }
}