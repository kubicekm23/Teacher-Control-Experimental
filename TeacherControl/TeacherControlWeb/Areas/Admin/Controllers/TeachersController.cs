using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeacherControlWeb.Data;
using TeacherControlWeb.Entities;

namespace TeacherControlWeb.Areas.Admin.Controllers;

public class TeachersController : AdminBaseController
{
    private readonly AppDbContext _context;

    public TeachersController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.Teachers.ToListAsync());
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(TeacherEntity teacher)
    {
        if (ModelState.IsValid)
        {
            teacher.Id = Guid.NewGuid();
            _context.Add(teacher);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Učitel byl úspěšně přidán.";
            return RedirectToAction(nameof(Index));
        }
        return View(teacher);
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        var teacher = await _context.Teachers.FindAsync(id);
        if (teacher == null) return NotFound();
        return View(teacher);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, TeacherEntity teacher)
    {
        if (id != teacher.Id) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(teacher);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Změny byly uloženy.";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Teachers.Any(e => e.Id == teacher.Id)) return NotFound();
                else throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(teacher);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(Guid id)
    {
        var teacher = await _context.Teachers.FindAsync(id);
        if (teacher != null)
        {
            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Učitel byl smazán.";
        }
        return RedirectToAction(nameof(Index));
    }
}
