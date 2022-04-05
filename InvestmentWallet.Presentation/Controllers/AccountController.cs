using InvestmentWallet.Domain.Entities;
using InvestmentWallet.Domain.Interfaces.Repositories;
using InvestmentWallet.Domain.Interfaces.Services;
using InvestmentWallet.Domain.Services;
using InvestmentWallet.Presentation.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;

namespace InvestmentWallet.Presentation.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUsuarioDomainService _usuarioDomainService;

        public AccountController(IUsuarioDomainService usuarioDomainService)
        {
            _usuarioDomainService = usuarioDomainService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(AccountLoginModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["MensagemAlerta"] = $"Ocorreram erros de validação no preenchimento dos dados, por favor verifique!";
                return View();
            }

            try {
                
                Usuario usuario = _usuarioDomainService.AutenticarUsuario(model.Email, model.Senha);

                // salvando a autenticação do usuário
                List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.GivenName, usuario.Nome),
                    new Claim(ClaimTypes.Name, usuario.IdUsuario.ToString())
                };

                ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                // limpando os campos
                ModelState.Clear();

                return RedirectToAction(nameof(DashboardController.Index), "Dashboard");

            }
            catch (Exception e)
            {

                TempData["MensagemErro"] = e.Message;
                return View();

            } 
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(AccountRegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["MensagemAlerta"] = $"Ocorreram erros de validação no preenchimento dos dados, por favor verifique!";
                return View();
            }

            try
            {

                // inicialização do objeto usuario
                Usuario usuario = new Usuario()
                {
                    IdUsuario = Guid.NewGuid(),
                    Nome = model.Nome,
                    Senha = model.Senha,
                    Email = model.Email
                };

                // passando o usuario para as regras de projeto
                _usuarioDomainService.CadastrarUsuario(usuario);


                TempData["MensagemSucesso"] = $"{usuario.Nome}, seu usuário com email {usuario.Email} foi cadastrado.";

                // limpando os campos
                ModelState.Clear();

            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = $"Falha ao cadastrar o usuário: {e.Message}";
                
            }
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login", "Account");
        }
    }
}
