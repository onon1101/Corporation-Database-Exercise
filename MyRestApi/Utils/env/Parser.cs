using System.ComponentModel;

namespace Utils;

static public class Parser
{
    static public Env Init(IConfiguration config)
    {
        var jwt = new Jwt
        {
            Key = config["Jwt:Key"] ?? "IHateNTUTThatsAApperarelyFuckingTrulyForMe",
            Issuer = config["jwt:Issuer"] ?? "MyRestApi",
            Audience = config["jwt:Audience"] ?? "MyRestApiClient",
        };

        return new Env
        {
            Jwt = jwt,
        };
    }
}