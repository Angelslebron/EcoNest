using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EcoNest.Persistence;
using EcoNest.Domain.Entities;
using System.Threading.Tasks;

namespace EcoNest.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CabinsController(EcoNestDbContext context) : ControllerBase
{
    private readonly EcoNestDbContext _context = context;

    [HttpGet]
    public async Task<IActionResult> GetCabins()
    {
        var cabin = await _context.Cabins.ToListAsync();

        if (cabin == null || cabin.Count == 0)
            return NotFound("No cabin found.");

        return Ok(cabin);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCabin(int id)
    {
        var cabin = await _context.Cabins.FindAsync(id);

        if (cabin == null)
            return NotFound();

        return Ok(cabin);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCabin(Cabin cabin)
    {
        _context.Cabins.Add(cabin);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCabin), new { id = cabin.Id }, cabin);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCabin(int id)
    {
        var cabin = await _context.Cabins.FindAsync(id);

        if (cabin == null)
            return NotFound();

        _context.Cabins.Remove(cabin);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}