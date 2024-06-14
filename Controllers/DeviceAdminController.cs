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

    public IActionResult EditarCliente(int id)
    {
        try
        {
            Tuple<ClienteModel, PessoaUpdateModel, TelefoneModel> clienteDados = _clienteRepository.GetClientById(id);

            PessoaClienteUpdateViewModel pessoaCliente = new PessoaClienteUpdateViewModel();

            pessoaCliente.Pessoa = clienteDados.Item2;
            pessoaCliente.Cliente = clienteDados.Item1;
            pessoaCliente.Telefone = clienteDados.Item3;

            return View(pessoaCliente);
        }
        catch (Exception erro)
        { 
            TempData["MensagemErro"] = $"Erro ao Buscar cliente, tente novamente, detalhe: {erro.Message}";
            return RedirectToAction("HomeAdmin");
        }
    }
    [HttpPost]
    public IActionResult EditarCliente(PessoaClienteUpdateViewModel pessoaClienteModel)
    {
        try
        {
            if (ModelState.IsValid)
            {
                // fazendo consulta pelo email e senha
                try
                {
                    _clienteRepository.UpdatePessoaByCliente(pessoaClienteModel);
                    TempData["MensagemSucesso"] = $"O Cliente Foi Alterado Com Sucesso";
                    return RedirectToAction("HomeAdmin");
                }
                catch (Exception erro)
                {
                    TempData["MensagemErro"] = $"Erro ao tentar fazer fazer o insert, detalhe: {erro.Message}";
                    return RedirectToAction("EditarCliente", pessoaClienteModel);
                }

            }
            return View("EditarCliente", pessoaClienteModel);
        }
        catch (Exception erro)
        {
            TempData["MensagemErro"] = $"Erro ao tentar fazer cadastro de cliente, detalhe: {erro.Message}";
            return RedirectToAction("EditarCliente", pessoaClienteModel);
        }
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