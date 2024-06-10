using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SmartBoard.Models;

namespace SmartBoard.Controllers;

public class LoginController : Controller
{
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

                //if (contato != null)
                //{
                //    if (contato.SenhaValida(loginModel.Senha))
                //    {
                //        _sessao.CriarSessaoUsuario(contato);
                //        return RedirectToAction("Index", "Home");
                //    }
                //    TempData["MensagemErro"] = $"Senha inválida";
                //    return RedirectToAction("Index");
                //}
                if(loginModel.Login =="adm" )
                {
                    return RedirectToAction("Index", "Home");
                }
                TempData["MensagemErro"] = $"usuário inválidos";
                return RedirectToAction("Index");
            }
            return View("Index");
        }
        catch (Exception erro)
        {
            TempData["MensagemErro"] = $"Erro ao tentar fazer login, detalhe: {erro.Message}";
            return RedirectToAction("Index");
        }
    }
}