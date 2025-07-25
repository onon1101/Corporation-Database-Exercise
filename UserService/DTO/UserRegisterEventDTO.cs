namespace Api.DTO;

public class UserRegisterEventDTO
{
    // public Guid UserId { get; set; }
    public string Username { get; set; } = default!;

    public string Email { get; set; }

    public string Password { get; set; }

    public UInt32 PhoneNumber { get; set; }
    
    public DateTime RegisteredAt { get; set; }
}