using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeacherControlWeb.Data;

namespace TeacherControlWeb.Areas.Admin.Controllers;

public class ModerationController : AdminBaseController
{
    private readonly AppDbContext _context;

    public ModerationController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Reviews()
    {
        var reviews = await _context.Reviews
            .Include(r => r.Teacher)
            .Include(r => r.User)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();
        return View(reviews);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteReview(Guid id)
    {
        var review = await _context.Reviews.FindAsync(id);
        if (review != null)
        {
            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Recenze byla smazána.";
        }
        return RedirectToAction(nameof(Reviews));
    }

    public async Task<IActionResult> Chat()
    {
        var messages = await _context.ChatMessages
            .Include(m => m.User)
            .OrderByDescending(m => m.CreatedAt)
            .Take(200)
            .ToListAsync();
        return View(messages);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteChatMessage(Guid id)
    {
        var message = await _context.ChatMessages.FindAsync(id);
        if (message != null)
        {
            _context.ChatMessages.Remove(message);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Zpráva byla smazána.";
        }
        return RedirectToAction(nameof(Chat));
    }
}
