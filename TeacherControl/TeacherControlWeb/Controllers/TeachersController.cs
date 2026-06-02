using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeacherControlWeb.Data;

namespace TeacherControlWeb.Controllers;

public class TeachersController : Controller
{
    private readonly AppDbContext _context;

    public TeachersController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var teachers = await _context.Teachers
            .Include(t => t.Reviews)
            .ToListAsync();
        return View(teachers);
    }

    public async Task<IActionResult> Details(Guid id)
    {
        var teacher = await _context.Teachers
            .Include(t => t.Reviews)
            .Include(t => t.Latenesses)
            .Include(t => t.Votes)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (teacher == null)
        {
            return NotFound();
        }

        return View(teacher);
    }
}
