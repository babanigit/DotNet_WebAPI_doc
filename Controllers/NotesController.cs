using DotNet_WebAPI_doc.Data;
using DotNet_WebAPI_doc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[Route("api/notes")]
[ApiController]
public class NotesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public NotesController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetNotes()
    {
        var userId = int.Parse(User.Claims.First(c => c.Type == "UserId").Value);
        var notes = _context.Notes.Where(n => n.UserId == userId).ToList();
        return Ok(notes);
    }

    [HttpPost]
    public IActionResult CreateNote([FromBody] Note note)
    {
        var userId = int.Parse(User.Claims.First(c => c.Type == "UserId").Value);
        note.UserId = userId;
        _context.Notes.Add(note);
        _context.SaveChanges();
        return Ok(note);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateNote(int id, [FromBody] Note updatedNote)
    {
        var userId = int.Parse(User.Claims.First(c => c.Type == "UserId").Value);
        var note = _context.Notes.SingleOrDefault(n => n.Id == id && n.UserId == userId);
        if (note == null)
        {
            return NotFound("Note not found.");
        }

        note.Title = updatedNote.Title;
        note.Content = updatedNote.Content;
        _context.SaveChanges();
        return Ok(note);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteNote(int id)
    {
        var userId = int.Parse(User.Claims.First(c => c.Type == "UserId").Value);
        var note = _context.Notes.SingleOrDefault(n => n.Id == id && n.UserId == userId);
        if (note == null)
        {
            return NotFound("Note not found.");
        }

        _context.Notes.Remove(note);
        _context.SaveChanges();
        return Ok(new { message = "Note deleted successfully." });
    }
}
