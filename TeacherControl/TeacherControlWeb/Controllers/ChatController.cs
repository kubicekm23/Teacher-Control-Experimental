using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeacherControlWeb.Data;
using TeacherControlWeb.Entities;

namespace TeacherControlWeb.Controllers;

[Authorize]
public class ChatController : Controller
{
    private readonly AppDbContext _context;

    public ChatController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var messages = await _context.ChatMessages
            .Include(m => m.User)
            .Include(m => m.Meme)
            .OrderByDescending(m => m.CreatedAt)
            .Take(100)
            .ToListAsync();
        
        ViewBag.Memes = await _context.Memes.ToListAsync();
        return View(messages.OrderBy(m => m.CreatedAt).ToList());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Send(string content, Guid? memeId, ChatMessageType type = ChatMessageType.Text)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Challenge();

        var message = new ChatMessageEntity
        {
            UserId = userId,
            Content = content ?? (memeId.HasValue ? "Meme" : ""),
            Type = memeId.HasValue ? ChatMessageType.Meme : type,
            MemeId = memeId,
            CreatedAt = DateTime.UtcNow,
            IsApproved = true
        };

        _context.ChatMessages.Add(message);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
}
