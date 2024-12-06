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


    // update the item
    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateItem(int id, [FromBody] Item updatedItem)
    {
        Console.WriteLine($"Received request to update item with ID: {id}");
        Console.WriteLine($"Request body: Name = {updatedItem.Name}, Description = {updatedItem.Description}");

        // Find the item in the database using the ID from the route
        var existingItem = await _context.Items!.FindAsync(id);
        if (existingItem == null)
        {
            Console.WriteLine($"Item with ID {id} not found.");
            return NotFound(); // Return 404 if the item doesn't exist
        }

        // Update only the fields that need changing
        existingItem.Name = updatedItem.Name;
        existingItem.Description = updatedItem.Description;

        // Save changes to the database
        await _context.SaveChangesAsync();
        Console.WriteLine($"Item with ID {id} successfully updated.");

        // return NoContent(); // 204 No Content to indicate success without a response body
        return Ok(existingItem);
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
