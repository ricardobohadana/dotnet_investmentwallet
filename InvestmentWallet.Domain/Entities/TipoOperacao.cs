using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentWallet.Domain.Entities
{
    public class TipoOperacao
    {
        #region Propriedades

        public Guid IdTipoOperacao { get; set; }

        public string Nome { get; set; }

        #endregion

        #region Relacionamentos

        public List<Operacao> Operacoes { get; set; }

        #endregion
    }
}
