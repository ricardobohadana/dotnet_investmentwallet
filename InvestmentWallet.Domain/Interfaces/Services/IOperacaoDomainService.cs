using InvestmentWallet.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentWallet.Domain.Interfaces.Services
{
    public interface IOperacaoDomainService
    {
        public List<Operacao> ObterDadosIndex(Guid idUsuario);

        public (List<TipoOperacao>, List<TipoAtivo>, List<Carteira>) FornecerDadosCriacao(Guid idUsuario);

        public void CriarOperacao(Operacao operacao);

        public Operacao FornecerDadosEdicao(Guid idOperacao);

        public void AtualizarOperacao(Operacao operacao);

    }
}
