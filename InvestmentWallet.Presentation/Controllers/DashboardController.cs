using InvestmentWallet.Domain.Entities;
using InvestmentWallet.Domain.Interfaces.Services;
using InvestmentWallet.Presentation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace InvestmentWallet.Presentation.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly IUsuarioDomainService _usuarioDomainService;

        public DashboardController(IUsuarioDomainService usuarioDomainService)
        {
            _usuarioDomainService = usuarioDomainService;
        }

        public IActionResult Index()
        {
            Usuario usuario = _usuarioDomainService.ObterUsuarioPorId(Guid.Parse(HttpContext.User.Identity.Name));
            DashboardIndexModel model = new DashboardIndexModel()
            {
                usuario = usuario,
            };

            return View(model);
        }

        private List<SelectListItem> ObterSelecao(List<PerfilInvestidor> perfisInvestidor)
        {
            List<SelectListItem> lista = new List<SelectListItem>();
            perfisInvestidor.ForEach(perfil =>
            {
                lista.Add(
                    new SelectListItem()
                    {
                        Value = perfil.Id.ToString(),
                        Text = perfil.Tipo.ToUpper()
                    }
                );
            });

            return lista;
        }

        public IActionResult EditarPerfil()
        {

            Usuario usuario = _usuarioDomainService.ObterUsuarioPorId(Guid.Parse(HttpContext.User.Identity.Name));
            List<PerfilInvestidor> perfisInvestidor = _usuarioDomainService.ObterPerfis();


            DashboardEditarPerfilModel model = new DashboardEditarPerfilModel()
            {
                Email = usuario.Email,
                SelectItemsPerfilInvestidor = ObterSelecao(perfisInvestidor),
                PerfilInvestidor = usuario.PerfilInvestidor.Id.ToString(),
                Nome = usuario.Nome,
                IdUsuario = usuario.IdUsuario
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult EditarPerfil(DashboardEditarPerfilModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Usuario usuario = new Usuario()
                    {
                        Email = model.Email,
                        Nome = model.Nome,
                        IdUsuario = model.IdUsuario,
                        IdPerfilInvestidor = Guid.Parse(model.PerfilInvestidor)
                    };

                    _usuarioDomainService.AtualizarUsuario(usuario);

                    TempData["MensagemSucesso"] = "As alterações foram realizadas com sucesso.";
                }
                catch (Exception e)
                {

                    TempData["MensagemErro"] = e.Message;
                }

            }

            List<PerfilInvestidor> perfisInvestidor = _usuarioDomainService.ObterPerfis();

            model.SelectItemsPerfilInvestidor = ObterSelecao(perfisInvestidor);
            
            return View(model);
        }
    }
}
