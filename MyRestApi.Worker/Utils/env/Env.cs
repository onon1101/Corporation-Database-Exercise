namespace Utils;

public class Env
{
    required public Jwt Jwt;
}


public class Jwt
{
    public string Key { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
}
