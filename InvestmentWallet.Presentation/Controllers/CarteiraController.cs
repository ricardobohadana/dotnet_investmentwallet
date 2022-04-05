using InvestmentWallet.Domain.Entities;
using InvestmentWallet.Domain.Interfaces.Repositories;
using InvestmentWallet.Presentation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InvestmentWallet.Presentation.Controllers
{
    [Authorize]
    public class CarteiraController : Controller
    {
        public IActionResult Index([FromServices] ICarteiraRepository carteiraRepository, [FromServices] IOperacaoRepository operacaoRepository)
        {
            Guid idUsuario = Guid.Parse(HttpContext.User.Identity.Name);

            List<Carteira> carteiraList = carteiraRepository.ObterPorIdUsuario(idUsuario);
            List<Operacao> operacaoList = operacaoRepository.ObterPorListaDeIdCarteiras(
                carteiraList.Select(carteira => carteira.IdCarteira).ToList()
            );
            foreach (Carteira carteira in carteiraList)
            {
                carteira.Operacoes = operacaoList.FindAll(op => op.Carteira.IdCarteira == carteira.IdCarteira).ToList();
            }

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
        public IActionResult Criar(CarteiraCriarModel model, [FromServices] ICarteiraRepository carteiraRepository)
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

                carteiraRepository.Inserir(carteira);
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
