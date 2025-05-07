using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia.Areas.Manage.ViewModels.Product;
using Pronia.DAL;
using Pronia.Models;

namespace Pronia.Areas.Manage.Controllers;

[Area("Manage")]
public class ProductController : Controller
{
    AppDbContext _context;

    public ProductController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var products = _context.Products
            .Include(x=>x.Category)
            .Include(x=>x.TagProducts)!
            .ThenInclude(x=>x.Tag)
            .ToList();
        return View(products);
    }

    public IActionResult Create()
    {
        ViewBag.Categories = _context.Categories.ToList();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductVm productVm)
    {
        ViewBag.Categories = _context.Categories.ToList();
        
        if (!ModelState.IsValid)
        {
            return View();
        }

        if (productVm.CategoryId != null)
        {
            if (!await _context.Categories.AnyAsync(x => x.Id == productVm.CategoryId))
            {
                ModelState.AddModelError("CategoryId", "Bele bir Category movcud deyil");
                return View();
            }
        }

        Product product = new Product()
        {
            Name = productVm.Name,
            Description = productVm.Description,
            Price = productVm.Price,
            CategoryId = productVm.CategoryId,
        };
        
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
        
        return RedirectToAction("Index");
    }

    public IActionResult Update(int? id)
    {
        ViewBag.Categories = _context.Categories.ToList();
        if (id == null)
        {
            return View("Error");
        }
        
        var product = _context.Products
            .Include(x=>x.Category)
            .Include(x=>x.TagProducts)!
            .ThenInclude(x=>x.Tag)
            .FirstOrDefault(x => x.Id == id);

        if (product == null)
        {
            return NotFound();
        }

        UpdateProductVm updateProductVm = new UpdateProductVm()
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            CategoryId = product.CategoryId,
        };
        
       return View(updateProductVm); 
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateProductVm updateProductVm)
    {
        ViewBag.Categories = _context.Categories.ToList();
        if (!ModelState.IsValid)
        {
            return View();
        }
        
        var oldProduct = await _context.Products.FirstOrDefaultAsync(x => x.Id == updateProductVm.Id);
        if (oldProduct == null)
        {
            return NotFound();
        }
        
        oldProduct.Name = updateProductVm.Name;
        oldProduct.Description = updateProductVm.Description;
        oldProduct.Price = updateProductVm.Price;
        oldProduct.CategoryId = updateProductVm.CategoryId;
        
        _context.Products.Update(oldProduct);
        await _context.SaveChangesAsync();
        
        return RedirectToAction("Index");
    }
}