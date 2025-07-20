using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyRestApi.DTO;
using MyRestApi.Models;
using MyRestApi.Services;

namespace MyRestApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class ScheduleController : ControllerBase
{
    private readonly IScheduleService _service;

    public ScheduleController(IScheduleService service)
    {
        _service = service;
    }

    /// <summary>
    /// 建立一筆電影場次
    /// </summary>
    /// <remarks>
    /// 管理員可以使用此 API 建立場次，包含電影、電影院與時間資訊。
    /// </remarks>
    /// <param name="dto">場次建立資料</param>
    /// <returns>新建立的場次 ID</returns>
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] CreateScheduleDTO dto)
    {
        var userIdClaim = User.FindFirst("UserId");
        if (userIdClaim == null)
            return Unauthorized(new { message = "Invalid token: no user ID." });

        var schedule = new Schedule
        {
            MovieId = dto.MovieId,
            TheaterId = dto.TheaterId,
            StartTime = dto.StartTime,
            EndTime = dto.EndTime
        };

        var id = await _service.CreateScheduleAsync(schedule);
        return Ok(new { id });
    }

    /// <summary>
    /// 取得所有電影場次
    /// </summary>
    /// <remarks>
    /// 回傳系統中所有已建立的電影場次資料。
    /// </remarks>
    /// <returns>場次清單</returns>
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> All()
    {
        var userIdClaim = User.FindFirst("UserId");
        if (userIdClaim == null)
            return Unauthorized(new { message = "Invalid token: no user ID." });

        var schedules = await _service.GetAllSchedulesAsync();
        return Ok(schedules);
    }

    /// <summary>
    /// 查詢某部電影的所有場次
    /// </summary>
    /// <remarks>
    /// 根據電影 ID，取得所有該部電影的場次。
    /// </remarks>
    /// <param name="movieId">電影 ID</param>
    /// <returns>該電影的場次列表</returns>
    [HttpGet("{movieId}")]
    [Authorize]
    public async Task<IActionResult> ByMovie(Guid movieId)
    {

        var userIdClaim = User.FindFirst("UserId");
        if (userIdClaim == null)
            return Unauthorized(new { message = "Invalid token: no user ID." });

        var schedules = await _service.GetSchedulesByMovieAsync(movieId);
        return Ok(schedules);
    }

    /// <summary>
    /// 刪除一筆電影場次
    /// </summary>
    /// <remarks>
    /// 根據場次 ID 刪除該筆資料。
    /// </remarks>
    /// <param name="id">場次 ID</param>
    /// <returns>刪除結果</returns>
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> Delete(Guid id)
    {

        var userIdClaim = User.FindFirst("UserId");
        if (userIdClaim == null)
            return Unauthorized(new { message = "Invalid token: no user ID." });

        var success = await _service.DeleteScheduleAsync(id);
        if (!success.IsSuccess)
            return NotFound(success);

        return Ok(new { message = "Schedule deleted" });
    }
}