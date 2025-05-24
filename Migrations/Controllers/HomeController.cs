using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using First_MVC_webApp.Models;

namespace First_MVC_webApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        ViewData["BackgroundImage"] = "/Images/home_background.jpg";
        return View();
    }

    public IActionResult PlayList()
    {
        
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
