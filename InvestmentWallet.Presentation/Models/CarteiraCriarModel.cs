using System.ComponentModel.DataAnnotations;

namespace InvestmentWallet.Presentation.Models
{
    public class CarteiraCriarModel
    {

        [Required(ErrorMessage = "Por favor, insira um nome para sua carteira.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Por favor, insira uma descrição para sua carteira.")]
        public string Descricao { get; set; }

    }
}
