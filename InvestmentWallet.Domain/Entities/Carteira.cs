using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentWallet.Domain.Entities
{
    public class Carteira
    {
        #region Propriedades

        public Guid IdCarteira { get; set; }

        public string Nome { get; set; }

        public string Descricao { get; set; }

        public Guid IdUsuario { get; set; }


        #endregion

        #region Relacionamentos

        public Usuario Usuario { get; set; }

        public List<Operacao> Operacoes { get; set; }

        #endregion
    }
}
