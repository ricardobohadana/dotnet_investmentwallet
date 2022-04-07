using InvestmentWallet.Domain.Entities;
using InvestmentWallet.Domain.Interfaces.Repositories;
using InvestmentWallet.Domain.Interfaces.Services;
using InvestmentWallet.Presentation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;

namespace InvestmentWallet.Presentation.Controllers
{
    [Authorize]
    public class OperacaoController : Controller
    {
        private readonly IOperacaoDomainService _operacaoDomainService;
        private readonly ICarteiraDomainService _carteiraDomainService;

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
                SelectItemsTipoOperacao = ObterSelecao(tiposOperacao),
                SelectItemsTipoAtivo    = ObterSelecao(tiposAtivo),
                SelectItemsCarteira     = ObterSelecao(carteiras),
            };



            return View(model);
        }

        private List<SelectListItem> ObterSelecao(List<Carteira> carteiras)
        {
            List<SelectListItem> lista = new List<SelectListItem>();

            carteiras.ForEach(carteira =>
            {
                lista.Add(
                    new SelectListItem()
                    {
                        Value = carteira.IdCarteira.ToString(),
                        Text = carteira.Nome.ToUpper()

                    }
                );
            });

            return lista;
        }
        private List<SelectListItem> ObterSelecao(List<TipoOperacao> tiposOperacao)
        {
            List<SelectListItem> lista = new List<SelectListItem>();

            tiposOperacao.ForEach(tipoOperacao =>
            {
                lista.Add(
                    new SelectListItem()
                    {
                        Value = tipoOperacao.IdTipoOperacao.ToString(),
                        Text = tipoOperacao.Nome.ToUpper()

                    }
                );
            });

            return lista;
        }
        private List<SelectListItem> ObterSelecao(List<TipoAtivo> tiposAtivo)
        {
            List<SelectListItem> lista = new List<SelectListItem>();

            tiposAtivo.ForEach(tipoAtivo =>
            {
                lista.Add(
                    new SelectListItem()
                    {
                        Value = tipoAtivo.IdTipoAtivo.ToString(),
                        Text = tipoAtivo.Nome.ToUpper()

                    }
                );
            });

            return lista;
        }



        [HttpPost]
        public IActionResult Criar(OperacaoCriarModel model)
        {
            //Criar uma interface para a implementação das regras de negócio para a criação de uma operação.
            Guid idUsuario = Guid.Parse(HttpContext.User.Identity.Name);
            (var tiposOperacao, var tiposAtivo, var carteiras) = _operacaoDomainService.FornecerDadosCriacao(idUsuario);


            if (ModelState.IsValid)
            {
                try
                {
                    Operacao operacao = new Operacao()
                    {
                        IdOperacao      = Guid.NewGuid(),
                        IdCarteira      = Guid.Parse(model.Carteira),
                        IdTipoAtivo     = Guid.Parse(model.TipoAtivo),
                        IdTipoOperacao  = Guid.Parse(model.TipoOperacao),
                        DataOperacao    = DateTime.Parse(model.DataOperacao),
                        NomeAtivo       = model.NomeAtivo,
                        DescricaoAtivo  = model.DescricaoAtivo,
                        Total           = (int) (Decimal.Parse(model.Total ?? "0.00", CultureInfo.InvariantCulture) * 100),
                        PrecoAtivo      = (int) (Decimal.Parse(model.PrecoAtivo, CultureInfo.InvariantCulture) * 100),
                        QuantidadeAtivo = int.Parse(model.QuantidadeAtivo),
                        SiglaAtivo      = model.SiglaAtivo,
                    };

                    _operacaoDomainService.CriarOperacao(operacao);

                    TempData["MensagemSucesso"] = $"Parabéns, sua operação de compra de {operacao.SiglaAtivo} foi criada com sucesso.";

                    ModelState.Clear();

                } catch (Exception e)
                {
                    TempData["MensagemErro"] = e.Message;
                }
            }
            else
            {
                TempData["MensagemErro"] = "Os dados preenchidos não estão corretos, por favor verifique.";
            }


            model.SelectItemsTipoOperacao = ObterSelecao(tiposOperacao);
            model.SelectItemsTipoAtivo = ObterSelecao(tiposAtivo);
            model.SelectItemsCarteira = ObterSelecao(carteiras);

            return View(model);
        }
    }
}
