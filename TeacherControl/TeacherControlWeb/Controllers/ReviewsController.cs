using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeacherControlWeb.Data;
using TeacherControlWeb.Entities;

namespace TeacherControlWeb.Controllers;

[Authorize]
public class ReviewsController : Controller
{
    private readonly AppDbContext _context;

    public ReviewsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Guid teacherId, int rating, string content, bool isAnonymous = false)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Challenge();

        var review = new ReviewEntity
        {
            TeacherId = teacherId,
            UserId = userId,
            Rating = rating,
            Content = content,
            IsAnonymous = isAnonymous,
            CreatedAt = DateTime.UtcNow
        };

        _context.Reviews.Add(review);
        await _context.SaveChangesAsync();

        return RedirectToAction("Details", "Teachers", new { id = teacherId });
    }
}
