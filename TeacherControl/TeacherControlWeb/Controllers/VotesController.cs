using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeacherControlWeb.Data;
using TeacherControlWeb.Entities;

namespace TeacherControlWeb.Controllers;

[Authorize]
public class VotesController : Controller
{
    private readonly AppDbContext _context;

    public VotesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Guid teacherId, string category)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Challenge();

        // Optional: Check if user already voted for this teacher in this category
        var existingVote = _context.Votes.Any(v => v.TeacherId == teacherId && v.UserId == userId && v.Category == category);
        if (existingVote)
        {
            TempData["ErrorMessage"] = "V této kategorii jste již hlasovali.";
            return RedirectToAction("Details", "Teachers", new { id = teacherId });
        }

        var vote = new VoteEntity
        {
            TeacherId = teacherId,
            UserId = userId,
            Category = category,
            CreatedAt = DateTime.UtcNow
        };

        _context.Votes.Add(vote);
        await _context.SaveChangesAsync();

        return RedirectToAction("Details", "Teachers", new { id = teacherId });
    }
}
