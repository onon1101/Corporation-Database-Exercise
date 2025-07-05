using Microsoft.AspNetCore.Mvc;
using MyRestApi.Models;

namespace MyRestApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoController : ControllerBase
{
    private static readonly List<Todo> todos = new()
    {
        new Todo { Id = 1, Title = "Buy groceries", IsDone = false },
        new Todo { Id = 2, Title = "Walk the dog", IsDone = true }
    };

    [HttpGet]
    public ActionResult<IEnumerable<Todo>> GetTodos()
    {
        return Ok(todos);
    }
}