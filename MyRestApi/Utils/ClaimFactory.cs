using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Utils;
using MyRestApi.Models;

public class ClaimFactory
{

    public ClaimsIdentity CreateIdentity(User user, Guid userId)
    {
        return new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim("UserId", userId.ToString())
        });
    }

    // public ClaimsIdentity CreateIdentity(User newUser, Guid registeredUserId)
    // {
    //      return new ClaimsIdentity(new[]
    //     {
    //         new Claim(ClaimTypes.Name, user.Username),
    //         new Claim(ClaimTypes.Email, user.Email),
    //         new Claim("UserId", userId.ToString())
    //     });
    // }
}

// public static string Generate(Env _env, ClaimsIdentity identity)
//     {
//         var tokenHandler = new JwtSecurityTokenHandler();
//         var key = Encoding.UTF8.GetBytes(_env.Jwt.Key);
//         var tokenDescriptor = new SecurityTokenDescriptor
//         {
//             Subject = identity,
//             Expires = DateTime.UtcNow.AddHours(1),
//             Issuer = _env.Jwt.Issuer,
//             Audience = _env.Jwt.Audience,
//             SigningCredentials = new SigningCredentials(
//                 new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
//         };

//         var token = tokenHandler.CreateToken(tokenDescriptor);
//         var result = tokenHandler.WriteToken(token);

//         return result;
//     }