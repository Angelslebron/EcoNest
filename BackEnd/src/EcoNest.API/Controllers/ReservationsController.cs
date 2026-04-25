using EcoNest.Application.DTOs.Reservation;
using EcoNest.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EcoNest.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationsController(ReservationService reservationService) : ControllerBase
{
    private readonly ReservationService _reservationService = reservationService;

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _reservationService.GetAllAsync());

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
        => Ok(await _reservationService.GetByIdAsync(id));

    [HttpGet("active")]
    public async Task<IActionResult> GetActiveAndUpcoming()
        => Ok(await _reservationService.GetActiveAndUpcomingAsync());

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateReservationRequest request)
    {
        var result = await _reservationService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateReservationRequest request)
        => Ok(await _reservationService.UpdateAsync(id, request));

    // PATCH api/reservations/5/cancel
    [HttpPatch("{id:int}/cancel")]
    public async Task<IActionResult> Cancel(int id)
    {
        await _reservationService.CancelAsync(id);
        return NoContent();
    }
}
