using MyRestApi.DTO;
using Microsoft.AspNetCore.Mvc;
using MyRestApi.Models;
using MyRestApi.Services;

namespace MyRestApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
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
        var user = new User
        {
            Username = dto.Username,
            Email = dto.Email,
            Password = dto.Password
        };

        var id = await _userService.RegisterUserAsync(user);

        var response = new UserResponseDTO
        {
            Id = id,
            Username = user.Username,
            Email = user.Email
        };

        return Ok(response);
    }

    /// <summary>
    /// 使用者登入
    /// </summary>
    /// <remarks>
    /// 驗證 Email 與密碼是否正確，登入成功後回傳使用者資訊。
    /// </remarks>
    /// <param name="dto">登入資訊</param>
    /// <returns>使用者資訊或 Unauthorized</returns>
    [HttpPost]
    public async Task<IActionResult> Login([FromBody] UserLoginDTO dto)
    {
        var user = await _userService.AuthenticateUserAsync(dto.Email, dto.Password);
        if (user == null)
        {
            return Unauthorized(new { message = "Invalid email or password" });
        }

        var response = new UserResponseDTO
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email
        };

        return Ok(response);
    }

    /// <summary>
    /// 取得使用者個人資訊
    /// </summary>
    /// <remarks>
    /// 根據使用者 ID 取得使用者資料（僅回傳非敏感欄位）。
    /// </remarks>
    /// <param name="id">使用者 ID</param>
    /// <returns>使用者資訊</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> Profile(Guid id)
    {
        var user = await _userService.GetUserById(id);
        if (user == null)
        {
            return NotFound(new { message = "User not found" });
        }

        var response = new UserResponseDTO
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email
        };

        return Ok(response);
    }
}

