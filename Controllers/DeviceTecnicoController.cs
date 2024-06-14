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
    private readonly IClienteRepository _clienteRepository;
    private readonly IDispositivoRepository _dispositivoRepository;
    private readonly IAmbienteRepository _ambienteRepository;
    private readonly ITipoDispositivoRepository _tipoDispositivoRepository;

    public DeviceTecnicoController(IPessoaRepository pessoaRepository, IClienteRepository clienteRepository, IDispositivoRepository dispositivoRepository, IAmbienteRepository ambienteRepository, ITipoDispositivoRepository tipoDispositivoRepository)
    {
        _pessoaRepository = pessoaRepository;
        _clienteRepository = clienteRepository;
        _dispositivoRepository = dispositivoRepository;
        _ambienteRepository = ambienteRepository;
        _tipoDispositivoRepository = tipoDispositivoRepository;
    }

    public IActionResult HomeTecnico()
    {
        return View();
    }

    public IActionResult CadastroCliente()
    {
        return View();
    }

    public IActionResult ListarDispositivoClientes(int Id)
    {
        Tuple<ClienteModel, List<DispositivoModel>, List<AmbienteModel>, List<TipoDispositivoModel>> clienteDispositivo = _dispositivoRepository.GetAllDispositivosByCliente(Id);
        return View(clienteDispositivo);
    }

    public IActionResult AdicionarDispositivoCliente(int Id)
    {
        DispositivoAmbienteViewModel dispositivoAmbienteView = new DispositivoAmbienteViewModel();

        dispositivoAmbienteView.Ambiente = (List<AmbienteModel>)_ambienteRepository.GetAllAmbientes();

        return View(dispositivoAmbienteView);
    }

    [HttpPost]
    public IActionResult AdicionarDispositivoCliente(int Id, DispositivoAmbienteViewModel dispositivoAmbienteView)
    {
        try
        {
            if (ModelState.IsValid)
            {
                // fazendo consulta pelo email e senha
                try
                {
                    //_pessoaRepository.CreateClient(dispositivoAmbienteView);
                    TempData["MensagemSucesso"] = $"O Cliente Foi Cadastrado Com Sucesso";
                    return RedirectToAction("HomeTecnico");
                }
                catch (Exception erro)
                {
                    TempData["MensagemErro"] = $"Erro ao tentar fazer fazer o insert, detalhe: {erro.Message}";
                    return RedirectToAction("CadastroCliente");
                }

            }
            return View("AdicionarDispositivoCliente");
        }
        catch (Exception erro)
        {
            TempData["MensagemErro"] = $"Erro ao tentar fazer cadastro de cliente, detalhe: {erro.Message}";
            return RedirectToAction("AdicionarDispositivoCliente");
        }
    }

    public IActionResult ListarClientes()
    {
        Tuple<List<ClienteModel>, List<PessoaModel>, List<TelefoneModel>> clienteDados = _clienteRepository.GetAllClienteInfo();

        return View(clienteDados);
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