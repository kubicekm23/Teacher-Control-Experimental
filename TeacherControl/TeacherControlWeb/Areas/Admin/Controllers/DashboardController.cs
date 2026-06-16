using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeacherControlWeb.Data;

namespace TeacherControlWeb.Areas.Admin.Controllers;

public class DashboardController : AdminBaseController
{
    private readonly AppDbContext _context;

    public DashboardController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        ViewBag.UserCount = await _context.Users.CountAsync();
        ViewBag.TeacherCount = await _context.Teachers.CountAsync();
        ViewBag.MemeCount = await _context.Memes.CountAsync();
        return View();
    }
}
