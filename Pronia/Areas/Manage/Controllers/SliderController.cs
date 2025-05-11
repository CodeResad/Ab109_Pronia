using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia.DAL;
using Pronia.Models;
using Pronia.Utils.Extentions;

namespace Pronia.Areas.Manage.Controllers;

[Area("Manage")]
[Authorize(Roles = "Admin")]
public class SliderController : Controller
{
    AppDbContext _context;
    IWebHostEnvironment _environment;

    public SliderController(AppDbContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }

    public async Task<IActionResult> Index()
    {
        List<Slider> sliderList = await _context.Sliders.ToListAsync();
        return View(sliderList);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Slider slider)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        if (!slider.File.ContentType.Contains("image"))
        {
            ModelState.AddModelError("File", "Duzgun format daxil edin");
            return View();
        }

        if (slider.File.Length > 2097152)
        {
            ModelState.AddModelError("File", "File is too long");
            return View();
        }

        slider.SliderImgUrl = slider.File.CreateFile(_environment.WebRootPath, "Upload/Slider");
        
        
        
        await _context.Sliders.AddAsync(slider);
        await _context.SaveChangesAsync();
        
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(int id)
    {
        var slider = await _context.Sliders.FirstOrDefaultAsync(x => x.Id == id);

        if (slider == null)
        {
            return View("Error");
        }
        
        slider.SliderImgUrl.RemoveFile(_environment.WebRootPath, "Upload/Slider");
        
        _context.Sliders.Remove(slider);
        await _context.SaveChangesAsync();
        
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Update(int id)
    {
        var slider = await _context.Sliders.FirstOrDefaultAsync(x => x.Id == id);
        if (slider == null)
        {
            return View("Error");
        }
        
        return View(slider);
    }

    [HttpPost]
    public async Task<IActionResult> Update(int id, Slider slider)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        
        if (!slider.File.ContentType.Contains("image"))
        {
            ModelState.AddModelError("File", "Duzgun format daxil edin");
            return View();
        }

        if (slider.File.Length > 2097152)
        {
            ModelState.AddModelError("File", "File is too long");
            return View();
        }
        
        slider.SliderImgUrl = slider.File.CreateFile(_environment.WebRootPath, "Upload/Slider");
        
        _context.Update(slider);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }
}