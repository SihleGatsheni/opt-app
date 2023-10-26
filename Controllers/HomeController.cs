using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OptiApp.Models;

namespace OptiApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        var username = HttpContext.User.Identity!.Name;
        ViewBag.UserName = username!;
        return View();
    }
    
    [HttpGet("exam")]
    public IActionResult ExamService()
    {
        return View();
    }
    [HttpGet("single")]
    public IActionResult SingleVisionService()
    {
        return View();
    }
    
    [HttpGet("bifocal")]
    public IActionResult BifocalsService()
    {
        return View();
    }

    [HttpGet("multifocal")]
    public IActionResult MultiFocalService()
    {
        return View();
    }
    [HttpGet("consult")]
    public IActionResult ConsulationService()
    {
        return View();
    }
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}