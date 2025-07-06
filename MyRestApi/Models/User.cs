namespace MyRestApi.Models;

public class User
{
    public Guid Id { set; get; }
    public string Username { set; get; } = string.Empty;
    public string Email { set; get; } = string.Empty;
    public string Password { set; get; } = string.Empty;
}