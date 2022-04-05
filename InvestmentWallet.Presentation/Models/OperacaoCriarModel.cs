using InvestmentWallet.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace InvestmentWallet.Presentation.Models
{
    public class OperacaoCriarModel
    {
        [Required(ErrorMessage = "Por favor, preencha este campo.")]
        public string NomeAtivo { get; set; }

        [Required(ErrorMessage = "Por favor, preencha este campo.")]
        public string SiglaAtivo { get; set; }

        [Required(ErrorMessage = "Por favor, preencha este campo.")]
        public string DescricaoAtivo { get; set; }

        [Required(ErrorMessage = "Por favor, preencha este campo.")]
        public string PrecoAtivo { get; set; }

        [Required(ErrorMessage = "Por favor, preencha este campo.")]
        public string DataOperacao { get; set; }

        [Required(ErrorMessage = "Por favor, preencha este campo.")]
        public string QuantidadeAtivo { get; set; }

        public string? Total { get; set; }

        [Required(ErrorMessage = "Por favor, preencha este campo.")]
        public string Carteira { get; set; }


        public List<Carteira>? Carteiras { get; set; }

        [Required(ErrorMessage = "Por favor, preencha este campo.")]
        public string TipoAtivo { get; set; }

        public List<TipoAtivo>? TiposAtivo { get; set; }

        [Required(ErrorMessage = "Por favor, selecione um tipo de operação.")]
        public string TipoOperacao { get; set; }

        public List<TipoOperacao>? TiposOperacao { get; set; }
    }
}
