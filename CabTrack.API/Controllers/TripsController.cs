using CabTrack.API.DTOs.Trips;
using CabTrack.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CabTrack.API.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/trips")]
[ApiVersion("1.0")]
[Authorize]
public class TripsController : ControllerBase
{
    private readonly ITripService _tripService;

    public TripsController(ITripService tripService)
    {
        _tripService = tripService;
    }

    private int UserId => int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TripCreateDto dto)
    {
        var created = await _tripService.CreateTripAsync(UserId, dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() }, created);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var trips = await _tripService.GetTripsAsync(UserId);
        return Ok(trips);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var trip = await _tripService.GetTripByIdAsync(UserId, id);
        return Ok(trip);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] TripUpdateDto dto)
    {
        await _tripService.UpdateTripAsync(UserId, id, dto);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _tripService.DeleteTripAsync(UserId, id);
        return NoContent();
    }
}