using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyRestApi.DTO;
using MyRestApi.Services;

namespace MyRestApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class MovieController : ControllerBase
{
    private readonly IMovieService _service;

    public MovieController(IMovieService service)
    {
        _service = service;
    }

    /// <summary>
    /// 建立一部新電影
    /// </summary>
    /// <remarks>
    /// 管理員可以使用此 API 新增電影資料，例如名稱、簡介、片長等。
    /// </remarks>
    /// <param name="dto">電影建立資料</param>
    /// <returns>新電影的 ID</returns>
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] CreateMovieDTO dto)
    {
        var userIdClaim = User.FindFirst("UserId");
        if (userIdClaim == null)
            return Unauthorized(new { message = "Invalid token: no user ID." });

        var id = await _service.CreateMovieAsync(dto);
        return Ok(new { id });
    }

    /// <summary>
    /// 取得所有電影列表
    /// </summary>
    /// <remarks>
    /// 回傳資料庫中所有電影的完整清單。
    /// </remarks>
    /// <returns>電影清單</returns>
    [HttpGet]
    public async Task<IActionResult> All()
    {
        var movies = await _service.GetAllMoviesAsync();
        return Ok(movies);
    }

    /// <summary>
    /// 查詢單一電影
    /// </summary>
    /// <remarks>
    /// 根據電影 ID 查詢該筆詳細資料。
    /// </remarks>
    /// <param name="id">電影 ID</param>
    /// <returns>電影資料</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var movie = await _service.GetMovieByIdAsync(id);
        if (movie == null)
            return NotFound(new { message = "Movie not found" });
        return Ok(movie);
    }

    /// <summary>
    /// 更新電影資料
    /// </summary>
    /// <remarks>
    /// 根據 ID 修改電影的標題、簡介、時長等欄位。
    /// </remarks>
    /// <param name="id">電影 ID</param>
    /// <param name="dto">更新資料</param>
    /// <returns>更新結果</returns>
    [HttpPatch("{id}")]
    [Authorize]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateMovieDTO dto)
    {
        var userIdClaim = User.FindFirst("UserId");
        if (userIdClaim == null)
            return Unauthorized(new { message = "Invalid token: no user ID." });

        var movie = new Movie
        {
            Title = dto.Title,
            Description = dto.Description,
            Duration = dto.Duration ?? 0,
            Rating = dto.Rating,
            PosterUrl = dto.PosterUrl
        };

        var success = await _service.UpdateMovieAsync(id, movie);
        if (!success.IsSuccess)
            return NotFound(success);

        return Ok(new { message = "Movie updated" });
    }

    /// <summary>
    /// 刪除電影
    /// </summary>
    /// <remarks>
    /// 根據 ID 移除電影紀錄。
    /// </remarks>
    /// <param name="id">電影 ID</param>
    /// <returns>刪除結果</returns>
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> Delete(Guid id)
    {
        var userIdClaim = User.FindFirst("UserId");
        if (userIdClaim == null)
            return Unauthorized(new { message = "Invalid token: no user ID." });

        var success = await _service.DeleteMovieAsync(id);
        if (!success.IsSuccess)
            return NotFound(success);

        return Ok(new { message = "Movie deleted" });
    }
}