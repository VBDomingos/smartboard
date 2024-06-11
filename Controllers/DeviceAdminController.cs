using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SmartBoard.Models;
using SmartBoard.Filters;

namespace SmartBoard.Controllers;

[AuthFilter]
public class DeviceAdminController : Controller
{
    public IActionResult HomeAdmin()
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