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
            .OrderByDescending(m => m.CreatedAt)
            .Take(100)
            .ToListAsync();
        
        return View(messages.OrderBy(m => m.CreatedAt).ToList());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Send(string content, ChatMessageType type = ChatMessageType.Text)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Challenge();

        var message = new ChatMessageEntity
        {
            UserId = userId,
            Content = content,
            Type = type,
            CreatedAt = DateTime.UtcNow,
            IsApproved = type == ChatMessageType.Text // Auto-approve text, memes might need moderation
        };

        _context.ChatMessages.Add(message);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
}
