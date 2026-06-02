using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeacherControlWeb.Data;
using TeacherControlWeb.Entities;

namespace TeacherControlWeb.Controllers;

[Authorize]
public class LatenessController : Controller
{
    private readonly AppDbContext _context;

    public LatenessController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Guid teacherId, int minutes)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Challenge();

        var lateness = new LatenessEntity
        {
            TeacherId = teacherId,
            UserId = userId,
            Minutes = minutes,
            CreatedAt = DateTime.UtcNow
        };

        _context.Latenesses.Add(lateness);
        await _context.SaveChangesAsync();

        return RedirectToAction("Details", "Teachers", new { id = teacherId });
    }
}
