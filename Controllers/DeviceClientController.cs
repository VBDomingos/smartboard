using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SmartBoard.Models;
using SmartBoard.Filters;
using SmartBoard.Repositories;

namespace SmartBoard.Controllers;

[AuthFilter]
public class DeviceClientController : Controller
{
    public IActionResult HomeClient()
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