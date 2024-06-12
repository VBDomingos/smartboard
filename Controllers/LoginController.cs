using System;
using System.Data;
using System.Diagnostics;
using System.Net.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SmartBoard.Models;
using SmartBoard.Repositories;

namespace SmartBoard.Controllers;

public class LoginController : Controller
{
    private readonly IPessoaRepository _pessoaRepository;

    public LoginController(IPessoaRepository pessoaRepository)
    {
        _pessoaRepository = pessoaRepository;
    }


    public IActionResult Index()
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

    [HttpPost]
    public IActionResult Entrar(LoginModel loginModel)
    {
        try
        {
            if (ModelState.IsValid)
            {
                // fazendo consulta pelo email e senha
                PessoaModel pessoa = _pessoaRepository.Login(loginModel.Login, loginModel.Senha);
                if (pessoa != null)
                {
                    if (pessoa.ativo == 1)
                    {
                        // Criação da sessão
                        HttpContext.Session.SetString("id_pessoa", pessoa.IdPessoa.ToString());
                        HttpContext.Session.SetString("Email", pessoa.Email);
                        HttpContext.Session.SetString("Nome", pessoa.Nome);
                        HttpContext.Session.SetString("TipoPessoa", pessoa.TipoPessoa);
                        HttpContext.Session.SetString("cep", pessoa.cep);
                        HttpContext.Session.SetString("numero", pessoa.numero.ToString());

                        if (Convert.ToChar(pessoa.TipoPessoa) == 'C') return RedirectToAction("HomeClient", "DeviceClient");
                        else if(Convert.ToChar(pessoa.TipoPessoa) == 'T') return RedirectToAction("HomeTecnico", "DeviceTecnico");
                        else if(Convert.ToChar(pessoa.TipoPessoa) == 'A') return RedirectToAction("HomeAdmin", "DeviceAdmin");

                        //var claims = new List<Claim>
                        //{
                        //    new Claim(ClaimTypes.Name, "Nome", pessoa.Nome)
                        //};
                        //var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        //var authProperties = new AuthenticationProperties();

                        //HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                    }
                    else
                    {
                        // mensagem para usuário inátivo = 0
                        TempData["MensagemErro"] = $"Usuário inativo";
                        return View("Index");
                    }
                }
                else
                {
                    // Exibir mensagem de erro
                    TempData["MensagemErro"] = $"Usuário ou Senha inválidos";
                    return View("Index");
                }
            }
            return View("Index");
        }
        catch (Exception erro)
        {
            TempData["MensagemErro"] = $"Erro ao tentar fazer login, detalhe: {erro.Message}";
            return RedirectToAction("Index");
        }
    }

    // Fazer logout
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index");
    }
}