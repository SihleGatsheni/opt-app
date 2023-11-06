using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OptiApp.Models;
using OptiApp.Service;

namespace OptiApp.Controllers;

[Authorize]
public class OptController : Controller
{
    private readonly OptometristService _service;
    public OptController(ApplicationDbContext context)
    {
        _service = new OptometristService(context);
    }
    // GET
    public async Task<IActionResult> Index()
    {
        var resultReports = await _service.GenerateReports();
        return View(resultReports);
    }
}