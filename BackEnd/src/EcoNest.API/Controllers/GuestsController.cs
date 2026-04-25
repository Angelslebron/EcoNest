using EcoNest.Application.DTOs.Guest;
using EcoNest.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EcoNest.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GuestsController(GuestService guestService) : ControllerBase
{
    private readonly GuestService _guestService = guestService;

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _guestService.GetAllAsync());

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
        => Ok(await _guestService.GetByIdAsync(id));

    // GET api/guests/search?term=Juan
    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string term)
        => Ok(await _guestService.SearchAsync(term));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateGuestRequest request)
    {
        var result = await _guestService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateGuestRequest request)
        => Ok(await _guestService.UpdateAsync(id, request));

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _guestService.DeleteAsync(id);
        return NoContent();
    }
}