using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SmartBoard.Models;
using SmartBoard.Filters;
using static System.Runtime.InteropServices.JavaScript.JSType;
using SmartBoard.Repositories;

namespace SmartBoard.Controllers;

[AuthFilter]
public class DeviceTecnicoController : Controller
{
    private readonly IPessoaRepository _pessoaRepository;

    public DeviceTecnicoController(IPessoaRepository pessoaRepository)
    {
        _pessoaRepository = pessoaRepository;
    }

    public IActionResult HomeTecnico()
    {
        return View();
    }

    public IActionResult CadastroCliente()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult CadastrarCliente(PessoaClienteViewModel pessoaClienteModel)
    {
        try
        {
            if (ModelState.IsValid)
            {
                // fazendo consulta pelo email e senha
                try
                {
                    _pessoaRepository.CreateClient(pessoaClienteModel);
                    TempData["MensagemSucesso"] = $"O Cliente Foi Cadastrado Com Sucesso";
                    return RedirectToAction("HomeTecnico");
                }
                catch (Exception erro)
                {
                    TempData["MensagemErro"] = $"Erro ao tentar fazer fazer o insert, detalhe: {erro.Message}";
                    return RedirectToAction("CadastroCliente");
                }
                
            }
            return View("CadastroCliente");
        }
        catch (Exception erro)
        {
            TempData["MensagemErro"] = $"Erro ao tentar fazer cadastro de cliente, detalhe: {erro.Message}";
            return RedirectToAction("CadastroCliente");
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}