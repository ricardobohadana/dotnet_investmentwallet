using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentWallet.Domain.Entities
{
    public class Operacao
    {
        #region Propriedades

        public Guid IdOperacao { get; set; }

        public Guid IdCarteira { get; set; }

        public Guid IdTipoAtivo { get; set; }

        public Guid IdTipoOperacao{ get; set; }

        public string NomeAtivo { get; set; }

        public string SiglaAtivo { get; set; }

        public string DescricaoAtivo { get; set; }

        public DateTime DataOperacao { get; set; }

        public int PrecoAtivo { get; set; }

        public int QuantidadeAtivo { get; set; }

        public int Total { get; set; }

        #endregion

        #region Relacionamentos

        public Carteira Carteira { get; set; }

        public TipoOperacao TipoOperacao { get; set; }

        public TipoAtivo TipoAtivo { get; set; }

        #endregion

    }
}
