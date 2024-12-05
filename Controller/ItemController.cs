using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DotNet9CookieAuthAPI.Data; // Replace with your actual namespace for AppDbContext
using DotNet9CookieAuthAPI.Models;

[Route("api/items")]
[ApiController]
public class ItemController : ControllerBase
{
    private readonly AppDbContext _context;

    public ItemController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetItems()
    {
        var items = await _context.Items!.ToListAsync();
        return Ok(items);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetItem(int id)
    {
        var item = await _context.Items!.FindAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateItem([FromBody] Item item)
    {
        _context.Items!.Add(item);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetItem), new { id = item.Id }, item);
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateItem(int id, [FromBody] Item item)
    {
        if (id != item.Id)
            return BadRequest();

        _context.Entry(item).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteItem(int id)
    {
        var item = await _context.Items!.FindAsync(id);
        if (item == null)
            return NotFound();

        _context.Items.Remove(item);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
