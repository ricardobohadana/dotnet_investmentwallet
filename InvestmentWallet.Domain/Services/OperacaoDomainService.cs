using InvestmentWallet.Domain.Entities;
using InvestmentWallet.Domain.Interfaces.Repositories;
using InvestmentWallet.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentWallet.Domain.Services
{
    public class OperacaoDomainService : IOperacaoDomainService
    {
        private readonly ICarteiraRepository _carteiraRepository;
        private readonly IOperacaoRepository _operacaoRepository;
        private readonly ITipoAtivoRepository _tipoAtivoRepository;
        private readonly ITipoOperacaoRepository _tipoOperacaoRepository;

        public OperacaoDomainService(ICarteiraRepository carteiraRepository, IOperacaoRepository operacaoRepository, ITipoAtivoRepository tipoAtivoRepository, ITipoOperacaoRepository tipoOperacaoRepository)
        {
            _carteiraRepository = carteiraRepository;
            _operacaoRepository = operacaoRepository;
            _tipoAtivoRepository = tipoAtivoRepository;
            _tipoOperacaoRepository = tipoOperacaoRepository;
        }

        public void CriarOperacao(Operacao operacao)
        {
            // REGRA 1: NÃO PERMITIR QUANTIDADE 0 ou NEGATIVA
            if (operacao.QuantidadeAtivo <= 0)
            {
                throw new Exception("Não é possível criar operações com quantidade nula ou negativa.");
            }

            // REGRA 2: NÃO PERMITIR PREÇO NEGATIVO
            if (operacao.PrecoAtivo <= 0)
            {
                throw new Exception("Não é possível criar operações com preço nulo ou negativo.");
            }

            // REGRA 3: SE O TOTAL NÃO ESTIVER PREENCHIDO, PREENCHER COM MULTIPLICAÇÃO ENTRE PREÇOATIVO E QUANTIDADE
            if (operacao.Total == 0)
            {
                operacao.Total = operacao.QuantidadeAtivo * operacao.PrecoAtivo;
            }


            // REGRA 4: NÃO PERMITIR TOTAL NEGATIVO
            if (operacao.Total < 0)
            {
                throw new Exception("Não é possível criar operações com Total nulo ou negativo.");
            }

            // REGRA 5: NÃO PERMITIR CRIAÇÃO DE OPERAÇÕES FUTURAS.
            if (operacao.DataOperacao.Date > DateTime.Today)
            {
                throw new Exception("Não é possível criar operações que ainda serão feitas em datas futuras.");
            }

            _operacaoRepository.Inserir(operacao);
            
        }

        public (List<TipoOperacao>, List<TipoAtivo>, List<Carteira>) FornecerDadosCriacao(Guid idUsuario)
        {
            List<Carteira> carteiras = _carteiraRepository.ObterPorIdUsuario(idUsuario);
            List<TipoOperacao> tiposOperacao = _tipoOperacaoRepository.Consultar();
            List<TipoAtivo> tiposAtivo = _tipoAtivoRepository.Consultar();

            return (tiposOperacao, tiposAtivo, carteiras);
        }

        public List<Operacao>  ObterDadosIndex(Guid idUsuario)
        {
            List<Guid> idsCarteira = _carteiraRepository.ObterPorIdUsuario(idUsuario).Select(carteira => carteira.IdCarteira).ToList();
            return _operacaoRepository.ObterPorListaDeIdCarteiras(idsCarteira);

        }

    }
}
