using EcoNest.Application.DTOs.Season;
using EcoNest.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EcoNest.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SeasonsController(SeasonService seasonService) : ControllerBase
{
    private readonly SeasonService _seasonService = seasonService;

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _seasonService.GetAllAsync());

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
        => Ok(await _seasonService.GetByIdAsync(id));

    // GET api/seasons/active?date=2025-12-15
    [HttpGet("active")]
    public async Task<IActionResult> GetActive([FromQuery] DateTime date)
    {
        var result = await _seasonService.GetActiveAsync(date);
        return result is null ? NotFound("No active season for the given date.") : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSeasonRequest request)
    {
        var result = await _seasonService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateSeasonRequest request)
        => Ok(await _seasonService.UpdateAsync(id, request));

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _seasonService.DeleteAsync(id);
        return NoContent();
    }
}
