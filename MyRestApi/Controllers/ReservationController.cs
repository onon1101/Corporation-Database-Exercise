using Microsoft.AspNetCore.Mvc;
using MyRestApi.DTO;
using MyRestApi.Models;
using MyRestApi.Services;

namespace MyRestApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class ReservationController : ControllerBase
{
    private readonly IReservationService _service;

    public ReservationController(IReservationService service)
    {
        _service = service;
    }

    /// <summary>
    /// 建立一筆新的訂位紀錄
    /// </summary>
    /// <remarks>
    /// 使用者可透過本 API 預約特定場次與座位。傳入使用者 ID、場次 ID 與座位清單。
    /// </remarks>
    /// <param name="dto">包含使用者、場次與座位的資料</param>
    /// <returns>成功建立的訂位 ID</returns>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateReservationDTO dto)
    {
        var reservation = new Reservation
        {
            UserId = dto.UserId,
            ScheduleId = dto.ScheduleId
        };

        var id = await _service.CreateReservationAsync(reservation, dto.SeatIds);
        return Ok(new { id });
    }

    /// <summary>
    /// 查詢使用者的所有訂位
    /// </summary>
    /// <remarks>
    /// 根據使用者 ID，查詢他預約過的所有場次資料。
    /// </remarks>
    /// <param name="userId">使用者的唯一識別碼</param>
    /// <returns>該使用者的所有訂位清單</returns>
    [HttpGet("{userId}")]
    public async Task<IActionResult> ByUser(Guid userId)
    {
        var reservations = await _service.GetReservationsByUserAsync(userId);
        return Ok(reservations);
    }

    /// <summary>
    /// 刪除一筆訂位紀錄
    /// </summary>
    /// <remarks>
    /// 根據訂位 ID 刪除該筆訂位資料，同時清除其座位關聯紀錄。
    /// </remarks>
    /// <param name="id">訂位 ID</param>
    /// <returns>刪除成功與否</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await _service.DeleteReservationAsync(id);
        if (!success)
            return NotFound(new { message = "Reservation not found" });

        return Ok(new { message = "Reservation deleted" });
    }
}