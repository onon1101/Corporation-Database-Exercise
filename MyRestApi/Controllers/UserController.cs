using Microsoft.AspNetCore.Mvc;
using MyRestApi.Controllers.Request;
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

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
    {
        var user = new User
        {
            Username = request.Username,
            Email = request.Email,
            Password = request.Password
        };

        var id = await _userService.RegisterUserAsync(user);

        return Ok(new { message = $"User '{request.Username}' registered successfully.", Id = id });
    }

    [HttpPost]
    public async Task<UserResponse> GetUserByEmail([FromBody] Request.EmailRequest req)
    {
        var response = new UserResponse();
        response.Data = await _userService.GetUserByEmail(req.email);

        // not found user
        if (null == response.Data)
        {
            response.StatusCode = 404;
            response.Data = "Not Found via user's email";
        }
        return response;
    }

    [HttpPost]
    public async Task<UserResponse> DeleteUser([FromBody] Request.DeleteUser req)
    {
        var response = new UserResponse();
        await _userService.DeleteUser(req.userid);

        response.Data = "Delete user successfully.";
        return response;
    }

    [HttpPost]
    public async Task<BaseResponse> GetUserById([FromBody] UserId req)
    {
        var response = new UserResponse();
        response.Data = await _userService.GetUserById(req.userid);

        response.StatusCode = 200;
        return response;
    }

    [HttpPost]
    public async Task<BaseResponse> ModifyUser([FromBody] User user)
    {
        var response = new UserResponse();
        await _userService.PatchUser(user);

        response.Data = "modify successful";
        return response;
    }
}