using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia.DAL;
using Pronia.Models;

namespace Pronia.Areas.Manage.Controllers;

[Area("Manage")]
public class CategoryController : Controller
{
    AppDbContext _context;

    public CategoryController(AppDbContext context)
    {
        _context = context;
    }


    public async Task<IActionResult> Index()
    {
        List<Category> categories = await _context.Categories
            .Include(x => x.Products)
            .ToListAsync();
        return View(categories);
    }

    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(Category category)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        
        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();
        
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(int id)
    {
        var category = _context.Categories.FirstOrDefault(x => x.Id == id);
        if (category == null)
        {
            return BadRequest();
        }
        
        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
        
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Update(int id)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
        if (category == null)
        {
            return BadRequest();
        }
        return View(category);
    }

    [HttpPost]
    public async Task<IActionResult> Update(int id, Category category)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        _context.Update(category);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }
}