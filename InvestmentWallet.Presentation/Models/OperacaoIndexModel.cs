using InvestmentWallet.Domain.Entities;

namespace InvestmentWallet.Presentation.Models
{
    public class OperacaoIndexModel
    {
        public Guid IdUsuario { get; set; }

        public List<Operacao> Operacoes { get; set; }
    }
}
