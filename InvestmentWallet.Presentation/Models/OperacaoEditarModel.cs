using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace InvestmentWallet.Presentation.Models
{
    public class OperacaoEditarModel
    {
        // campo oculto
        public Guid IdOperacao { get; set; }

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


        [Required(ErrorMessage = "Por favor, preencha este campo.")]
        public string TipoAtivo { get; set; }


        [Required(ErrorMessage = "Por favor, selecione um tipo de operação.")]
        public string TipoOperacao { get; set; }


        public List<SelectListItem>? SelectItemsCarteira { get; set; }
        public List<SelectListItem>? SelectItemsTipoAtivo { get; set; }
        public List<SelectListItem>? SelectItemsTipoOperacao { get; set; }
    }
}
