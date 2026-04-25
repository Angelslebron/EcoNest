using EcoNest.Application.DTOs.Maintenance;
using EcoNest.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EcoNest.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MaintenancesController(MaintenanceService maintenanceService) : ControllerBase
{
    private readonly MaintenanceService _maintenanceService = maintenanceService;

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _maintenanceService.GetAllAsync());

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
        => Ok(await _maintenanceService.GetByIdAsync(id));

    // GET api/maintenances/cabin/3
    [HttpGet("cabin/{cabinId:int}")]
    public async Task<IActionResult> GetByCabin(int cabinId)
        => Ok(await _maintenanceService.GetByCabinAsync(cabinId));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateMaintenanceRequest request)
    {
        var result = await _maintenanceService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateMaintenanceRequest request)
        => Ok(await _maintenanceService.UpdateAsync(id, request));

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _maintenanceService.DeleteAsync(id);
        return NoContent();
    }
}