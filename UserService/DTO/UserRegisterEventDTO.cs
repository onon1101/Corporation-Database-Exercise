namespace Api.DTO;

public class UserRegisterEventDTO
{
    public Guid UserId { get; set; }
    public string Username { get; set; } = default!;
    public DateTime RegisteredAt { get; set; }
}