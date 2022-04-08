using InvestmentWallet.Domain.Entities;
using InvestmentWallet.Domain.Interfaces.Services;
using InvestmentWallet.Presentation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

    }
}
