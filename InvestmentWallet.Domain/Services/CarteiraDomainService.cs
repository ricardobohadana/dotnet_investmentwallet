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
    public class CarteiraDomainService : ICarteiraDomainService
    {
        private readonly ICarteiraRepository _carteiraRepository;
        private readonly IOperacaoRepository _operacaoRepository;

        public CarteiraDomainService(ICarteiraRepository carteiraRepository, IOperacaoRepository operacaoRepository)
        {
            _carteiraRepository = carteiraRepository;
            _operacaoRepository = operacaoRepository;
        }

        public List<Carteira> ConsultarCarteirasPorUsuario(Guid idUsuario)
        {
            return _carteiraRepository.ObterPorIdUsuario(idUsuario);
        }


        public List<Carteira> ObterCarteirasPorUsuarioComOperacoes(Guid idUsuario)
        {
            List<Carteira> carteiraList = ConsultarCarteirasPorUsuario(idUsuario);
            List<Guid> carteiraIds = carteiraList.Select(carteira => carteira.IdCarteira).ToList();
            List<Operacao> operacaoList = _operacaoRepository.ObterPorListaDeIdCarteiras(carteiraIds);

            foreach (Carteira carteira in carteiraList)
            {
                carteira.Operacoes = operacaoList.FindAll(op => op.Carteira.IdCarteira == carteira.IdCarteira).ToList();
            }

            return carteiraList;
        }

        public void Cadastrar(Carteira carteira)
        {
            //cadastrando carteiras
            try
            {
                _carteiraRepository.Inserir(carteira);
            }
            catch (Exception e)
            {
                throw new Exception("Ocorreu um erro ao cadastrar a carteira no banco de dados:\n" + e.Message);
            }
        }
    }
}
