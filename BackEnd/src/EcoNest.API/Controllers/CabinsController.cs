using EcoNest.Application.DTOs.Cabin;
using EcoNest.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EcoNest.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CabinsController(CabinService cabinService) : ControllerBase
{
    private readonly CabinService _cabinService = cabinService;

    // GET api/cabins
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _cabinService.GetAllAsync();
        return Ok(result);
    }

    // GET api/cabins/5
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _cabinService.GetByIdAsync(id);
        return Ok(result);
    }

    // GET api/cabins/available?checkIn=2025-12-01&checkOut=2025-12-05
    [HttpGet("available")]
    public async Task<IActionResult> GetAvailable([FromQuery] DateTime checkIn, [FromQuery] DateTime checkOut)
    {
        var result = await _cabinService.GetAvailableAsync(checkIn, checkOut);
        return Ok(result);
    }

    // POST api/cabins
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCabinRequest request)
    {
        var result = await _cabinService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    // PUT api/cabins/5
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateCabinRequest request)
    {
        var result = await _cabinService.UpdateAsync(id, request);
        return Ok(result);
    }

    // DELETE api/cabins/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _cabinService.DeleteAsync(id);
        return NoContent();
    }
}