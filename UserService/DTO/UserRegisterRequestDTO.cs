namespace Api.DTO;

public class UserRegisterRequestDTO
{
    public string Username { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public UInt32 PhoneNumber { get; set; }
}