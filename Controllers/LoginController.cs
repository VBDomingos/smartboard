using System;
using System.Data;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SmartBoard.Models;

namespace SmartBoard.Controllers;

public class LoginController : Controller
{
    private readonly IConfiguration _configuration;

    public LoginController(IConfiguration configuration)
    {
        _configuration = configuration;
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
                Pessoa pessoa = null;

                string connectionString = _configuration.GetConnectionString("DefaultConnection");

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT id_pessoa, nome, email, TipoPessoa, cep, numero, ativo FROM Pessoas WHERE Email = @Login AND senha = @Password";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@Login", SqlDbType.VarChar).Value = loginModel.Login;
                        cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = loginModel.Senha;

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                pessoa = new Pessoa
                                {
                                    IdPessoa = (int)reader["id_pessoa"],
                                    Email = reader["email"].ToString(),
                                    Nome = reader["Nome"].ToString(),
                                    TipoPessoa = reader["TipoPessoa"].ToString(),
                                    cep = reader["cep"].ToString(),
                                    numero = Convert.ToInt16(reader["numero"]),
                                    ativo = Convert.ToInt16(reader["ativo"])
                                };
                            }
                        }
                    }
                }
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

                        if(Convert.ToChar(pessoa.TipoPessoa) == 'C') return RedirectToAction("HomeClient", "DeviceClient");
                        else if(Convert.ToChar(pessoa.TipoPessoa) == 'T') return RedirectToAction("HomeTecnico", "DeviceTecnico");
                        else if(Convert.ToChar(pessoa.TipoPessoa) == 'A') return RedirectToAction("HomeAdmin", "DeviceAdmin");


                    }
                    else
                    {
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
}