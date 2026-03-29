using CabTrack.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CabTrack.API.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/reports")]
[ApiVersion("1.0")]
[Authorize]
public class ReportsController : ControllerBase
{
    private readonly ITripService _tripService;

    public ReportsController(ITripService tripService)
    {
        _tripService = tripService;
    }

    private int UserId => int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);

    [HttpGet("daily")]
    public async Task<IActionResult> Daily()
    {
        var today = DateTime.UtcNow.Date;
        var totalFare = await _tripService.GetReportAsync(UserId, today, today.AddDays(1).AddTicks(-1));
        return Ok(new { totalFare });
    }

    [HttpGet("weekly")]
    public async Task<IActionResult> Weekly()
    {
        var today = DateTime.UtcNow.Date;
        var start = today.AddDays(-(int)today.DayOfWeek);
        var totalFare = await _tripService.GetReportAsync(UserId, start, start.AddDays(7).AddTicks(-1));
        return Ok(new { totalFare });
    }

    [HttpGet("monthly")]
    public async Task<IActionResult> Monthly()
    {
        var today = DateTime.UtcNow.Date;
        var start = new DateTime(today.Year, today.Month, 1);
        var end = start.AddMonths(1).AddTicks(-1);
        var totalFare = await _tripService.GetReportAsync(UserId, start, end);
        return Ok(new { totalFare });
    }

    [HttpGet("yearly")]
    public async Task<IActionResult> Yearly()
    {
        var today = DateTime.UtcNow.Date;
        var start = new DateTime(today.Year, 1, 1);
        var end = start.AddYears(1).AddTicks(-1);
        var totalFare = await _tripService.GetReportAsync(UserId, start, end);
        return Ok(new { totalFare });
    }
}