using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Utils;
namespace Utils;

public class JwtGenerator
{
    private readonly Env _env;
    public JwtGenerator(Env env)
    {
        _env = env;
    }

    public string CreateToken(ClaimsIdentity identity)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_env.Jwt.Key);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = identity,
            Expires = DateTime.UtcNow.AddHours(1),
            Issuer = _env.Jwt.Issuer,
            Audience = _env.Jwt.Audience,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var result = tokenHandler.WriteToken(token);

        return result;
    }
}