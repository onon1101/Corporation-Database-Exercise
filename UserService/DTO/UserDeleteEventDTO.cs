namespace Api.DTO;

public class UserDeleteEventDTO
{
    public string Username { get; set; }
    
    public string Password { get; set; }
    
    public DateTime DeletedOn { get; set; }
}