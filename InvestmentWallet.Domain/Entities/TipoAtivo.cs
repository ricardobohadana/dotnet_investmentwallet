using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentWallet.Domain.Entities
{
    public class TipoAtivo
    {
        #region Propriedades

        public Guid IdTipoAtivo { get; set; }

        public string Nome { get; set; }

        #endregion

        #region Relacionamentos

        public List<Operacao> Operacoes { get; set; }

        #endregion
    }
}
