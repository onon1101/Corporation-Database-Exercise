using MyRestApi.DTO;
using Microsoft.AspNetCore.Mvc;
using MyRestApi.Models;
using MyRestApi.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Utils;
using Microsoft.AspNetCore.Authentication.OAuth;
using Serilog;
using System.Reflection.Metadata.Ecma335;

namespace MyRestApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class UserController : ControllerBase
{
    private readonly ClaimFactory _claimFactory;
    private readonly JwtGenerator _jwtGenerator;
    private readonly IUserService _userService;
    private readonly Env _env;

    public UserController(IUserService userService, IConfiguration config)
    {
        _userService = userService;
        // _env = parser(config;
        _env = Parser.Init(config);
        _jwtGenerator = new JwtGenerator(_env);
        _claimFactory = new ClaimFactory();
    }

    /// <summary>
    /// 使用者註冊
    /// </summary>
    /// <remarks>
    /// 建立新使用者帳號。輸入使用者名稱、Email 與密碼。
    /// </remarks>
    /// <param name="dto">註冊資訊</param>
    /// <returns>回傳使用者基本資料與 ID</returns>
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] UserRegisterDTO dto)
    {
        var regUser = await _userService.RegisterUserAsync(dto);
        if (!regUser.IsSuccess)
        {
            return BadRequest(regUser);
        }
        Guid id = regUser.Payload;

        var identity = _claimFactory.CreateIdentity(dto, id);
        var jwtToken = _jwtGenerator.CreateToken(identity);

        return Ok(new { token = jwtToken });
    }

    /// <summary>
    /// 使用者登入
    /// </summary>
    /// <remarks>
    /// 驗證 Email 與密碼是否正確，登入成功後回傳使用者資訊。
    /// </remarks>
    /// <param name="dto">登入資訊</param>
    /// <returns>使用者資訊或 Unauthorized</returns>
    // [Authorize]
    [HttpPost("by-email")]
    public async Task<IActionResult> Login([FromBody] UserEmailLoginDTO dto) //TODO: ttt
    {
        var result = await _userService.AuthenticateUserAsync(dto.Email, dto.Password);
        if (!result.IsSuccess)
        {
            return Unauthorized(new { result });
        }
        User user = result.Payload;

        var identity = _claimFactory.CreateIdentity(user, user.Id);
        var jwtToken = _jwtGenerator.CreateToken(identity);

        var response = new LoginResponseDTO
        {
            Token = jwtToken,
            Id = user.Id,
            Username = user.Username,
            Email = user.Email
        };

        return Ok(response);
    }

    [HttpPost("by-username")]
    public async Task<IActionResult> Login([FromBody] UsernameLoginDTO dto)
    {
        var result = await _userService.AuthenticateUsernameAsync(dto.Username, dto.Password);
        if (!result.IsSuccess)
        {
            return Unauthorized(result);
        }
        var user = result.Payload;

        // jwt
        var identity = _claimFactory.CreateIdentity(user, user.Id);
        var jwtToken = _jwtGenerator.CreateToken(identity);

        var response = new LoginResponseDTO
        {
            Token = jwtToken,
            Id = user.Id,
            Username = user.Username,
            Email = user.Email
        };

        return Ok(Result<LoginResponseDTO>.Success(response));
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] UserLoginEmailOrUname dto)
    {
        var result = await _userService.AuthenticateUserMutilAsync(dto);
        if (!result.IsSuccess)
        {
            return Unauthorized(result);
        }
        var user = result.Payload;

        var identity = _claimFactory.CreateIdentity(user, user.Id);
        var jwtToken = _jwtGenerator.CreateToken(identity);

        var response = new LoginResponseDTO
        {
            Token = jwtToken,
            Id = user.Id,
            Username = user.Username,
            Email = user.Email
        };

        return Ok(Result<LoginResponseDTO>.Success(response));
    }

    /// <summary>
    /// 取得使用者個人資訊
    /// </summary>
    /// <remarks>
    /// 根據使用者 ID 取得使用者資料（僅回傳非敏感欄位）。
    /// </remarks>
    /// <param name="id">使用者 ID</param>
    /// <returns>使用者資訊</returns>
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Profile()
    {
        var userIdClaim = User.FindFirst("UserId");
        if (userIdClaim == null)
            return Unauthorized(new { message = "Invalid token: no user ID." });

        var userId = Guid.Parse(userIdClaim.Value);
        var result = await _userService.GetUserById(userId);
        if (!result.IsSuccess)
            return NotFound(result);

        User user = result.Payload;
        var response = new UserResponseDTO
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email
        };

        return Ok(response);
    }

    [Authorize]
    [HttpDelete]
    public async Task<IActionResult> DeleteUser(UserDeleteDTO dto) //TODO: ...
    {
        var userIdClaim = User.FindFirst("UserId");
        var isPermissionResult = await _userService.HasSufficientPermission(Guid.Parse(userIdClaim.Value), UserRole.ADMIN);
        if (!isPermissionResult.IsSuccess)
            return BadRequest(new { message = "Unknow issue" });

        var isPermission = isPermissionResult.Payload;
        if (!isPermission)
            return Unauthorized(new { message = "Invalid token: no user ID." });

        // var userId = Guid.Parse(userIdClaim.Value);
        var userId = dto.Id;
        var user = await _userService.GetUserById(userId);
        if (user == null)
            return NotFound(new { message = $"User with ID {userId} not found." });



        await _userService.DeleteUser(userId);

        return Ok(new { message = $"User with ID {userId} deleted successfully." });
    }
}

