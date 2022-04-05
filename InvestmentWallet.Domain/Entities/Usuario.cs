using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentWallet.Domain.Entities
{

    public class Usuario
    {

        #region Propriedades

        public Guid IdUsuario { get; set; }

        public Guid IdPerfilInvestidor { get; set; }

        public string Nome { get; set; }

        public string Senha { get; set; }

        public string Email { get; set; }
        

        #endregion

        #region Relacionamentos

        public PerfilInvestidor PerfilInvestidor { get; set; }

        public List<Carteira> Carteiras { get; set; }

        #endregion


    }
}
