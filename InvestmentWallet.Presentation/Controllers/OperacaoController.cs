using InvestmentWallet.Domain.Entities;
using InvestmentWallet.Domain.Interfaces.Repositories;
using InvestmentWallet.Domain.Interfaces.Services;
using InvestmentWallet.Presentation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace InvestmentWallet.Presentation.Controllers
{
    [Authorize]
    public class OperacaoController : Controller
    {
        private readonly IOperacaoDomainService _operacaoDomainService;

        public OperacaoController(IOperacaoDomainService operacaoDomainService)
        {
            _operacaoDomainService = operacaoDomainService;
        }

        public IActionResult Index()
        {
            Guid idUsuario = Guid.Parse(HttpContext.User.Identity.Name);
            
            List<Operacao> operacoes = _operacaoDomainService.ObterDadosIndex(idUsuario);

            OperacaoIndexModel model = new OperacaoIndexModel()
            {
                IdUsuario = idUsuario,
                Operacoes = operacoes
            };

            return View(model);
        }


        public IActionResult Criar()
        {
            Guid idUsuario = Guid.Parse(HttpContext.User.Identity.Name);

            (var tiposOperacao, var tiposAtivo, var carteiras) = _operacaoDomainService.FornecerDadosCriacao(idUsuario);

            OperacaoCriarModel model = new OperacaoCriarModel()
            {
                TiposOperacao = tiposOperacao,
                TiposAtivo = tiposAtivo,
                Carteiras = carteiras,
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Criar(OperacaoCriarModel model)
        {
            //Criar uma interface para a implementação das regras de negócio para a criação de uma operação.
            Guid idUsuario = Guid.Parse(HttpContext.User.Identity.Name);

            (var tiposOperacao, var tiposAtivo, var carteiras) = _operacaoDomainService.FornecerDadosCriacao(idUsuario);
            model.TiposOperacao = tiposOperacao;
            model.TiposAtivo = tiposAtivo;
            model.Carteiras = carteiras;

            if (!ModelState.IsValid)
            {
                TempData["MensagemErro"] = "Os dados preenchidos não estão corretos, por favor verifique.";
                return View(model);
            }
            try
            {
                Operacao operacao = new Operacao()
                {
                    IdOperacao = Guid.NewGuid(),
                    IdCarteira = Guid.Parse(model.Carteira),
                    IdTipoAtivo = Guid.Parse(model.TipoAtivo),
                    IdTipoOperacao = Guid.Parse(model.TipoOperacao),
                    DataOperacao = DateTime.Parse(model.DataOperacao),
                    NomeAtivo = model.NomeAtivo,
                    DescricaoAtivo = model.DescricaoAtivo,
                    Total = (int) (Decimal.Parse(model.Total ?? "0.00", CultureInfo.InvariantCulture) * 100),
                    PrecoAtivo = (int) (Decimal.Parse(model.PrecoAtivo, CultureInfo.InvariantCulture) * 100),
                    QuantidadeAtivo = int.Parse(model.QuantidadeAtivo),
                    SiglaAtivo = model.SiglaAtivo,
                };

                _operacaoDomainService.CriarOperacao(operacao);

                TempData["MensagemSucesso"] = $"Parabéns, sua operação de compra de {operacao.SiglaAtivo} foi criada com sucesso.";

                ModelState.Clear();

                return View(model);

            } catch (Exception e)
            {
                TempData["MensagemErro"] = e.Message;
            }


            return View(model);
        }
    }
}
