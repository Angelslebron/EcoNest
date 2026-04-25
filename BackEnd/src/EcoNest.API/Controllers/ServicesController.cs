using EcoNest.Application.DTOs.Service;
using EcoNest.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EcoNest.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServicesController(ServiceService serviceService) : ControllerBase
{
    private readonly ServiceService _serviceService = serviceService;

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _serviceService.GetAllAsync());

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
        => Ok(await _serviceService.GetByIdAsync(id));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateServiceRequest request)
    {
        var result = await _serviceService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateServiceRequest request)
        => Ok(await _serviceService.UpdateAsync(id, request));

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _serviceService.DeleteAsync(id);
        return NoContent();
    }
}
