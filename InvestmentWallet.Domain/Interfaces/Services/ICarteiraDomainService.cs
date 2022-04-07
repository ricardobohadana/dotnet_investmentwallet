using InvestmentWallet.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentWallet.Domain.Interfaces.Services
{
    public interface ICarteiraDomainService
    {
        List<Carteira> ConsultarCarteirasPorUsuario(Guid idUsuario);

        List<Carteira> ObterCarteirasPorUsuarioComOperacoes(Guid idUsuario);

        void Cadastrar(Carteira carteira);
    }
}
