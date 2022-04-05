using InvestmentWallet.Domain.Entities;
using InvestmentWallet.Domain.Interfaces.Services;
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
            return View();
        }

    }
}
