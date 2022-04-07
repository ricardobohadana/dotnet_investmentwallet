﻿using InvestmentWallet.Domain.Entities;
using InvestmentWallet.Domain.Interfaces.Repositories;
using InvestmentWallet.Domain.Interfaces.Services;
using InvestmentWallet.Presentation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InvestmentWallet.Presentation.Controllers
{
    [Authorize]
    public class CarteiraController : Controller
    {
        public IActionResult Index([FromServices] ICarteiraDomainService carteiraDomainService)
        {
            Guid idUsuario = Guid.Parse(HttpContext.User.Identity.Name);

            List<Carteira> carteiraList = carteiraDomainService.ObterCarteirasPorUsuarioComOperacoes(idUsuario);

            CarteiraIndexModel model = new CarteiraIndexModel()
            {
                Carteiras = carteiraList,
                IdUsuario = idUsuario
            };

            return View(model);
        }

        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar(CarteiraCriarModel model, [FromServices] ICarteiraDomainService carteiraDomainService)
        { 
            if (!ModelState.IsValid)
            {
                TempData["MensagemErro"] = "Ocorreu um erro, por favor tente novamente.";
                return View();
            }

            try
            {
                Carteira carteira = new Carteira()
                {
                    IdCarteira = Guid.NewGuid(),
                    Descricao = model.Descricao,
                    Nome = model.Nome,
                    IdUsuario = Guid.Parse(HttpContext.User.Identity.Name)
                };

                carteiraDomainService.Cadastrar(carteira);
                ModelState.Clear();
                TempData["MensagemSucesso"] = $"Parabéns, sua carteira foi criada com sucesso!";

                return View();


            }
            catch (Exception e)
            {

                TempData["MensagemErro"] = "Ocorreu um erro: " + e.Message;
                return View(); 
            }

            return View();
        }
    }
}