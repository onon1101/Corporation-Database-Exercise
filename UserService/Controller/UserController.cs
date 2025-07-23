using Api.Utils;
using Api.DTO;
using Api.Services.Interface;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace UserService.Controller;

[ApiController]
[Route(APIRoutes.User)]
public class UserController(IUserService userService, IConfiguration configuration): ControllerBase
{
    private readonly IConfiguration _config = configuration;
    private readonly IUserService _service = userService;

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] UserRegisterRequestDTO dto)
    {
        var registerUser = await _service.RegisterUser(dto);
        return Ok(registerUser.Payload!);
    }
}