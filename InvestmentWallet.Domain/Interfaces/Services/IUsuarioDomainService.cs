using InvestmentWallet.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentWallet.Domain.Interfaces.Services
{
    public interface IUsuarioDomainService
    {
        void CadastrarUsuario(Usuario usuario);

        Usuario AutenticarUsuario(string email, string senha);

        Usuario ObterUsuarioPorId(Guid id);
    }
}
