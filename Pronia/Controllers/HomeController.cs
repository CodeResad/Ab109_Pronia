using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia.DAL;
using Pronia.Models;

namespace Pronia.Controllers;

public class HomeController : Controller
{
    AppDbContext _context;

    public HomeController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        List<Product> products = await _context.Products
            .Include(p => p.Images)
            .ToListAsync();
        
        List<Slider> sliders = await _context.Sliders.ToListAsync();
        
        ViewData["Sliders"] = sliders;
        
        return View(products);
    }

    public async Task<IActionResult> Detail(int? id)
    {
        if (id == null)
        {
            return BadRequest();
        }
        
        Product product = await _context.Products
            .Include(x=>x.Images)
            .Include(x=>x.Category)
            .FirstOrDefaultAsync(x => x.Id == id);

        List<Product> relatedProducts = await _context.Products
            .Include(x => x.Images)
            .Where(x=>x.CategoryId == product.CategoryId && x.Id != id)
            .ToListAsync();
        
        ViewData["relatedProducts"] = relatedProducts;
        
        return View(product);
    }
}