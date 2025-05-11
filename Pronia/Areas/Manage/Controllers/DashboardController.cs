using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Pronia.Areas.Manage.Controllers;

[Area("Manage")]
[Authorize(Roles = "Admin")]
public class DashboardController : Controller
{
    // GET
    
    public IActionResult Index()
    {
        return View();
    }
}