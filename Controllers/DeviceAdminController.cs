using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SmartBoard.Models;
using SmartBoard.Filters;
using SmartBoard.Repositories;
using System.Reflection;

namespace SmartBoard.Controllers;

[AuthFilter]
public class DeviceAdminController : Controller
{
    private readonly IClienteRepository _clienteRepository;

    public DeviceAdminController (IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    public IActionResult HomeAdmin()
    {
        Tuple<List<ClienteModel>, List<PessoaModel>, List<TelefoneModel>> clienteDados = _clienteRepository.GetAllClienteInfo();

        return View(clienteDados);
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