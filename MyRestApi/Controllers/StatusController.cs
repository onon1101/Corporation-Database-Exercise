using Microsoft.AspNetCore.Mvc;
using MyRestApi.Models;

namespace MyRestApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StatusController : ControllerBase
{
    [HttpGet]
    public ActionResult SayHello()
    {
        return Ok(new { message = "hello " });
    }
}