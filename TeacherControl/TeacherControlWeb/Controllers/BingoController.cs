using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeacherControlWeb.Services;

namespace TeacherControlWeb.Controllers;

[Authorize]
public class BingoController : Controller
{
    private readonly IBingoService _bingoService;

    public BingoController(IBingoService bingoService)
    {
        _bingoService = bingoService;
    }

    public async Task<IActionResult> Index()
    {
        var board = await _bingoService.GetCurrentBoardAsync();
        return View(board);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Trigger(Guid tileId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Challenge();

        await _bingoService.TriggerTileAsync(tileId, userId);
        return RedirectToAction(nameof(Index));
    }
}
