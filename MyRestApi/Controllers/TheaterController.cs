using Microsoft.AspNetCore.Mvc;
using MyRestApi.DTO;
using MyRestApi.Models;
using MyRestApi.Repositories;
using MyRestApi.Services;

namespace MyRestApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class TheaterController : ControllerBase
{
    private readonly ITheaterService _service;
    private readonly ISeatService _seatService;

    public TheaterController(ITheaterService service, ISeatService seatService)
    {
        _service = service;
        _seatService = seatService;
    }

    /// <summary>
    /// 創建電影院
    /// </summary>
    /// <remarks>
    /// 創建新電影院資訊。輸入電影院名稱、位置與總容納座位大小。
    /// </remarks>
    /// <param name="dto">創建電影院資訊</param>
    /// <returns>回傳戲院 ID</returns>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTheaterDTO dto)
    {
        var theater = new Theater
        {
            Name = dto.Name,
            Location = dto.Location,
            TotalSeats = dto.TotalSeats
        };
        var result = await _service.CreateTheaterAsync(theater);
        if (!result.IsSuccess)
        {
            return BadRequest(result);
        }

        Guid test = result.Ok;
        Console.WriteLine($"tttttttttttttttttt: {result.Ok}");
        return Ok(new { id = result.Ok });
        // return Ok(new { Id = test });
    }

    /// <summary>
    /// 刪除戲院
    /// </summary>
    /// <remarks>
    /// 刪除電影院
    /// </remarks>
    /// <param name="id">電影院 ID</param>
    /// <returns>回傳訊息是否刪除成功</returns>
    [HttpPost("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _service.DeleteTheaterAsync(id);
        if (!result)
            return NotFound(new { message = "Theater not found" });
        return Ok(new { message = "Theater deleted" });
    }

    /// <summary>
    /// 取得所有電影院的資訊
    /// </summary>
    /// <remarks>
    /// 所有電影院的資訊
    /// </remarks>
    /// <returns>回傳電影院的陣列</returns>
    [HttpGet]
    public async Task<IActionResult> All()
    {
        var theaters = await _service.GetAllTheatersAsync();
        return Ok(theaters);
    }

    /// <summary>
    /// 使用 ID 取得該電影院的資訊
    /// </summary>
    /// <remarks>
    /// 根據電影院 ID 取得該電影院的資料
    /// </remarks>
    /// <param name="id">電影院 ID</param>
    /// <returns>電影院資訊</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var theater = await _service.GetTheaterByIdAsync(id);
        if (theater == null)
            return NotFound(new { message = "Theater not found" });
        return Ok(theater);
    }

    /// <summary>
    /// 更新電影院的資訊
    /// </summary>
    /// <remarks>
    /// 根據傳入的電影院資訊，修改資料庫中的既有的電影院資訊
    /// </remarks>
    /// <param name="id">電影院 ID</param>
    /// <param name="dto">修改電影院之相關資訓</param>
    /// <returns>是否修改成功</returns>
    [HttpPatch("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTheaterDTO dto)
    {
        var data = new Theater
        {
            Name = dto.Name,
            Location = dto.Location,
            TotalSeats = dto.TotalSeats ?? 0
        };

        var success = await _service.UpdateTheaterAsync(id, data);
        if (!success)
            return NotFound(new { message = "Theater not found" });

        return Ok(new { message = "Theater updated" });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSeatAll(Guid id)
    {
        var seats = await _seatService.GetSeatsByTheaterAsync(id);

        return Ok(seats);
    }
}