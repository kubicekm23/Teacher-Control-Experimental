using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeacherControlWeb.Data;
using TeacherControlWeb.Entities;

namespace TeacherControlWeb.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class MemesController : AdminBaseController
{
    private readonly AppDbContext _context;

    public MemesController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.Memes.OrderByDescending(m => m.CreatedAt).ToListAsync());
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(MemeEntity meme)
    {
        if (ModelState.IsValid)
        {
            _context.Add(meme);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(meme);
    }

    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null) return NotFound();

        var meme = await _context.Memes.FindAsync(id);
        if (meme == null) return NotFound();

        return View(meme);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, MemeEntity meme)
    {
        if (id != meme.Id) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(meme);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MemeExists(meme.Id)) return NotFound();
                else throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(meme);
    }

    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null) return NotFound();

        var meme = await _context.Memes.FindAsync(id);
        if (meme == null) return NotFound();

        _context.Memes.Remove(meme);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool MemeExists(Guid id)
    {
        return _context.Memes.Any(e => e.Id == id);
    }
}
