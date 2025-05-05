using Microsoft.AspNetCore.Mvc;

namespace Pronia.Areas.Manage.Controllers;

[Area("Manage")]
public class DashboardController : Controller
{
    // GET
    
    public IActionResult Index()
    {
        return View();
    }
}