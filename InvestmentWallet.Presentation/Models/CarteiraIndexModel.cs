using InvestmentWallet.Domain.Entities;

namespace InvestmentWallet.Presentation.Models
{
    public class CarteiraIndexModel
    {
        public Guid IdUsuario { get; set; }

        public List<Carteira> Carteiras { get; set; }

    }
}
