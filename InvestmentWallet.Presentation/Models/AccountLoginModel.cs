using System.ComponentModel.DataAnnotations;

namespace InvestmentWallet.Presentation.Models
{
    public class AccountLoginModel
    {
        [EmailAddress(ErrorMessage = "Por favor, insira um email válido.")]
        [Required(ErrorMessage = "Por favor, insira um email.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Por favor, insira a senha.")]
        [MaxLength(20, ErrorMessage = "Por favor, insira uma senha com no máximo {1} caracteres")]
        [MinLength(6, ErrorMessage = "Por favor, insira uma senha com no mínimo {1} caracteres")]
        public string Senha { get; set; }


    }
}
