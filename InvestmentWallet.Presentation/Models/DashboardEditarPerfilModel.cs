using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace InvestmentWallet.Presentation.Models
{
    public class DashboardEditarPerfilModel
    {
        // hidden input
        public Guid IdUsuario { get; set; }

        [Required(ErrorMessage = "Por favor, insira o seu nome.")]
        public string Nome { get; set; }

        [EmailAddress(ErrorMessage = "Por favor, insira um email válido.")]
        [Required(ErrorMessage = "Por favor, insira um email.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Por favor, selecione um perfil de investidor.")]
        public string PerfilInvestidor { get; set; }

        public List<SelectListItem>? SelectItemsPerfilInvestidor { get; set; }
    }
}
